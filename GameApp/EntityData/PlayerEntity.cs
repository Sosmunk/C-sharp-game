using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Sample_Text
{
    public class PlayerEntity : Entity
    {
        public bool shootingLeft, shootingRight, shootingUp, shootingDown;
        public int shootCooldown;
        private readonly int ShootingSpeed;
        public int InvulnerabilityFrames { get; private set; }
        public Vector PreviousPosition;
        public int PrevPositionCounter;
        public PlayerEntity(int posX, int posY, int speed) : base(posX, posY, speed)
        {
            Hitbox = 70;
            Health = 228;
            ShootingSpeed = 50;
            shootCooldown = ShootingSpeed;
            InvulnerabilityFrames = 10;
            PreviousPosition = new Vector(posX, posY);
            PrevPositionCounter = 25;
        }

        public override void Move()
        {
            PrevPositionCounter--;
            var direction = NormalizeMovement();
            var newPos = Position + (direction * Speed);
            if (PrevPositionCounter == 0)
            {
                PreviousPosition = Position;
                PrevPositionCounter = 25;
            }
            if (newPos.X < 0 || newPos.X > 1440)
            {
                Position += new Vector(Position.X - newPos.X, direction.Y * Speed);
                return;
            }
            if (newPos.Y < 0 || newPos.Y > 1024)
            {
                Position += new Vector(direction.X * Speed, Position.Y - newPos.Y);
                return;
            }
            if (newPos.X < 0 || newPos.Y < 0 || newPos.X > 1440 || newPos.Y > 1024)
                return;
            if (InvulnerabilityFrames > 0)
            {
                Position += direction * (Speed * 3 / 2);
                return;
            }
            Position += direction * Speed;
            
        }

        public override void TakeDamage(int value)
        {
            Health -= value;
            if (Health <= 0)
            {
                IsAlive = false;
            }
            InvulnerabilityFrames = 50;
        }
        public void UpdateInvulnerabilityFrames()
        {
            if (InvulnerabilityFrames > 0)
                InvulnerabilityFrames--;
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
