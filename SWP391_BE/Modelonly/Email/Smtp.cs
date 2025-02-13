namespace SWP391_BE.Modelonly.Email
{
    public class Smtp
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public bool EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }

    }

}
