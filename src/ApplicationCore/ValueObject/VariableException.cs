namespace ApplicationCore.ValueObject
{
    public static class VariableException
    {
        public static string BadRequest { get; } = "Bad Request";
        public static string NotFound { get; } = "Not Found";
        public static string UserNotFound { get; } = "User Not Found";
        public static string EmailNull { get; } = "Not yet regrist email";
        public static string EmailNotConfirm { get; } = "Email not yet confirm";
        public static string ChangePassword { get; } = "New password don't like confirm password";
    }
}
