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
        public enum LookDirection
        {
            Left,
            Right,
        }
        public Vector Position { get; set; }
        public int Hitbox;
        public bool movingUp, movingDown, movingRight, movingLeft;
        public int speed;
        public bool IsAlive;
        public int Health;
        public LookDirection lookDirection;

        public Entity(int posX, int posY, int speed)
        {
            Position = new Vector(posX, posY);
            this.speed = speed;
            IsAlive = true;
            lookDirection = LookDirection.Right;
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
            var newPos = Position + (direction * speed);
            if (newPos.X < 0 || newPos.X > 1440) 
            {
                Position += new Vector(Position.X - newPos.X, direction.Y * speed);
                return;
            }
            if (newPos.Y < 0 || newPos.Y > 1024)
            {
                Position += new Vector(direction.X * speed, Position.Y - newPos.Y);
                return;
            }
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
        public Rectangle GetHitBoxRectangle()
        {
            return new Rectangle((int)Position.X - Hitbox / 2, (int)Position.Y - Hitbox / 2, Hitbox,Hitbox);
        }
        public Rectangle GetHitBoxRectangle(int posX, int posY)
        {
            return new Rectangle(posX - Hitbox / 2, posY - Hitbox / 2, Hitbox, Hitbox);
        }

    }
}
