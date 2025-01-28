/*
T6) Kehitä edellistä ohjelmaa niin, että se sisältää taulukon kolmelle
henkilölle. Kysy ohjelmassa ensin henkilön tiedot (nimi, paino, pituus)
ja sen jälkeen laske kunkin painoindeksi ja lopuksi tulosta tiedot.(5 p)
*/

using System;

class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        dynamic[,] henkilo = new dynamic[3, 3];         //luodaan taulukko kolmelle henkilölle. koska taulukkoon menee niin tekstiä kuin desimaalilukuja, käytämme dynamic kutsua taulukkoa luodessa
        for (int i = 0; i <= 2; i++)
        {
            for (int j = 0; j <= 2; j++)                //kaksi sisäkkäistä for silmukkaa joilla täytämme taulukon arvot
            {
                switch (j)
                {
                    case 0:
                    try
                    {
                        Console.Write("Nimi: ");
                        henkilo[i,j] = Console.ReadLine();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Virheellinen syöte.", e);
                        return;
                    }
                    break;

                    case 1:
                    try
                    {
                        Console.Write("Pituus metreinä: ");
                        henkilo[i,j] = double.Parse(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Virheellinen syöte.", e);
                        return;
                    }
                    break;

                    case 2:
                    try
                    {
                        Console.Write("Paino kilogrammoina: ");
                        henkilo[i,j] = double.Parse(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Virheellinen syöte.", e);
                        return;
                    }
                    break;
                }
            }
        }

        for (int i = 0; i <= 2; i++)
        {
            for (int j = 0; j <= 2; j++)            //tulostusta varten uusi sisäistetty for silmukka
            {
                switch (j)
                {
                    case 0:
                    Console.WriteLine($"Henkilön nimi on {henkilo[i, j]}");
                    break;

                    case 1:
                    Console.WriteLine($"Pituus on {henkilo[i, j]}");
                    break;
                    
                    case 2:
                    Console.WriteLine($"Paino on {henkilo[i, j]}");
                    Console.WriteLine($"Painoindeksi on {henkilo[i, 2] / henkilo[i, 1] /henkilo[i, 1]:N2}");
                    break;
                }
            }
        }
    }
}