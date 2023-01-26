using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Tetris_klmpk9
{
    public class GameGrid
    {
        private readonly int[,] grid;
        public int Baris { get; }
        public int Kolom { get; }
        public int this[int b, int k]
        {
            get => grid[b, k];
            set => grid[b, k] = value;
        }
        public GameGrid(int baris, int kolom)
        {
            Baris = baris;
            Kolom = kolom;
            grid = new int[baris, kolom];
        }
        public bool DiDalam(int b, int k)
        {
            return b >= 0 && b < Baris && k >= 0 && k < Kolom;
        }
        public bool Kosong(int b, int k)
        {
            return DiDalam(b, k) && grid[b, k] == 0;
        }

        public bool BarisFull(int b)
        {
            for (int k = 0; k < Kolom; k++)
            {
                if (grid[b, k] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool BarisKosong(int b)
        {
            for (int k = 0; k < Kolom; k++)
            {
                if (grid[b, k] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void ClearBaris(int b)
        {
            for (int k = 0;k < Kolom; k++ )
            {
                grid[b, k] = 0;
            }
        }
        private void BarisKeBawah(int b, int noBaris)
        {
            for (int k = 0; k < Kolom; k++)
            {
                grid[b + noBaris, k] = grid[b, k];
                grid[b, k] = 0;
            }
        }
        public int ClearBarisFull()
        {
            int Bersihkan = 0;
            for (int b = Baris - 1; b >= 0; b--)
            {
                if (BarisFull(b))
                {
                    ClearBaris(b);
                    Bersihkan++;
                }
                else if (Bersihkan > 0)
                {
                    BarisKeBawah(b, Bersihkan);
                }
            }
            return Bersihkan;
            
        }
     }
}
