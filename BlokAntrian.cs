using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Tetris_klmpk9
{
    public class BlokAntrian
    {
        private readonly Blok[] bloks = new Blok[]
        {
            new Blok_I(),
            new Blok_J(),
            new Blok_L(),
            new Blok_O(),
            new Blok_S(),
            new Blok_T(),
            new Blok_Z()
        };

        private readonly Random acak = new Random();
        public Blok NextBlok { get; private set; }

        public BlokAntrian()
        {
            NextBlok = RandomBlok();
        }

        private Blok RandomBlok()
        {
            return bloks[acak.Next(bloks.Length)];
        }

        public Blok Perbarui()
        {
            Blok blok = NextBlok;
            do
            {
                NextBlok= RandomBlok();
            }
            while (blok.Id == NextBlok.Id);

            return blok;

        }
    }
}
