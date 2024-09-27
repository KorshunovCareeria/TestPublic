namespace HelloWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Koodi tähän
            int kierros = 0;
            List<double> numerolista = new List<double>();
            Console.WriteLine("Syötä listalle desimaalilukuja, kunnes lista on valmis (10kpl)");

            while (true)
            {
                numerolista.Add(double.Parse(Console.ReadLine()));
                if (kierros == 9)
                {
                    break;
                }
                kierros++;
            }
            Console.WriteLine("Indeksisijainnin 3 ja 5 lukujen summa: " + numerolista[3] + " + " + numerolista[5] + " = " + (numerolista[3] + numerolista[5]));
            Console.WriteLine("Indeksisijainnin 1 ja 8 lukujen erotus: " + numerolista[1] + " - " + numerolista[8] + " = " + (numerolista[1] - numerolista[8]));
            Console.WriteLine("Indeksisijainnin 0 ja 9 lukujen tulo: " + numerolista[0] + " * " + numerolista[9] + " = " + (numerolista[0] * numerolista[9]));
            Console.WriteLine("Indeksisijainnin 2 ja 7 lukujen osamäärä: " + numerolista[2] + " / " + numerolista[7] + " = " + (numerolista[2] / numerolista[7]));
            Console.WriteLine("Listan loput luvut ovat indeksisijainnissa 4 ja 6: " + numerolista[4] + " ja " + numerolista[6]);
        }
    }
}


