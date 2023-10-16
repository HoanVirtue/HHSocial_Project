namespace Clone_Main_Project_0710.Repository
{
    public interface IRepository<T>
    {
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(Guid id);
        bool Exist(Guid id);
        Task<T> FindByID(Guid id);
        Task<List<T>> GetAll();
    }
}