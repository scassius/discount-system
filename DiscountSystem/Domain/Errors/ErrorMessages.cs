namespace DiscountSystem.Domain.Errors
{
    public static class ErrorMessages
    {
        public const string CodeNotFound = "Discount code not found.";
        public const string CodeAlreadyUsed = "Discount code has already been used.";
        public const string GenerationFailed = "Failed to generate the required number of unique discount codes.";
        public const string InvalidCount = "The count must be between 1 and 2000.";
        public const string InvalidLength = "The code length must be between 7 and 8 characters.";
    }
}