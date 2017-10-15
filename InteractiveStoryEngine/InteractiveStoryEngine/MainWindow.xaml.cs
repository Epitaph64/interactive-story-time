using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InteractiveStoryEngine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        view.GameConsole gc;
        view.GameView gv;

        public MainWindow()
        {
            InitializeComponent();

            this.Title = "Interactive Story Viewer";
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            gc = new view.GameConsole(ref textBoxGameStatus);
            gv = new view.GameView(gc, ButtonGrid);

            // add listener for key presses
            this.PreviewKeyDown += new KeyEventHandler(HandleKeys);
        }

        // KEYPRESSES
        private void HandleKeys(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void clickFileOpen(Object sender, RoutedEventArgs args)
        {
            
        }

        private void clickFileAbout(Object sender, RoutedEventArgs args)
        {
            gc.Write("Interactive Story Viewer - Version 1.0\n" +
                "This software was created by Walter Macfarland using C#/WPF\n" +
                "and using the YAML reader found at https://github.com/aaubry/YamlDotNet\n");
        }
    }
}
