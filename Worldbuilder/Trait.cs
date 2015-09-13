using System.Collections.Generic;
using Worldbuilder.Annotations;

namespace Worldbuilder
{
    public class Trait
    {
        public Trait()
        {
        }

        public Trait(string name, List<Modifier> traitModifiers)
        {
            Name = name;
            TraitModifiers = traitModifiers;
        }

        public string Name { get; set; }
        public List<Modifier> TraitModifiers { get; set; }

        public Dictionary<string, ICharacterAttribute> ApplyModifiers(Dictionary<string, ICharacterAttribute> inParams)
        {
            //Copy / Clone... Whatever you want to call it this is important as to not 
            //unintentionally screw up yoru base collection.
            var response = new Dictionary<string, ICharacterAttribute>();

            foreach (var m in TraitModifiers)
            {
                //If we have this attrib, keep going
                if (inParams.ContainsKey(m.TargetName))
                {
                    //If this is the first time the response ran into it, add it
                    if (!response.ContainsKey(m.TargetName))
                    {
                        response.Add(m.TargetName, inParams[m.TargetName]);
                    }

                    //And wait what's this... let the Modifier apply it!?  
                    //yes... pass it down again... you'll see why in a second.
                    m.Apply(response[m.TargetName]);
                }
            }

            return response;
        }
    }
}