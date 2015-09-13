using System;
using System.Collections.Generic;
using System.Linq;

namespace Worldbuilder
{
    public class Character
    {
        public enum CharacterGender
        {
            Male,
            Female
        }

        private Dictionary<string, ICharacterAttribute> _modifiedAttributes;
        private Dictionary<string, ICharacterAttribute> _rawAttributes;

        public Character(WorldDate currentWorldDate, string firstname, string dynasty, CharacterGender characterGender)
        {
            Firstname = firstname;
            Dynasty = dynasty;
            BornDate = new WorldDate(currentWorldDate.Year, currentWorldDate.Season, currentWorldDate.Day);
            var gender = characterGender;
            _rawAttributes = new Dictionary<string, ICharacterAttribute>();
            _modifiedAttributes = new Dictionary<string, ICharacterAttribute>();
            Traits = new List<Trait>();
            AddAttribute(new ValueCharacterAttribute("Fertility", 0.5m));
            AddAttribute(new ValueCharacterAttribute("Combat", 1m));
            AddAttribute(new ValueCharacterAttribute("Strength", 10));


            var mods = new List<Modifier>()
            {
                new Modifier("Fertility",0.5m, new FlatModifier())
            };

            var kuk = new Trait("Järnringsvigör", mods);

            if (Dynasty == "Järnring")
            {
                AddTrait(kuk);
            }

            mods = new List<Modifier>()
            {
                new Modifier("Combat", 8m,new FlatModifier()),
                new Modifier("Strength", 4m,new FlatModifier())
            };

            kuk = new Trait("Dunhallskrigare", mods);
            if (Dynasty == "Dunhall")
            {
                AddTrait(kuk);
            }

            _modifiedAttributes = FetchModifiedAttributes();

            foreach (var modifiedAttribute in _modifiedAttributes)
            {
                Console.WriteLine(firstname + " " + dynasty + " " + modifiedAttribute.Key + ": " + modifiedAttribute.Value.Value);
            }

            Console.WriteLine(Traits.Count);
        }

        public string Firstname { get; private set; }
        public string Dynasty { get; private set; }
        public WorldDate BornDate { get; private set; }
        public WorldDate DiedDate { get; private set; }
        public Character Father { get; private set; }
        public Character Mother { get; private set; }
        public List<Character> Children { get; private set; }
        public List<Trait> Traits { get; private set; }
        private bool AreModifiedAttributesCurrent { get; set; }

        public void AddAttribute(ICharacterAttribute x)
        {
            _rawAttributes.Add(x.Name, x);
        }

        public void AddTrait(Trait t)
        {
            Traits.Add(t);
            AreModifiedAttributesCurrent = false;
        }

        //Finally you want a way to fetch the modified attribs
        //Keep in mind you need to do the copy dance in the  apply to not upset your 
        //base stats.
        public Dictionary<string, ICharacterAttribute> FetchModifiedAttributes()
        {
            var traceAttribs = _rawAttributes;

            if (!AreModifiedAttributesCurrent)
            {
                foreach (var t in Traits)
                {
                    traceAttribs = t.ApplyModifiers(traceAttribs);
                }
                _modifiedAttributes = traceAttribs;
                AreModifiedAttributesCurrent = true;
            }

            return _modifiedAttributes;
        }

        public void Kill(WorldDate currentWorldDate)
        {
            DiedDate = new WorldDate(currentWorldDate.Year, currentWorldDate.Season, currentWorldDate.Day);
        }

        public int Age(WorldDate currentWorldDate)
        {
            return IsAlive(currentWorldDate) ? CalculateAge(currentWorldDate) : CalculateAge(DiedDate);
        }

        public bool IsAlive(WorldDate currentWorldDate)
        {
            if (DiedDate == null)
            {
                return true;
            }
            return currentWorldDate.Year < DiedDate.Year && currentWorldDate.Season < DiedDate.Season &&
                   currentWorldDate.Day < DiedDate.Day;
        }

        private int CalculateAge(WorldDate worldDate)
        {
            var age = worldDate.Year - BornDate.Year;

            if (worldDate.Season - BornDate.Season <= 0)
            {
                return age;
            }
            if (worldDate.Day - BornDate.Day > 0)
            {
                age++;
            }
            return age;
        }

        public string PrintName()
        {
            return Firstname + " " + Dynasty;
        }

        public bool IsBorn(WorldDate currentWorldDate)
        {
            return currentWorldDate.Year >= BornDate.Year && currentWorldDate.Season >= BornDate.Season &&
                   currentWorldDate.Day >= BornDate.Day;
        }

        public bool IsAdult(WorldDate currentWorldDate)
        {
            return CalculateAge(currentWorldDate) >= 16;
        }
    }
}