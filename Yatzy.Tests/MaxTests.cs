namespace Yatzy.Tests;

public class MaxTests
{
    private readonly YatzyPoengberegner _yatzy = new();

    [Fact]
    public void Test_Yatzy()
    {
        string kast = "2, 2, 2, 2, 2";

        Resultat resultat = _yatzy.Max(kast);

        Assert.Equal(50, resultat.Poeng);
        Assert.Equal(Kategori.Yatzy, resultat.Kategori);
    }

    [Fact]
    public void Test_StorStraight()
    {
        string kast = "2, 3, 4, 5, 6";

        Resultat resultat = _yatzy.Max(kast);

        Assert.Equal(20, resultat.Poeng);
        Assert.True(resultat.Kategori == Kategori.StorStraight || resultat.Kategori == Kategori.Sjanse);
    }

    [Fact]
    public void Test_LitenStraight()
    {
        string kast = "1, 2, 3, 4, 5";

        Resultat resultat = _yatzy.Max(kast);

        Assert.Equal(15, resultat.Poeng);
        Assert.True(resultat.Kategori == Kategori.LitenStraight || resultat.Kategori == Kategori.Sjanse);
    }

    [Fact]
    public void Test_FulltHus()
    {
        string kast = "1, 1, 6, 6, 6";

        Resultat resultat = _yatzy.Max(kast);

        Assert.Equal(20, resultat.Poeng);
        Assert.True(resultat.Kategori == Kategori.FulltHus || resultat.Kategori == Kategori.Sjanse);
    }
}
