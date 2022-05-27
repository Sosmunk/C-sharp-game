using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample_Text.Controller
{
    public class MenuInputManager
    {
        private readonly Sample_Text gameForm;

        public MenuInputManager(Sample_Text form)
        {
            gameForm = form;
        }
        public void PressEnterToRestart(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                gameForm.StartGame();
            }
        }

    }
}
