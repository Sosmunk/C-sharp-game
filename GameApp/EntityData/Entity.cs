using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sample_Text
{
    public abstract class Entity
    {
        public Vector Position { get; set; }
        public int Hitbox;
        public bool movingUp, movingDown, movingRight, movingLeft;
        public int speed;
        public bool IsAlive;
        public int Health;

        public Entity(int posX, int posY, int speed)
        {
            Position = new Vector(posX, posY);
            this.speed = speed;
            IsAlive = true;
        }

        public virtual void TakeDamage(int value)
        {
            Health -= value;
            if (Health <= 0)
            {
                IsAlive = false;
            }
        }
        public virtual void Move()
        {
            var direction = NormalizeMovement();
            var newPos = Position + direction * speed;
            if (newPos.X < 0 || newPos.Y < 0 || newPos.X > 1440 || newPos.Y > 1024)
                return;
            Position += direction*speed;
        }
        public virtual Vector NormalizeMovement()
        {
            var vector = new Vector(0,0);
            if (movingUp) vector.Y -= 1;
            if (movingDown) vector.Y += 1;
            if (movingLeft) vector.X -= 1;
            if (movingRight) vector.X += 1;
            if (vector.X == 0 && vector.Y == 0) return vector;
            vector.Normalize();
            return vector;
        }
        public Rectangle HitBoxRectangle()
        {
            return new Rectangle((int)Position.X - Hitbox / 2, (int)Position.Y - Hitbox / 2, Hitbox,Hitbox);
        }
        
    }
}
