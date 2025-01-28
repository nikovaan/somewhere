/*
T7) Lisäys edelliseen. Kun tiedot on kysytty ja indeksit laskettu,
ohjelma ei tulosta kaikkia tietoja, vaan kysyy käyttäjältä henkilön nimeä
ja näyttää sen jälkeen ko. henkilön tiedot. Henkilön nimeä kysytään aina
uudelleen, kunnes käyttäjä syöttää nimeksi ”Loppu”. (10 p)
*/

using System;
using System.Linq;

class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        dynamic[,] henkilo = new dynamic[3, 3];
        for (int i = 0; i <= 2; i++)
        {
            for (int j = 0; j <= 2; j++)
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

        string haku;

        while (true)            //while silmukka jossa jatketaan nimien kyselyä kunnes käyttäjältä tulee nimeksi "Loppu"
        {
            Console.Write("Kenen tiedot halutaan? ");
            try
            {
                haku = Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Virheellinen syöte.", e);
                return;
            }

            if (haku == "Loppu")
            {
                return;
            }

            for (int i = 0; i <= 2; i++)
            {
                if (henkilo[i, 0] == haku)          //tarkistetaan onko annettu nimi sellainen, joka on kirjattu käyttämällä if lauseketta for silmukan sisällä
                {
                    for (int j = 0; j <= 2; j++)        //tulostetaan tiedot jos löytyi tärppi
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



    }
}

//Tämä koko tulostushässäkkä oli tarkoitus rakentaa täysin erilailla mutta sitten sain selville että FindIndex ei toimi moniulotteisissa taulukoissa ja tajusin että olisi sittenkin pitänyt tehdä taulukko tietueista :)