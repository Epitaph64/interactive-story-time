using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStoryEngine.view
{
    class GameConsole
    {
        private System.Windows.Controls.TextBox textBox;

        // constructor
        public GameConsole(ref System.Windows.Controls.TextBox textBox)
        {
            this.textBox = textBox;
        }

        public void Write(string storyString)
        {
            if (storyString == null) return;
            storyString = storyString.TrimEnd(Environment.NewLine.ToCharArray());
            this.textBox.AppendText(storyString + Environment.NewLine + Environment.NewLine);
            this.textBox.ScrollToEnd();
        }
    }
}
