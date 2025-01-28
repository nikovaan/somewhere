// Tee ohjelma, joka kysyy käyttäjältä kolmen henkilön pituudet ja tulostaa pituuksien keskiarvon.

using System;       //ei tarvitse kirjoittaa System.Console.Etc myöhemmin. käsittääkseni tosin viimeisimmässä C# muodossa tätä ei enää tarvitse tehdä

class Ohjelma
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;         //varmistetaan, että öökköset toimivat
        try
        {
            Console.Write("Anna henkilön 1 pituus: ");
            double height1 = double.Parse(Console.ReadLine());      //kysytään pituutta try-lausekkeen kautta siltä varalta, että käyttäjän antama syöte ei käänny double muotoon
            Console.Write("Anna henkilön 2 pituus: ");
            double height2 = double.Parse(Console.ReadLine());
            Console.Write("Anna henkilön 3 pituus: ");
            double height3 = double.Parse(Console.ReadLine());
            Console.Write($"Pituuksien keskiarvo on {(height1 + height2 + height3) / 3:N2}");       //tulostetaan pituuksien keskiarvo kahden desimaalin tarkkuudella. tämän olisi voinut tietenkin tehdä try lausekkeen ulkopuolellakin.
        }
        catch (Exception e)
        {
            Console.WriteLine("Virheellinen syöte.", e);            //jos käyttäjän antama syöte ei taipunut double muotoon, päädymme tänne
            throw;
        }
    }
}