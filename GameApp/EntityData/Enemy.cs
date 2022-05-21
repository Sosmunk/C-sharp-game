using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Sample_Text.ModelManagers;

namespace Sample_Text.EntityData
{
    public abstract class Enemy : Entity
    {
        protected Enemy(int posX, int posY, int speed) : base(posX, posY, speed)
        {
            Health = 50;
            Hitbox = 50;
        }
        public void Move(HashSet<Enemy> enemies)
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
            //var hBoxRectangle = GetHitBoxRectangle((int)newPos.X,(int)newPos.Y);
            //foreach (var enemy in enemies)
            //{
            //    if (enemy != this)
            //    {
            //        if (AreIntersected(hBoxRectangle, enemy.GetHitBoxRectangle()))
            //        {
            //            if (enemy.Position.X > newPos.X)
            //            {
            //                
            //            }
            //            return;
            //        }
            //    }
            //}

            Position = newPos;
        }
        public void SetMoveDirectionToPlayerLocation(PlayerEntity player)
        {
            if (this.Position.X - speed > player.PreviousPosition.X)
            {
                movingLeft = true;
                movingRight = false;
                lookDirection = LookDirection.Left;
            }
            else if (this.Position.X + speed < player.PreviousPosition.X)
            {
                movingRight = true;
                movingLeft = false;
                lookDirection=LookDirection.Right;
            }
            if (this.Position.Y - speed >= player.PreviousPosition.Y)
            {
                movingUp = true;
                movingDown = false;
            }
            else if (this.Position.Y + speed <= player.PreviousPosition.Y)
            {
                movingDown = true;
                movingUp = false;
            }
            if (Math.Abs(player.PreviousPosition.X - Position.X) < speed)
            {
                Position = new System.Windows.Vector(player.PreviousPosition.X, Position.Y);
            }
            if (Math.Abs(player.PreviousPosition.Y - Position.Y) < speed)
            {
                Position = new System.Windows.Vector(Position.X, player.PreviousPosition.Y);
            }
        }
        public static bool AreIntersected(Rectangle r1, Rectangle r2)
        {
            // Прямоугольники не пересекутся, если их противоположные вертикальные
            // и горизонтальные стороны не будут проходить друг через друга
            if ((r1.Top > r2.Bottom) || (r2.Top > r1.Bottom))
            {
                return false;
            }
            if ((r1.Right < r2.Left) || (r2.Right < r1.Left))
            {
                return false;
            }
            return true;
        }
    }
}
