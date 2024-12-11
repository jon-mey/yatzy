namespace Yatzy.Tests;

public class BeregnPoengTests
{
    private readonly YatzyPoengberegner _yatzy = new();

    [Theory]
    [InlineData(Kategori.Enere, "1, 1, 2, 3, 4", 2)]
    [InlineData(Kategori.Toere, "1, 1, 2, 3, 2", 4)]
    [InlineData(Kategori.Treere, "1, 1, 2, 3, 2", 3)]
    [InlineData(Kategori.Firere, "1, 4, 2, 3, 4", 8)]
    [InlineData(Kategori.Femmere, "1, 5, 5, 5, 4", 15)]
    [InlineData(Kategori.Seksere, "1, 6, 5, 5, 6", 12)]
    [InlineData(Kategori.Seksere, "1, 5, 5, 5, 2", 0)]
    public void Test_Xere(Kategori kategori, string kast, int forventet)
    {
        int resultat = _yatzy.BeregnPoeng(kast, kategori);

        Assert.Equal(forventet, resultat);
    }

    [Fact]
    public void Test_Par()
    {
        string kast = "1, 1, 5, 5, 6";

        int resultat = _yatzy.BeregnPoeng(kast, Kategori.Par);

        Assert.Equal(10, resultat);
    }

    [Fact]
    public void Test_ToPar()
    {
        string kast = "1, 1, 5, 5, 6";

        int resultat = _yatzy.BeregnPoeng(kast, Kategori.ToPar);

        Assert.Equal(12, resultat);
    }

    [Fact]
    public void Test_ToPar_Ugyldig()
    {
        string kast = "1, 2, 5, 5, 6";

        int resultat = _yatzy.BeregnPoeng(kast, Kategori.ToPar);

        Assert.Equal(0, resultat);
    }

    [Fact]
    public void Test_TreLike()
    {
        string kast = "5, 1, 5, 2, 5";

        int resultat = _yatzy.BeregnPoeng(kast, Kategori.TreLike);

        Assert.Equal(15, resultat);
    }

    [Fact]
    public void Test_FireLike()
    {
        string kast = "1, 1, 4, 1, 1";

        int resultat = _yatzy.BeregnPoeng(kast, Kategori.FireLike);

        Assert.Equal(4, resultat);
    }

    [Fact]
    public void Test_LitenStraight_Gyldig()
    {
        string kast = "1, 2, 3, 4, 5";

        int resultat = _yatzy.BeregnPoeng(kast, Kategori.LitenStraight);

        Assert.Equal(15, resultat);
    }

    [Fact]
    public void Test_LitenStraight_Ugyldig()
    {
        string kast = "2, 2, 3, 4, 5";

        int resultat = _yatzy.BeregnPoeng(kast, Kategori.LitenStraight);

        Assert.Equal(0, resultat);
    }

    [Fact]
    public void Test_StorStraight_Gyldig()
    {
        string kast = "2, 3, 4, 5, 6";

        int resultat = _yatzy.BeregnPoeng(kast, Kategori.StorStraight);

        Assert.Equal(20, resultat);
    }

    [Fact]
    public void Test_StorStraight_Ugyldig()
    {
        string kast = "3, 3, 4, 5, 6";

        int resultat = _yatzy.BeregnPoeng(kast, Kategori.StorStraight);

        Assert.Equal(0, resultat);
    }

    [Fact]
    public void Test_FulltHus_Gyldig_1()
    {
        string kast = "2, 5, 5, 2, 2";

        int resultat = _yatzy.BeregnPoeng(kast, Kategori.FulltHus);

        Assert.Equal(16, resultat);
    }

    [Fact]
    public void Test_FulltHus_Gyldig_2()
    {
        string kast = "6, 2, 6, 2, 6";

        int resultat = _yatzy.BeregnPoeng(kast, Kategori.FulltHus);

        Assert.Equal(22, resultat);
    }

    [Fact]
    public void Test_FulltHus_Ugyldig()
    {
        string kast = "1, 1, 4, 3, 1";

        int resultat = _yatzy.BeregnPoeng(kast, Kategori.FulltHus);

        Assert.Equal(0, resultat);
    }

    [Fact]
    public void Test_Sjanse()
    {
        string kast = "1, 5, 2, 4, 5";

        int resultat = _yatzy.BeregnPoeng(kast, Kategori.Sjanse);

        Assert.Equal(1 + 5 + 2 + 4 + 5, resultat);
    }

    [Fact]
    public void Test_Yatzy()
    {
        string kast = "1, 1, 1, 1, 1";

        int resultat = _yatzy.BeregnPoeng(kast, Kategori.Yatzy);

        Assert.Equal(50, resultat);
    }

    [Fact]
    public void Test_Yatzy_Ugyldig()
    {
        string kast = "1, 1, 1, 1, 2";

        int resultat = _yatzy.BeregnPoeng(kast, Kategori.Yatzy);

        Assert.Equal(0, resultat);
    }

    [Fact]
    public void Test_AlleKategorierErDekt()
    {
        string kast = "1, 1, 1, 1, 2";

        Kategori[] kategorier = Enum.GetValues<Kategori>();
        // start på 1 for å hoppe over Kategori.None
        for (int i = 1; i < kategorier.Length; i++)
        {
            // thrower hvis en kategori feiler
            _yatzy.BeregnPoeng(kast, kategorier[i]);
        }
    }
}