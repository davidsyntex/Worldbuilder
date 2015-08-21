using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
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

namespace Worldbuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random _random;
        Character _character = new Character();

        public MainWindow()
        {
            InitializeComponent();
            _random = new Random();
        }

        private void ButtonCreateWorldClicked(object sender, RoutedEventArgs e)
        {
            textboxOutput.Text = _character.PrintName() + Environment.NewLine;
            RunWorld();
        }

        async void foo()
        {
            // something
            await Task.Delay(50);
        }

        private void RunWorld()
        {
            var isAlive = true;
            var år = 1;
            var sb = new StringBuilder();

            sb.Append("ÅR.SäsongDag" + Environment.NewLine);

            while (isAlive)
            {
                for (var säsong = 1; säsong <= 4; säsong++)
                {
                    for (var dag = 0; dag <= 99; dag++)
                    {
                        sb.Append("en ny dag har börjat: ");
                        sb.Append(år.ToString() + "." + säsong.ToString() + PaddedZeroes(dag, säsong) + Environment.NewLine);
                        

                        if (!(_random.NextDouble() <= 0.0025))
                        {
                            continue;
                        }

                        sb.Append(_character.PrintName() + " has died!" + Environment.NewLine + Environment.NewLine);
                        isAlive = false;
                        break;
                    }
                    if (!isAlive)
                    {
                        break;
                    }
                }
                år++;
            }

            textboxOutput.Text += sb.ToString();
        }

        private string PaddedZeroes(int dag, int säsong)
        {
            var paddedDag = dag.ToString();
            if (dag < 10)
            {
                paddedDag = paddedDag.Insert(0, "0");
            }
            
            return paddedDag;
        }
    }
}
