namespace MyEiu.Data.EF.Interface
{
    public interface IUnitOfWork: IDisposable
    {
        void SaveChange();

        Task SaveChangeAsync();
    }
}
