using FirstApp.Model;

namespace FirstApp
{
    public interface ICommand
    {
        IResult GetById(int id);
        IResult CreateFruit(int id, Fruit fruktik);
        IResult DeleteFruit(int id);
    }
}
