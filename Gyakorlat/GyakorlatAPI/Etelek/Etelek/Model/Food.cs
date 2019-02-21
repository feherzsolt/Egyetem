namespace Etelek.Model
{
    public class Food
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public byte Difficulty { get; set; }
        public FoodType Type { get; set; }
    }
}
