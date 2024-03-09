namespace SG.Units
{
    public interface IUnit
    {
        public string Name { get; }
        public int Id { get; }
        public int Stength { get; }
        public int Agility { get; }
        public int Lucky { get; }
        public int Endurance { get; }

        public int Health { get; set; }
    }
}
