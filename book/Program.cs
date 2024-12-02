using System;
using System.Collections.Generic;
using System.Linq;

namespace konyv
{
    internal class Program
    {
        private static readonly Random veletlen = new Random();
        public static void Main(string[] args)
        {
            List<Book> konyvek = Konyvek(15);
            Emulacio(konyvek);
        }

        private static List<Book> Konyvek(int konyvekSzama)
        {
            List<Book> konyvek = new List<Book>();
            HashSet<long> isbnHalmaz = new HashSet<long>();
            string[] nyelvek = { "magyar", "angol" };

            for (int i = 0; i < konyvekSzama; i++)
            {
                List<Szerzo> szerzok = Szerzok(veletlen.Next(1, 4));
                string cim = Cim(nyelvek[veletlen.Next(0, nyelvek.Length)]);

                long isbn;
                do
                {
                    isbn = ISBN();
                } while (isbnHalmaz.Contains(isbn));

                isbnHalmaz.Add(isbn);
                int keszlet = veletlen.Next(0, 100) < 30 ? 0 : veletlen.Next(5, 11);
                int ar = veletlen.Next(1000, 10000);

                konyvek.Add(new Book(isbn, szerzok, cim, nyelvek[veletlen.Next(0, nyelvek.Length)], keszlet, ar));
            }

            return konyvek;
        }


        private static List<Szerzo> Szerzok(int szam)
        {
            List<Szerzo> szerzok = new List<Szerzo>();
            string[] nevek = { "Kiss Zoltan", "Nagy Anna", "Molnar Janos", "Toth Katalin", "Szabo Gabor" };

            for (int i = 0; i < szam; i++)
            {
                string nev = nevek[veletlen.Next(0, nevek.Length)];
                szerzok.Add(new Szerzo(nev));
            }

            return szerzok;
        }

        private static string Cim(string nyelv)
        {
            string[] cimkMagyar = { "A kisherceg", "A nagy utazas", "A titkos kert" };
            string[] cimkAngol = { "The Little Prince", "The Big Journey", "The Secret Garden" };

            return nyelv == "magyar"
                ? cimkMagyar[veletlen.Next(0,cimkMagyar.Length)]
                : cimkAngol[veletlen.Next(0,cimkAngol.Length)];
        }

        private static long ISBN()
        {
            long isbn = veletlen.Next(100000000, 999999999);
            return isbn * 10 + veletlen.Next(0, 10);
        }


        private static void Emulacio(List<Book> konyvek)
        {
            decimal osszesBevetel = 0;
            int kifogyottKonyvek = 0;
            int kezdoKeszlet = konyvek.Sum(k => k.Keszlet);
            int aktualisKeszlet = kezdoKeszlet;

            for (int i = 0; i < 100; i++)
            {

                Book keresettKonyv = konyvek[veletlen.Next(0, konyvek.Count)];

                if (keresettKonyv.Keszlet > 0)
                {
                    osszesBevetel += keresettKonyv.Ar;
                    keresettKonyv.Keszlet --;
                    aktualisKeszlet --;
                }
                else
                {
                    if (veletlen.Next(0, 100)<50)
                    {
                        int ujKeszlet = veletlen.Next(1,11);
                        keresettKonyv.Keszlet += ujKeszlet;
                        aktualisKeszlet += ujKeszlet;
                    }
                    else
                    {
                        konyvek.Remove(keresettKonyv);
                        kifogyottKonyvek ++;
                    }
                }
            }

            Console.WriteLine($"Osszes bevetel: {osszesBevetel} Ft");
            Console.WriteLine($"Kifogyott konyvek a nagykerbol: {kifogyottKonyvek}");
            Console.WriteLine($"Kezdo keszlet: {kezdoKeszlet}, Aktualis keszlet: {aktualisKeszlet}, Kulonbseg: {aktualisKeszlet - kezdoKeszlet}");
        }
    }
}