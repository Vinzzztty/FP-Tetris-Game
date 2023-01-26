using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_klmpk9
{
    public class Blok_Z : Blok
    {
        private readonly Posisi[][] tiles = new Posisi[][]
        {
            new Posisi[] {
                new(0,0),
                new(0,1),
                new(1,1),
                new(1,2)},
            new Posisi[] {
                new(0,2),
                new(1,1),
                new(1,2),
                new(2,1)},
            new Posisi[] {
                new(1,0),
                new(1,1),
                new(2,1),
                new(2,2)},
            new Posisi[] {
                new(0,1),
                new(1,0),
                new(1,1),
                new(2,0)},
        };
        public override int Id => 7;
        protected override Posisi Mulaioffset => new Posisi(0, 3);
        protected override Posisi[][] Tiles => tiles;
    }
}
