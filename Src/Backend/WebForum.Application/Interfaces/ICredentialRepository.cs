using WebForum.Domain.Models;
using WebForum.Domain.Models.UserModels;

namespace WebForum.Application.Interfaces;

public interface ICredentialRepository
{
    public Task Add(Credential credential, CancellationToken cancellationToken);

    public Task<Credential> GetById(Guid id, CancellationToken cancellationToken);
}