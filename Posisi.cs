using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_klmpk9
{
    public class Posisi
    {
        public int Baris { get; set; }
        public int Kolom { get; set; }

        public Posisi(int baris, int kolom )
        {
            Baris = baris;
            Kolom = kolom;
        }
    }
}
