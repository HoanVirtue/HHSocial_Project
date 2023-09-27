namespace HHSocialNetwork_Project.Repository
{
    public interface IRepository<T>
    {
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
        bool Exist(int id);
        Task<T> FindByID(int id);
        Task<List<T>> GetAll();
    }
}