using System.Collections.Generic;
using System.Windows.Forms;

namespace Sample_Text.Controller
{
    public class GameplayInputManager
    {
        private readonly PlayerEntity Player;
        public GameplayInputManager(PlayerEntity player)
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
                    Player.shootingDown = false;
                    Player.shootingLeft = false;
                    Player.shootingRight = false;
                    break;
                case Keys.Down:
                    Player.shootingDown = true;
                    Player.shootingUp = false;
                    Player.shootingLeft = false;
                    Player.shootingRight = false;
                    break;
                case Keys.Left:
                    Player.shootingLeft = true;
                    Player.shootingUp = false;
                    Player.shootingDown = false;
                    Player.shootingRight = false;
                    break;
                case Keys.Right:
                    Player.shootingRight = true;
                    Player.shootingUp = false;
                    Player.shootingDown = false;
                    Player.shootingLeft = false;
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
