using System.Collections.Generic;

namespace konyv
{
    public class Book
    {
        public long ISBN { get; }
        public List<Szerzo> Szerzok { get; }
        public string Cim { get; }
        public string Nyelv { get; }
        public int Keszlet { get; set; }
        public int Ar { get; }


        public Book(long isbn, List<Szerzo> szerzok, string cim, string nyelv, int keszlet, int ar)
        {
            ISBN = isbn;
            Szerzok = szerzok;
            Cim = cim;
            Nyelv = nyelv;
            Keszlet = keszlet;
            Ar = ar;
        }
    }

    public class Szerzo
    {
        public string Nev { get; }

        public Szerzo(string nev)
        {
            Nev = nev;
        }
    }
}