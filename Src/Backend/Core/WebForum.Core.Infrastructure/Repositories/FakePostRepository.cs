using WebForum.Core.Application.Interfaces;
using WebForum.Core.Domain.Models;

namespace WebForum.Core.Infrastructure.Repositories;

public sealed class FakePostRepository : IPostRepository
{
    private static readonly List<Post> Posts = [];

    public Task Save(Post post, CancellationToken cancellationToken)
    {
        Posts.Add(post);
        return Task.CompletedTask;
    }

    public Task<Post?> FindById(Guid id, CancellationToken cancellationToken)
    {
        return Task.FromResult(Posts.FirstOrDefault(post => post.Id == id));
    }

    public IAsyncEnumerable<Post> FindByParentId(Guid parentId, int take, int skip, CancellationToken cancellationToken)
    {
        return Posts.Where(post => post.ParentId == parentId).Skip(skip).Take(take).ToAsyncEnumerable();
    }

    public IAsyncEnumerable<Post> GetAll(int take, int skip, CancellationToken cancellationToken)
    {
        return Posts.Where(post => post.ParentId is null).Skip(skip).Take(take).ToAsyncEnumerable();
    }
}