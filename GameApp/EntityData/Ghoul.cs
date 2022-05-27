using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Text.EntityData
{
    public class Ghoul : Enemy
    {
        public Ghoul(int posX, int posY, int speed) : base(posX, posY, speed)
        {
            Hitbox = 100;
            Health = 50;
            Damage = 30;
        }
    }
}
