using Fluxor;
using WebForum.Frontend.Models;

namespace WebForum.Frontend.States.AuthState;

[FeatureState]
public record AuthState
{
    public bool IsLoading = false;
    public string Error = "";
    public AuthInfo? Info { get; init; }

    public AuthState()
    {
    }

    public bool IsLogInEnd()
    {
        return Info?.ExpiresIn > DateTime.Now;
    }
}