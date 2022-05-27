using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample_Text
{
    public class Bullet : Entity
    {
        public int Damage = 50;
        public Bullet(int posX, int posY, int speed, int damage) : base(posX, posY, speed)
        {
            Hitbox = 15;
            Health = 100;
            Damage = damage;
            
        }
        public override void Move()
        {
            var direction = NormalizeMovement();
            var newPos = Position + direction * Speed;
            if (newPos.X < 0 || newPos.Y < 0 || newPos.X > 1440 || newPos.Y > 1024 || Health <= 0 )
            {
                IsAlive = false;
                return;
            }
            Health -= 1;   
            Position += direction * Speed;
        }



    }
}
