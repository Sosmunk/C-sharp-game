using System.Collections.Generic;
using System.Windows.Forms;

namespace Sample_Text.Controller
{
    public class InputManager
    {
        PlayerEntity Player;
        public InputManager(PlayerEntity player)
        {
            this.Player = player;
        }
        public void MoveOnPress(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    Player.movingUp = true;
                    break;
                case Keys.S:
                    Player.movingDown = true;
                    break;
                case Keys.D:
                    Player.movingRight = true;
                    Player.lookDirection = Entity.LookDirection.Right;
                    break;
                case Keys.A:
                    Player.movingLeft = true;
                    Player.lookDirection = Entity.LookDirection.Left;
                    break;
            }
        }
        public void StopMovingOnRelease(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    Player.movingUp = false;
                    break;
                case Keys.S:
                    Player.movingDown = false;
                    break;
                case Keys.D:
                    Player.movingRight = false;
                    break;
                case Keys.A:
                    Player.movingLeft = false;
                    break;
            }
        }
        public void Shoot(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    Player.shootingUp = true;
                    break;
                case Keys.Down:
                    Player.shootingDown = true;
                    break;
                case Keys.Left:
                    Player.shootingLeft = true;
                    break;
                case Keys.Right:
                    Player.shootingRight = true;
                    break;
            }
        }
        public void ReleaseShooting(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    Player.shootingUp = false;
                    break;
                case Keys.Down:
                    Player.shootingDown = false;
                    break;
                case Keys.Left:
                    Player.shootingLeft = false;
                    break;
                case Keys.Right:
                    Player.shootingRight = false;
                    break;
            }
        }
    }
}
