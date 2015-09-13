namespace Worldbuilder
{
    public interface ICharacterAttribute
    {
        string Name { get; set; }
        decimal Value { get; set; }
    }

    public class ValueCharacterAttribute:ICharacterAttribute
    {
        public ValueCharacterAttribute(string name, decimal value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public decimal Value { get; set; }
    }


    public class TimeCharacterAttribute:ICharacterAttribute
    {
        public TimeCharacterAttribute(string name, decimal value, WorldDate endDate)
        {
            Name = name;
            EndDate = endDate;
            Value = value;
        }

        public string Name { get;set;}
        public decimal Value { get; set; }
        public WorldDate EndDate { get; set; }
    }
}