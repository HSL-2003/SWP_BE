



using SWP391_BE.Abstraction.Utility;

namespace SWP391_BE.RealUtility

{
    public class UtilityService : IUtilityService
    {
        public Task<string> AddFile(IFormFile fileUpload, string folder)
        {
            throw new NotImplementedException();
        }

        public string CreateQrCode(string paymentCode, string name, string email, string phone)
        {
            throw new NotImplementedException();
        }

        public string GenerationCode()
        {
            throw new NotImplementedException();
        }

        public string Hash(string content)
        {
            throw new NotImplementedException();
        }

        public void RemoveFile(List<string> addressFile)
        {
            throw new NotImplementedException();
        }

        public bool Verify(string content, string hash)
        {
            throw new NotImplementedException();
        }
    }
}
