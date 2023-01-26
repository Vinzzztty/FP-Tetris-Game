using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_klmpk9
{
    public class Blok_O : Blok
    {
        private readonly Posisi[][] tiles = new Posisi[][]
        {
            new Posisi[] { new(0,0), new(0,1), new(1,0), new(1,1)},
        };
        public override int Id => 4;
        protected override Posisi Mulaioffset => new Posisi(0, 4);
        protected override Posisi[][] Tiles => tiles;
    }
}
