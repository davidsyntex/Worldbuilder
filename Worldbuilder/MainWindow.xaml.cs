using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.TextFormatting;

namespace Worldbuilder
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Character> _listOfCharacters;
        private readonly Random _random;

        public MainWindow()
        {
            InitializeComponent();
            textboxOutput.Clear();
            _random = new Random();
        }

        private void ButtonCreateWorldClicked(object sender, RoutedEventArgs e)
        {
            PopulateCharacterList();
            RunWorld();
        }

        private void PopulateCharacterList()
        {
            _listOfCharacters = new List<Character>();

            for (var i = 0; i < 20; i++)
            {
                _listOfCharacters.Add(new Character(_random.Next(1, 30), _random.Next(1, 4), _random.Next(0, 99),
                    "Hasse" + (i+1)));
                //_listOfCharacters.Add(new Character(_random.Next(1, 30), _random.Next(1, 4), _random.Next(0, 99), "Hasse" + i));
            }

            foreach (var character in _listOfCharacters)
            {
                textboxOutput.Text += character.PrintName() + Environment.NewLine;
            }
        }

        private void RunWorld()
        {
            textboxOutput.Clear();
            int currentYear = 1;
            int currentSeason = 1;
            int currentDay = 0;
            var sb = new StringBuilder();

            sb.Append("ÅR.SäsongDag" + Environment.NewLine);

            while (IsHalfOfCharactersAlive() || currentYear == 400)
            {
                for (currentSeason = 1; currentSeason <= 4; currentSeason++)
                {
                    for (currentDay = 0; currentDay <= 99; currentDay++)
                    {
                        foreach (
                            var character in
                                _listOfCharacters.Where(character => character.isAlive)
                                    .Where(character => character.IsBorn(currentYear, currentSeason, currentDay)))
                        {
                            if (_random.NextDouble() <= 0.000075)
                            {
                                character.Kill(currentYear, currentSeason, currentDay);

                                //sb.Append(character.PrintName() + " has died on " + FormatDate(character.DiedYear, character.DiedSeason, character.DiedDay) + Environment.NewLine);
                            }
                            if (character.Age(currentYear) == 100)
                            {
                                character.Kill(currentYear, currentSeason, currentDay);
                            }
                        }
                    }
                }
                currentYear++;
            }
            sb.Append("Simulation completed on: " + FormatDate(currentYear, currentSeason, currentDay-1) + Environment.NewLine);

            foreach (var character in _listOfCharacters.OrderBy(character => character.isAlive))
            {
                if (character.isAlive)
                {
                    sb.Append(character.PrintName() + " is alive and well, hin is " + character.Age(currentYear) +
                              " years old" + Environment.NewLine);
                }
                else
                {
                    sb.Append(character.PrintName() + " died on " +
                              FormatDate(character.DiedYear, character.DiedSeason, character.DiedDay) + " he became " +
                              character.Age(character.DiedYear) + " years old" + Environment.NewLine);
                }
            }

            textboxOutput.Text += sb.ToString();
        }

        private bool IsHalfOfCharactersAlive()
        {
            var numberOfAlive = _listOfCharacters.Count(character => character.isAlive);
            return numberOfAlive >= _listOfCharacters.Count / 2;
        }

        private static string FormatDate(int year, int season, int day)
        {
            return year + "." + season + PaddedZeroes(day);
        }

        private static string PaddedZeroes(int day)
        {
            var paddedDag = day.ToString();
            if (day < 10)
            {
                paddedDag = paddedDag.Insert(0, "0");
            }

            return paddedDag;
        }
    }
}