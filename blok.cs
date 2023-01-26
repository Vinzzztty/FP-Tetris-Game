using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_klmpk9
{
    public abstract class Blok
    {
        protected abstract Posisi[][] Tiles { get; }
        protected abstract Posisi Mulaioffset { get; }
        public abstract int Id { get; }

        private int Perputaran;
        private Posisi  offset;

        public Blok()
        {
            offset = new Posisi(Mulaioffset.Baris, Mulaioffset.Kolom);
        }

        public IEnumerable<Posisi> TilePosisi()
        {
            foreach (Posisi p in Tiles[Perputaran])
            {
                yield return new Posisi(p.Baris + offset.Baris, p.Kolom + offset.Kolom);
            }
        }

        public void PutarCW()
        {
            Perputaran = (Perputaran+ 1) % Tiles.Length;
        }

        public void PutarCCW()
        {
            if (Perputaran == 0) 
            {
                Perputaran = Tiles.Length - 1;

            }
            else
            {
                Perputaran--;
            }    
        }

        public void Pindah(int baris, int kolom)
        {
            offset.Baris += baris;
            offset.Kolom += kolom;
        }

        public void Reset()
        {
            Perputaran = 0;
            offset.Baris = Mulaioffset.Baris;
            offset.Kolom = Mulaioffset.Kolom;
        }
    }
}
