namespace KazanNeft.MobileApp.Models.Loaders
{
    public interface ILoader<T>
    {
        T Load();
    }
}
