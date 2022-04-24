using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WindowsFormsApp2
{
    public class Entity
    {
        public int posX;
        public int posY;
        public bool movingUp, movingDown, movingRight, movingLeft;
        public int speedX, speedY;

        public Entity(int posX, int posY, int speedX, int speedY)
        {
            this.posX = posX;
            this.posY = posY;
            this.speedX = speedX;
            this.speedY = speedY;
        }

        public void Move(int x, int y)
        {
            posX += x;
            posY += y;
        }
        public void DirectionalMove()
        {
            var direction = NormalizeMovement();
            //if (movingUp) posY -= 5;
            //if (movingDown) posY += 5;
            //if (movingLeft) posX -= 5;
            //if (movingRight) posX += 5;
            posX += (int)Math.Round(direction.X * speedX);
            posY += (int)Math.Round(direction.Y * speedY);
        }
        public Vector NormalizeMovement()
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
        
    }
}
