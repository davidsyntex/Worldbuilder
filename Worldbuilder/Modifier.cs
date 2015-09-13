using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worldbuilder
{
    //Decoration... Wonderful
    //This base class gives you a interface to work with... Hell, it could be an interface but I decided
    //to type abstract today.
    public interface ModifierType
    {
        string ModifierName { get; }
        decimal ApplyModifier(Modifier m, decimal initialValue);
    }

    //A concrete type of ModifierType... This is what determines how the modifier value is applied.
    //This gives you more flexibility than hard coding modifier types.  If you really wanted to you could
    //serialize these and store lambda expressions in the DB so you not only have type driven logic, you could have
    //data driven behavior.
    public class FlatModifier : ModifierType
    {
        //The names can be really handy if you want to expose calculations to players.
        public string ModifierName { get { return "Flat Effect"; } }
        //And finally... let the calculation happen!  Time to bubble back up!
        public decimal ApplyModifier(Modifier m, decimal InitialValue)
        {
            return InitialValue + m.ModifierValue;
        }
    }

    public class Modifier
    {
        public Modifier(string targetName, decimal modifierValue, ModifierType modifierType)
        {
            TargetName = targetName;
            ModifierValue = modifierValue;
            ModifierType = modifierType;
        }

        public string TargetName { get; set; }
        public decimal ModifierValue { get; set; }
        //The other stuff is kind of pointless... but this is where the magic happens... All in a modifier type.
        public ModifierType ModifierType { get; set; }
        //Let the modifier apply it's own values... off the type... yea
        //I did that on purpose ;-)
        public void Apply(ICharacterAttribute a)
        {
            a.Value = ModifierType.ApplyModifier(this, a.Value);
            Console.WriteLine("applied!");
        }
    }
}
