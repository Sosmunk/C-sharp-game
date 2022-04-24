using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Entity player;
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 10;
            timer1.Tick += new EventHandler(Update);
            KeyDown += new KeyEventHandler(OnPress);
            KeyUp += new KeyEventHandler(OnRelease);
            Init();
        }

        public void OnPress(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    player.movingUp = true;
                    break;
                case Keys.S:
                    player.movingDown = true;
                    break;
                case Keys.D:
                    player.movingRight = true;
                    break;
                case Keys.A:
                    player.movingLeft = true;
                    break;
            }
        }
        public void OnRelease(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    player.movingUp = false;
                    break;
                case Keys.S:
                    player.movingDown = false;
                    break;
                case Keys.D:
                    player.movingRight = false;
                    break;
                case Keys.A:
                    player.movingLeft = false;
                    break;
            }
        }
        public void Init()
        {
            player = new Entity(50,50,1,1);
            timer1.Start();
        }

        public void Update(object sender, EventArgs e)
        {
            player.DirectionalMove();
            Invalidate();
        }
        private void Player_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var playerModel = new Rectangle(player.posX, player.posY, 50, 50);
            
            g.DrawRectangle(new Pen(Color.Red, 10), playerModel);
            g.FillRectangle(new SolidBrush(Color.Red), playerModel);
            
        }
        private void UI_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var healthBar = new Rectangle(0, 20, Player.Health * 3, 10);
            g.DrawRectangle(new Pen(Color.Green, 10), healthBar);
        }
    }
}
