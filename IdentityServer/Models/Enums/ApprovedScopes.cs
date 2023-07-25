using System.Runtime.Serialization;

namespace IdentityServer.Models.Enums
{
    public enum ApprovedScopes
    {
        MovieApi,
        [EnumMember(Value = "roles")]
        Roles
    }
}
