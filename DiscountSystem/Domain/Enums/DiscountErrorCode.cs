namespace DiscountSystem.Domain.Enums
{
    public enum DiscountErrorCode : byte
    {
        Success = 0,
        CodeNotFound = 1,
        CodeAlreadyUsed = 2,
        GenerationFailed = 3
    }
}