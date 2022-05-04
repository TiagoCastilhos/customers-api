namespace Customers.Model.Types
{
    public sealed class Email
    {
        public string Value { get; }

        public Email(string value)
        {
            AssertionConcern.AssertArgumentNotNullOrEmpty(value, "Email");
            AssertionConcern.AssertArgumentIsEmail(value);

            Value = value;
        }

        public static implicit operator Email(string value)
            => new(value);

        public static implicit operator string(Email email)
            => email.Value;
    }
}
