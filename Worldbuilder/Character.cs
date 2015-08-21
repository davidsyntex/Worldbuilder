using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Worldbuilder
{
    class Character
    {
        public string Förnamn { get; private set; }
        public string Dynasti { get; private set; }

        public Character()
        {
            Förnamn = "anders";
            Dynasti = "knutson";
        }

        public string PrintName()
        {
            return Förnamn + " " + Dynasti;
        }
    }
}
