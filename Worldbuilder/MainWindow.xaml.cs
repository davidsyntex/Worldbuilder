using System;
using System.Linq;
using System.Text;
using System.Windows;

namespace Worldbuilder
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            _world = new World(new WorldDate(1,1,0));
            InitializeComponent();
            TextboxOutput.Clear();
        }

        private readonly World _world;

        private void ButtonCreateWorldClicked(object sender, RoutedEventArgs e)
        {
            _world.StartWorld(new WorldDate(400, 4, 99));
            TextboxOutput.Clear();
            TextboxOutput.Text += "ÅR.SäsongDag" + Environment.NewLine;

            OutputSimulation();
        }

        private void OutputSimulation()
        {
            var sb = new StringBuilder();
            sb.Append("Simulation completed on: " +
                      _world.CurrentWorldDate.FormattedDate() +
                      Environment.NewLine);

            sb.Append(OutputCharacters());

            TextboxOutput.Text += sb.ToString();
        }

        private string OutputCharacters()
        {
            var sb = new StringBuilder();

            foreach (var character in _world.GetAllCharacters().Where(character => character.IsBorn(_world.CurrentWorldDate)).ToList())
            {
                sb.Append(character.PrintName());
                sb.Append(Environment.NewLine);
                sb.Append(String.Format("\tBorn: {0}", character.BornDate.FormattedDate()));

                if (character.IsAlive(_world.CurrentWorldDate))
                {
                    sb.Append(" is ");
                    sb.Append(character.Age(_world.CurrentWorldDate));
                    sb.Append(" years old.");
                }
                else
                {
                    sb.Append(Environment.NewLine);
                    sb.Append(String.Format("\tDied: {0} at age of: {1} years old.", character.DiedDate.FormattedDate(), character.Age(_world.CurrentWorldDate)));
                }


                sb.Append(Environment.NewLine);
                foreach (var attribute in character.FetchModifiedAttributes())
                {
                    sb.AppendFormat("\t{0}: {1}{2}", attribute.Key, attribute.Value.Value, Environment.NewLine);
                }

                foreach (var t in character.Traits)
                {
                    sb.AppendLine("\t" + t.Name);

                    foreach (var mod in t.TraitModifiers)
                        {
                            sb.Append("\t\t" + mod.TargetName + ": +" + mod.ModifierValue + Environment.NewLine);
                        }
                    
                }


                sb.Append(Environment.NewLine);
            }


            return sb.ToString();
        }
    }
}