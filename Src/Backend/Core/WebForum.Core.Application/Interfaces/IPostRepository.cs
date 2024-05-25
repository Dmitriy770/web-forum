using WebForum.Core.Domain.Models;

namespace WebForum.Core.Application.Interfaces;

public interface IPostRepository
{
    public Task Save(Post post, CancellationToken cancellationToken);
    
    public Task<Post?> FindById(Guid id, CancellationToken cancellationToken);
    
    public IAsyncEnumerable<Post> FindByParentId(Guid parentId, int take, int skip, CancellationToken cancellationToken);

    public IAsyncEnumerable<Post> FindByUserId(Guid userId, int take, int skip, CancellationToken cancellationToken);

    public IAsyncEnumerable<Post> GetAll(int take, int skip, CancellationToken cancellationToken);
}