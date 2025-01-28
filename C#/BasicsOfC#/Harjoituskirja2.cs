/*
 Tee ohjelma, joka pyytää käyttäjältä merkin (char), tekstin (string),
kokonaisluvun (int) ja desimaaliluvun (decimal). Ohjelman tulee lukea
syötteet, minkä jälkeen se kääntää syötteet luvuiksi, muokkaa niitä
jollain tavalla sekä tulostaa muutetut arvot näytölle. Tee ohjelmaan
tyyppimuunnos, jossa testaan kuinka tyyppimuunnos tehdään
explisiittisesti int to decimal ja decimal to int. Tulosta näytölle luvut
ennen ja jälkeen tyyppimuunnoksen.
*/

using System;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        char merkki;
        double merkkinumero;
        string teksti;
        double tekstinumero;
        double tekstien_summa;
        bool tekstilippu;
        int kokonaisluku;        
        double desimaali;

        //alustetaan kaikki muuttujat heti siltä varalta, että jokin niistä ei tulisi vastaan käyttäjän syötteiden takia. asian ei pitäisi olla ongelma, mutta varmuus on kaikki kaikessa.

        try
        {
            Console.Write("Anna merkki: ");
            merkki = char.Parse(Console.ReadLine());
            Console.Write("Anna teksti: ");
            teksti = Console.ReadLine();
            Console.Write("Anna kokonaisluku: ");
            kokonaisluku = int.Parse(Console.ReadLine());
            Console.Write("Anna desimaaliluku: ");
            desimaali = double.Parse(Console.ReadLine());
        }
        catch (Exception e)
        {
            Console.WriteLine("Virheellinen syöte.", e);
            return;
        }
        Console.WriteLine($"Antamasi merkki: {merkki}, teksti: {teksti}, kokonaisluku: {kokonaisluku}, desimaaliluku: {desimaali}.");
        merkkinumero = char.GetNumericValue(merkki);                //vaikka merkille olisi annettu kirjain, se saa silti numeerisen arvon -1
        if (double.TryParse(teksti, out tekstinumero) == true)
        {
            tekstilippu = true;                                     //tarkistetaan tuliko tekstin syötteelle numero, ja asetetaan lippu sen mukaan. tämän varmaankin olisi voinut tehdä hienommin, mutta näinkin toimii ihan hyvin.
        }
        else
        {
            Console.WriteLine("Antamasi teksti ei kääntynyt luvuksi.");
            tekstilippu = false;
        }
        if (tekstilippu == true)
        {
            tekstien_summa = tekstinumero + merkkinumero;
            Console.WriteLine($"Tekstin ja merkin summa on {tekstien_summa}");      //merkkinumeroa ei tarkisteta, koska sieltä tulee ainakin -1 jos ei muuta, joten laskutoimitus toimii.
        }
        else
        {
            Console.WriteLine($"Merkki numerona on {merkkinumero}, ja tuplattuna se on {merkkinumero * 2}");
        }
        Console.WriteLine($"Kokonaisluvun ja desimaalin summa on {kokonaisluku + desimaali}.");
        double muunnettu_kokonaisluku = (double)kokonaisluku;
        int muunnettu_desimaali = (int)desimaali;                       //explisiittesti käännetään numerot toisiinsa. desimaali -> int menettää tietoa, mutta minkäs tekee.
        Console.WriteLine($"Muunnosten jälkeen kokonaisluku on {muunnettu_kokonaisluku} ja desimaali on {muunnettu_desimaali}.");
    }
}
