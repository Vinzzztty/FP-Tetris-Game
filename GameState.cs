using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_klmpk9
{
    public class GameState
    {
        public Blok saatiniBlok;
        public Blok SaatiniBlok
        {
            get => saatiniBlok;
            private set
            {
                saatiniBlok = value;
                saatiniBlok.Reset();
            }
        }

        public GameGrid GameGrid { get; }
        public BlokAntrian BlokAntrian { get; }
        public bool GameOver { get; set; }
        public int Score { get; private set; }
        public Blok DiTahanBlok { get; private set; }
        public bool Ditahan { get; private set; }
        public int ScoreHigh { get; private set; }
        public GameState()
        {
            GameGrid = new GameGrid(22, 10);
            BlokAntrian = new BlokAntrian();
            SaatiniBlok = BlokAntrian.Perbarui();
            Ditahan = true;
        }

        public bool FitBlok()
        {
            foreach (Posisi p in SaatiniBlok.TilePosisi())
            {
                if (!GameGrid.Kosong(p.Baris, p.Kolom))
                {
                    return false;
                }
            }
            return true;
        }

        public void TahanBlok()
        {
            if (!Ditahan)
            {
                return;
            }

            if (DiTahanBlok == null)
            {
                DiTahanBlok = SaatiniBlok;
                SaatiniBlok = BlokAntrian.Perbarui();
            }
            else
            {
                Blok temp = SaatiniBlok;
                SaatiniBlok = DiTahanBlok;
                DiTahanBlok = temp;
            }

            Ditahan = false;
        }
        public void PutarBlokCW()
        {
            SaatiniBlok.PutarCW();
            if (!FitBlok())
            {
                SaatiniBlok.PutarCCW();
            }
        }

        public void PutarBlokCCW()
        {
            SaatiniBlok.PutarCCW();
            if (!FitBlok())
            {
                SaatiniBlok.PutarCW();
            }
        }

        public void PindahKiri()
        {
            SaatiniBlok.Pindah(0, -1);
            if (!FitBlok())
            {
                SaatiniBlok.Pindah(0, 1);
            }
        }
        public void PindahKanan()
        {
            SaatiniBlok.Pindah(0, 1);
            if (!FitBlok())
            {
                SaatiniBlok.Pindah(0, -1);
            }
        }

        private bool IsGameOver()
        {
            return !(GameGrid.BarisKosong(0) && GameGrid.BarisKosong(1));
        }

        private void TempatBlok()
        {
            foreach (Posisi p in SaatiniBlok.TilePosisi())
            {
                GameGrid[p.Baris, p.Kolom] = SaatiniBlok.Id;
            }

            Score += GameGrid.ClearBarisFull();
            ScoreHigh = 0;


            if (IsGameOver())
            {
                ScoreHigh = Score;
                GameOver = true;
            }
            else
            {
                SaatiniBlok = BlokAntrian.Perbarui();
                Ditahan = true;
            }
        }

        public void PindahBawah()
        {
            SaatiniBlok.Pindah(1, 0);
            if (!FitBlok())
            {
                SaatiniBlok.Pindah(-1, 0);
                TempatBlok();
            }
        }

        public void statepause(bool pause)
        {
            if(pause == true)
            {
                SaatiniBlok.Pindah(0, 0);
            }
            else
            {
                PindahBawah();
            }
        }

        private int TileDrop(Posisi p )
        {
            int drop = 0;
            while (GameGrid.Kosong(p.Baris + drop + 1, p.Kolom))
            {
                drop++;
            }
            return drop;
        }

        public int BlokDrop()
        {
            int drop = GameGrid.Baris;
            foreach (Posisi p in SaatiniBlok.TilePosisi())
            {
                drop = System.Math.Min(drop, TileDrop(p));
            }
            return drop;
        }

        public void DropBlok()
        {
            SaatiniBlok.Pindah(BlokDrop(), 0);
            TempatBlok();
        }
    }
}
