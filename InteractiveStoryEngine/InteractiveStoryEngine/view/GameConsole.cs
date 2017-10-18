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
            this.textBox.AppendText(FormatString(storyString));
            this.textBox.ScrollToEnd();
        }

        public static string FormatString(string s)
        {
            if (s == null) return null;
            s.TrimEnd(new char[] { '\r', '\n' });
            return s + Environment.NewLine + Environment.NewLine;
        }
    }
}
