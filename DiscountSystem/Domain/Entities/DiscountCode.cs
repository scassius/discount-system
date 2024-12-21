using System;

namespace DiscountSystem.Domain.Entities
{
    public class DiscountCode
    {
        public int Id { get; private set; }
        public string Code { get; private set; }
        public bool IsUsed { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private DiscountCode() { }

        public DiscountCode(string code)
        {
            Code = code;
            IsUsed = false;
            CreatedAt = DateTime.UtcNow;
        }

        public void Use()
        {
            if (IsUsed)
                throw new InvalidOperationException("CodeAlreadyUsed");

            IsUsed = true;
        }
    }
}