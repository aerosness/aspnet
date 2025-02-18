using FirstApp.Model;

namespace FirstApp
{
    public interface ICommand
    {
        IResult GetById(int id);
        IResult CreateFruit(int id, Fruit fruit);
        IResult DeleteFruit(int id);
    }
}
