namespace Digiblob.Api.Auth.Constants;

public static class DbDefaults
{
    public const string ConnectionString =
        "Server=localhost,1433;Database=Digiblob;Trusted_Connection=True;Integrated Security=False;MultipleActiveResultSets=True;TrustServerCertificate=True;User=sa;Password=Pass123$;";

    public const string Schema = "dbo";

    public static class Tables
    {
        public const string Users = "users";
        public const string Roles = "roles";
        public const string UserRoles = "user_roles";
    }

    public static class Columns
    {
        #region Common

        public const string PrimaryKeyColumnName = "id";

        public const string ConcurrencyStampColumnName = "concurrency_stamp";

        #endregion

        #region User

        public const string UserNameColumnName = "user_name";

        public const string NormalizedUserNameColumnName = "normalized_user_name";

        public const string EmailColumnName = "email";

        public const string NormalizedEmailColumnName = "normalized_email";

        public const string PasswordHashColumnName = "password_hash";

        public const string EmailConfirmedColumnName = "email_confirmed";

        public const string GivenNameColumnName = "given_name";

        public const string FamilyNameColumnName = "family_name";

        public const string SecurityStampColumnName = "security_stamp";

        public const string AccessFailCountColumnName = "access_fail_count";

        public const string DateLockoutEndColumnName = "date_locukout_end";

        #endregion

        #region Role

        public const string RoleNameColumnName = "role_name";

        public const string NormalizedRoleNameColumnName = "normalized_role_name";

        #endregion

        #region UserRole

        public const string UserForeignKeyColumnName = "user_id";

        public const string RoleForeignKeyColumnName = "role_id";

        #endregion
    }

    public static class Constraints
    {
        #region User
        
        public const string AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789~!#$%^&*-_=+`|<>[]{}():?/\\";
    
        public const string AllowedPasswordCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789`~!@#$%^&*()-_=+()[]{}:.<>\\/?|";
        
        public const int MinimumPasswordLength = 7;
        
        public const int MaximumPasswordLength = 100;
        
        public const int MinimumUserNameLength = 2;

        public const int MaximumUserNameLength = 255;

        public const int MinimumGivenNameLength = 2;

        public const int MaximumGivenNameLength = 256;

        public const int MinimumFamilyNameLength = 2;

        public const int MaximumFamilyNameLength = 256;
        
        public const int MaximumEmailLength = 256;
        
        public const int SecurityStampLength = 450;

        #endregion
        
        #region Role
        
        public const string AllowedRoleNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public const int MinimumRoleNameLength = 2;

        public const int MaximumRoleNameLength = 256;

        #endregion
    }
}