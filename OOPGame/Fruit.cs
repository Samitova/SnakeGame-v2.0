
namespace OOPGame
{
    abstract class Fruit 
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }        

        public Fruit(int x, int y, int size)
        {
            X = x;
            Y = y;
            Size = size;
        }
    }
}
