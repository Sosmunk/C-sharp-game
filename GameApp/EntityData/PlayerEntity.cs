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
        public bool shootingLeft, shootingRight, shootingUp, shootingDown;
        public int shootCooldown;
        private int ShootingSpeed = 50;
        public PlayerEntity(int posX, int posY, int speed) : base(posX, posY, speed)
        {
            Hitbox = 70;
            Health = 228;
            shootCooldown = ShootingSpeed;
        }
        public void Shoot(HashSet<Bullet> bullets)
        {
            shootCooldown--;
            if (shootCooldown > 0) return;
            if (!shootingDown && !shootingRight && !shootingUp && !shootingLeft) return;
            var bullet = new Bullet((int)Position.X, (int)Position.Y, 10,50);
            
            if (shootingLeft)
            {
                bullet.movingLeft = true;
            }
            if (shootingRight)
            {
                bullet.movingRight = true;

            }
            if (shootingUp)
            {
                bullet.movingUp = true;
            }
            if (shootingDown)
            {
                bullet.movingDown = true;
            }
            shootCooldown = ShootingSpeed;
            bullets.Add(bullet);

        }
    }
}
