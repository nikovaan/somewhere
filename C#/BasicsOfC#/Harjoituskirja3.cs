/*
T3) Tee ohjelma, joka tulostaa bensakuitin. Bensan hinta on vakio 2.07
E/litra. Ohjelma kysyy käyttäjältä ostetun bensamäärän litroina
(desimaalit mahdollisia) ja annetun rahan ja tulostaa ruutuun kuitin,
joka on suunnilleen seuraavanlainen: (10 p)
Bensakuitti
Litrahinta on 2.07 €/litra
Ostettumäärä on 10.00 litraa
Maksettava 20.70 €
Maksettu 25.00 €
Takaisin 4.30 €
*/

using System;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        double litrahinta = 2.07;
        double ostomaara;
        double maksu;

        Console.Write("Monta litraa polttoainetta ostatte? ");
        try
        {
            ostomaara = double.Parse(Console.ReadLine());
        }
        catch (Exception e)
        {
            Console.WriteLine("Virheellinen syöte.", e);
            return;
        }
        Console.Write("Montako euroa maksatte? ");
        try
        {
            maksu = double.Parse(Console.ReadLine());
        }
        catch (Exception e)
        {
            Console.WriteLine("Virheellinen syöte.", e);
            return;
        }
        if ((ostomaara * litrahinta) <= maksu)      //jonkinlaisen realismin vuoksi katsotaan onko käyttäjä maksamassa tarpeeksi polttoainemäärää varten
        {
            Console.Write($"\nBensakuitti\nLitrahinta on 2,07 €/litra\nOstettu määrä on {ostomaara}\nMaksettava {ostomaara * litrahinta} €\nMaksettu {maksu} €\nTakaisin {maksu - (ostomaara * litrahinta):N2} €\n");
        }
        else
        {
            Console.Write($"\nBensakuitti\nLitrahinta on 2,07 €/litra\nOstettu määrä on {maksu/litrahinta:N2}\nMaksettava {maksu} €\nMaksettu {maksu} €\nTakaisin 0 €\n");
            //jos käyttäjä ei maksa tarpeeksi käytetään hieman erilaista matikkaa kuittia varten, jotta ei anneta velkaa takaisin. molemmissa kuitessa käytetään kahden desimaalin tarkkuutta
        }
        
    }
}