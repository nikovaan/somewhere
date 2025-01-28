/*
T4) Tee ohjelma, joka kysyy käyttäjältä pituuden ja painon. Ohjelma
laskee painoindeksin kaavalla
indeksi = paino / (pituus * pituus)
Paino annetaan kiloina ja pituus metreinä.
Jos indeksi on 20-25, ohjelma toteaa sinut normaalipainoiseksi. Jos
indeksi on < 20, ohjelma ehdottaa syömisen lisäämistä. Jos indeksi on 25-
30, ohjelma ehdottaa pientä liikunnan lisäystä. Jos indeksi on yli 30,
ohjelma kehottaa tekemään jotain radikaalia. (10 p)
*/

using System;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        double height;
        double weight;

        Console.Write("Pituus metreinä: ");
        try
        {
            height = double.Parse(Console.ReadLine());
        }
        catch (Exception e)
        {
            Console.WriteLine("Virheellinen syöte.", e);
            return;
        }

        Console.Write("Paino kilogrammoina: ");
        try
        {
            weight = double.Parse(Console.ReadLine());
        }
        catch (Exception e)
        {
            Console.WriteLine("Virheellinen syöte.", e);
            return;
        }

        Console.WriteLine($"Painoindeksi on: {weight/height/height:N2}");

        switch(weight/height/height)    //switch rakenne jolla kommentoidaan toimenpiteitä painoindeksin perusteella
        {
            case <20:
            Console.WriteLine("Suosittelen nostamaan päivittäistä kalorimäärää.");
            break;

            case >30:
            Console.WriteLine("Suosittelen liikunnan lisäämistä ja pienempää kalorimäärää jokaiselle päivälle.");
            break;

            case >25:
            Console.WriteLine("Suosittelen vähentämään päivittäistä kalorimäärää.");
            break;

            default:
            Console.WriteLine("Hyvä. Jatka samaan malliin.");
            break;
        }
    }
}