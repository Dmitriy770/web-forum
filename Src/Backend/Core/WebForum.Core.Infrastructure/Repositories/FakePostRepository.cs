using WebForum.Core.Application.Interfaces;
using WebForum.Core.Domain.Models;

namespace WebForum.Core.Infrastructure.Repositories;

public sealed class FakePostRepository : IPostRepository
{
    private static List<Post> Posts = [];

    public Task Save(Post post, CancellationToken cancellationToken)
    {
        Posts.Add(post);
        return Task.CompletedTask;
    }

    public Task Update(Post post, CancellationToken cancellationToken)
    {
        Posts = Posts.Where(p => p.Id != post.Id).ToList();
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

    public IAsyncEnumerable<Post> FindByUserId(Guid userId, int take, int skip, CancellationToken cancellationToken)
    {
        return Posts.Where(post => post.Profile.UserId == userId).Skip(skip).Take(take).ToAsyncEnumerable();
    }


    public IAsyncEnumerable<Post> GetAll(int take, int skip, CancellationToken cancellationToken)
    {
        return Posts.Where(post => post.ParentId is null)
            .Skip(skip)
            .Take(take)
            .OrderByDescending(post => post.CreationDate)
            .ToAsyncEnumerable();
    }
}