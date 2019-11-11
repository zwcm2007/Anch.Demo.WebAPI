namespace Anch.Demo.Application
{
    public class LoginOutput
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }

        public UserInfo UserInfo { get; set; }
    }

    public class UserInfo
    {
        public string Id { get; set; }
        public string UserName { get; set; }
    }
}