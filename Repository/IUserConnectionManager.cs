namespace Clone_Main_Project_0710.Repository
{
    public interface IUserConnectionManager
    {
        Task KeepUserConnection(Guid userId, string connectionId);
        Task RemoveUserConnection(string connectionId);
        List<string> GetUserConnections(Guid userId);
    }
}