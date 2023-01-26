using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_klmpk9
{
    public class Blok_T : Blok
    {
        private readonly Posisi[][] tiles = new Posisi[][]
        {
            new Posisi[] {
                new(0,1),
                new(1,0),
                new(1,1),
                new(1,2)},
            new Posisi[] {
                new(0,1),
                new(1,1),
                new(1,2),
                new(2,1)},
            new Posisi[] {
                new(1,0),
                new(1,1),
                new(1,2),
                new(2,1)},
            new Posisi[] {
                new(0,1),
                new(1,0),
                new(1,1),
                new(2,1)},
        };
        public override int Id => 6;
        protected override Posisi Mulaioffset => new Posisi(0, 3);
        protected override Posisi[][] Tiles => tiles;
    }
}
