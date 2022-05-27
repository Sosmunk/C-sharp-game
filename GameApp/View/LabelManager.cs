using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sample_Text;
using static System.Windows.Forms.Control;
using System.Drawing;

namespace Sample_Text.View
{
    public class LabelManager
    {

        private readonly ControlCollection Controls;
        private readonly Form Form;
        public Label KillCount;
        public LabelManager(ControlCollection controls, Form form)
        {
            Form = form;
            Controls = controls;
            KillCount = new Label()
            {
                Text = "Kills: ",
                Location = new Point(1200, 10),
                Size = new Size(500, 40),
                Font = new Font("Arial", 20, FontStyle.Regular)
            };
        }

        public void ChangeKillCount(int killCount)
        {
            KillCount.Text = "Kills:   " + killCount.ToString();
        }
        public void CreateGameOverLabels()
        {
            var gameOverLabel = new Label
            {
                Text = "Game Over",
                Font = new Font("Arial", 32, FontStyle.Bold),
                Size = new Size(Form.Size.Width / 3, Form.Size.Height / 8),
                Location = new Point(Form.Size.Width / 3, Form.Size.Height / 4),
                TextAlign = ContentAlignment.MiddleCenter,
            };
            Controls.Add(gameOverLabel);
            var restartLabel = new Label
            {
                Text = "Press ENTER to restart",
                Font = new Font("Arial", 32, FontStyle.Bold),
                Size = new Size(Form.Size.Width / 3, Form.Size.Height / 8 + 50),
                Location = new Point(Form.Size.Width / 3, Form.Size.Height / 4 + 200),
                TextAlign = ContentAlignment.MiddleCenter,
            };
            Controls.Add(restartLabel);
        }
        
    }
}
