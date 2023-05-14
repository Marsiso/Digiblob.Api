namespace Digiblob.Api.Auth.Constants;

/// <summary>
///     Collection of fine-grained access control.
/// </summary>
public static class Roles
{
    public const string CanCreateUsers = "can_create_users";
    public const string CanReadUsers = "can_read_users";
    public const string CanUpdateUsers = "can_update_users";
    public const string CanDeleteUsers = "can_delete_users";
    public const string CanAssignPermissions = "can_assign_permissions";
    public const string CanRevokePermissions = "can_revoke_permissions";
    public const string CanCreateResources = "can_create_resources";
    public const string CanReadResources = "can_read_resources";
    public const string CanUpdateResources = "can_update_resources";
    public const string CanDeleteResources = "can_delete_resources";
}