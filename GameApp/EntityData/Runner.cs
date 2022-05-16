using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Text.EntityData
{
    public class Runner : Entity
    {
        public Runner(int posX, int posY, int speed) : base(posX, posY, speed)
        {
            Hitbox = 50;
            Health = 50;
        }
        public void FindPlayer(PlayerEntity player)
        {
            if (this.Position.X >= player.Position.X)
            {
                movingLeft = true;
                movingRight = false;
            }
            else if (this.Position.X < player.Position.X)
            {
                movingRight = true;
                movingLeft = false;
            }
            if (this.Position.Y >= player.Position.Y)
            {
                movingUp = true;
                movingDown = false;
            }
            else if (this.Position.Y <= player.Position.Y)
            {
                movingDown = true;
                movingUp = false;
            }
        }
    }
}
