using FirstApp.Model;

namespace FirstApp
{
    public static class Data
    {
        public static Dictionary<int, Fruit> all = 
            new Dictionary<int, Fruit>() {
                { 1, new Fruit("Banana", 20) },
                { 2, new Fruit("Apple", 40) }          
        };
    }
}
