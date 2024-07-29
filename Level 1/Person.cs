namespace Level_1
{
    public class Person
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public override string ToString()
        {
            return $"{Name}: {Birthday.ToShortDateString()}";
        }
    }
}