/*
T5) Kehitäpä painoindeksi-ohjelmaa (T4) niin, että tiedot kysytään
tietueeseen, jossa on nimi, pituus, paino sekä paikka painoindeksille,
joka lasketaan ohjelmassa. Laskemisen jälkeen tulostetaan tietueen kaikki
tiedot. (5 p)
*/


using System;

class Program
{
    public struct Henkilotiedot
    {
        public string nimi;
        public double pituus, paino, painoindeksi;
        public void KysyNimi()
        {
            try
            {
                Console.Write("Nimi: ");
                nimi = Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Virheellinen syöte.", e);
                return;
            }
        }

        public void KysyPituus()
        {   
            try
            {
                Console.Write("Pituus metreinä: ");
                pituus = double.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("Virheellinen syöte.", e);
                return;
            }
        }

        public void KysyPaino()
        {
            try
            {
                Console.Write("Paino kilogrammoina: ");
                paino = double.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("Virheellinen syöte.", e);
                return;
            }
        }
        
        public void Indeksinlasku(double paino, double pituus)
        {
            painoindeksi = paino / pituus / pituus;
        }
    }


    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        Henkilotiedot hlo1 = new Henkilotiedot();           //luodaan uusi tietue hlo1 käyttäen Henkilotiedot
        hlo1.KysyNimi();
        hlo1.KysyPituus();                                  //kysytään henkilön tiedot tietueessa määriteltyjä metodeja käyttäen
        hlo1.KysyPaino();
        hlo1.Indeksinlasku(hlo1.paino,hlo1.pituus);
        Console.Write($"Henkilön nimi: {hlo1.nimi}\nPituus: {hlo1.pituus}\nPaino: {hlo1.paino}\nPainoindeksi: {hlo1.painoindeksi:2N}\n");       //tulostetaan henkilön tiedot tietueesta
        switch (hlo1.painoindeksi)
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