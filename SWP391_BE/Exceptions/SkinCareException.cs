namespace SWP391_BE.Exceptions
{
    public class SkinCareException : Exception
    {
        public SkinCareException() { }
        public SkinCareException(string message) : base(message) { }
        public SkinCareException(string message, Exception inner) : base(message, inner) { }
    }

    public class ProductNotFoundException : SkinCareException
    {
        public ProductNotFoundException(int productId) 
            : base($"Không tìm thấy sản phẩm với ID {productId}") { }
    }

    public class SkinTypeNotFoundException : SkinCareException
    {
        public SkinTypeNotFoundException(int skinTypeId) 
            : base($"Không tìm thấy loại da với ID {skinTypeId}") { }
    }

    public class InvalidAssessmentException : SkinCareException
    {
        public InvalidAssessmentException(string message) 
            : base(message) { }
    }

    public class SkinRoutineNotFoundException : SkinCareException
    {
        public SkinRoutineNotFoundException(int skinTypeId) 
            : base($"Không tìm thấy routine cho loại da với SkinTypeId {skinTypeId}") { }
    }
} 