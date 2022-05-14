using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample_Text
{
    public class PlayerEntity : Entity
    {
        public PlayerEntity(int posX, int posY, int speed) : base(posX, posY, speed)
        {
            Hitbox = 50;
            Health = 228;
        }
    }
}
