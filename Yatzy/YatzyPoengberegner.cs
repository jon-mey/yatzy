namespace Yatzy;

public class YatzyPoengberegner
{
    // Merk sjanse ofte har en lik eller høyere sum til f.eks. liten og stor straight, fire like osv.
    // Men siden sjanse er plassert nest sist i enum og vi itererer fra start vil vi foretrekke andre kategorier
    // Neste steg kunne evt. vært å ta hensyn til hvilke kategorier man allerede har "brukt opp",
    // og i tillegg returnere en List<Resultat> med de som gir lik sum
    public Resultat Max(string kast)
    {
        int maxPoeng = 0;
        Kategori maxKategori = Kategori.None;
        Kategori[] kategorier = Enum.GetValues<Kategori>();

        for (int i = 1; i < kategorier.Length; i++)
        {
            Kategori kategori = kategorier[i];
            int poeng = BeregnPoeng(kast, kategori);

            if (poeng > maxPoeng)
            {
                maxPoeng = poeng;
                maxKategori = kategori;
            }
        }

        return new Resultat(maxPoeng, maxKategori);
    }

    public int BeregnPoeng(string kast, Kategori kategori)
    {
        int[] antall = ParseKast(kast);

        return kategori switch
        {
            Kategori.Enere => 1 * antall[0],
            Kategori.Toere => 2 * antall[1],
            Kategori.Treere => 3 * antall[2],
            Kategori.Firere => 4 * antall[3],
            Kategori.Femmere => 5 * antall[4],
            Kategori.Seksere => 6 * antall[5],
            Kategori.Par => BeregnLike(antall, 2),
            Kategori.TreLike => BeregnLike(antall, 3),
            Kategori.FireLike => BeregnLike(antall, 4),
            Kategori.ToPar => BeregnToPar(antall),
            Kategori.LitenStraight => BeregnLitenStraight(antall),
            Kategori.StorStraight => BeregnStorStraight(antall),
            Kategori.FulltHus => BeregnFulltHus(antall),
            Kategori.Sjanse => BeregnSjanse(antall),
            Kategori.Yatzy => BeregnLike(antall, 5) > 0 ? 50 : 0,

            _ => throw new InvalidOperationException($"Ukjent kategori {kategori}"),
        };
    }
    
    // returnerer array med <øyne>: <antall>,
    // [0] = antall terninger med øyne 1,
    // ..
    // [5] = antall terninger med øyne 6
    private static int[] ParseKast(string kast)
    {
        int[] verdier = kast.Split(",")
            .Select(x => x.Trim())
            .Select(int.Parse)
            .ToArray();

        if (verdier.Length != 5)
        {
            throw new ArgumentOutOfRangeException($"Kast inneholder feil antall terninger {verdier.Length} ({kast})");
        }

        int[] antall = new int[6];

        foreach (var v in verdier)
        {
            if (v < 0 || v > 6)
            {
                throw new ArgumentOutOfRangeException($"Kast inneholder ugyldig verdi {v} ({kast})");
            }

            antall[v - 1]++;
        }

        return antall;
    }


    // generelt: metodene antar 5 terninger

    private static int BeregnLike(int[] antall, int like)
    {
        for (int i = antall.Length - 1; i >= 0; i--)
        {
            if (antall[i] == like)
            {
                return (i + 1) * like;
            }
        }

        return 0;
    }

    private static int BeregnToPar(int[] antall)
    {
        int resultat = 0;
        bool toPar = false;

        for (int i = antall.Length - 1; i >= 0; i--)
        {
            if (antall[i] == 2)
            {
                if (resultat > 0)
                {
                    toPar = true;
                }

                resultat += 2 * (i + 1);
            }
        }

        return toPar ? resultat : 0;
    }

    private static int BeregnSjanse(int[] antall)
    {
        int resultat = 0;

        for (int i = 0; i < antall.Length; i++)
        {
            resultat += (i + 1) * antall[i];
        }

        return resultat;
    }

    private static int BeregnLitenStraight(int[] antall)
    {
        for (int i = 0; i < antall.Length - 1; i++)
        {
            if (antall[i] != 1)
            {
                return 0;
            }
        }

        return 15;
    }

    private static int BeregnStorStraight(int[] antall)
    {
        for (int i = 1; i < antall.Length; i++)
        {
            if (antall[i] != 1)
            {
                return 0;
            }
        }

        return 20;
    }

    private static int BeregnFulltHus(int[] antall)
    {
        int treLikePoeng = 0;
        int toLikePoeng = 0;

        for (int i = 0; i < antall.Length; i++)
        {
            int like = antall[i];

            if (like == 3)
            {
                treLikePoeng = 3 * (i + 1);
            }
            else if (like == 2)
            {
                toLikePoeng = 2 * (i + 1);
            }
        }

        return (treLikePoeng > 0 && toLikePoeng > 0) ? (treLikePoeng + toLikePoeng) : 0;
    }
}
