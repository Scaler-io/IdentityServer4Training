using System.Runtime.Serialization;

namespace IdentityServer.Models.Enums
{
    public enum ApprovedScopes
    {
        MovieApi,
        JobApi,

        [EnumMember(Value = "roles")]
        Roles
    }
}
