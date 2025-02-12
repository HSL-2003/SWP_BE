namespace SWP391_BE.Modelonly.Email
{
    public class EmailSetting
    {
        public string FromEmailAddress { get; set; } = string.Empty;
        public string FromDisplayName { get; set; } = string.Empty;
        public Smtp Smtp { get; set; } = new Smtp();

    }
}
