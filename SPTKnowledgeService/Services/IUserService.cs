namespace SPTKnowledgeService.Services
{
    public interface IUserService
    {
        Task<bool> UserExistsAsync(string username);
    }
}
