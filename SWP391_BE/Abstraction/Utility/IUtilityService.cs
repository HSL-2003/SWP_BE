namespace SWP391_BE.Abstraction.Utility
{
    public interface IUtilityService
    {
        public string GenerationCode();
        public string Hash(string content);
        public bool Verify(string content, string hash);

        /// <summary>
        /// Upload file to specific folder
        /// </summary>
        /// <param name="fileUpload"></param>
        /// <param name="folder"></param>
        /// <returns></returns>
        public Task<string> AddFile(IFormFile fileUpload, string folder);

        public void RemoveFile(List<string> addressFile);

        public string CreateQrCode(string paymentCode, string name, string email, string phone);
    }
}
