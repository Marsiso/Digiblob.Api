﻿namespace Digiblob.Api.Auth.Constants;

public static class ApiRoutes
{
    public const string Root = "api";
    public const string Version = "v1";
    public const string Base = Root + "/" + Version;

    public static class Posts
    {
        public const string Signin = Base + "/sign-in";
        public const string Login = Base + "/login";
    }

    public static class Gets
    {
        public const string Login = Posts.Login;
    }
}