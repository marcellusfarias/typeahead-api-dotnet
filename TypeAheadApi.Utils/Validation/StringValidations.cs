namespace TypeAheadApi.Utils.Validations
{
    public class StringValidations : BaseValidations<string, StringValidations>
    {
        public StringValidations(string value) : base(value)
        {
        }

        public StringValidations IsNotNullOrWhiteSpace(string? message = null)
        {
            if (string.IsNullOrWhiteSpace(value))
                Error(message);

            return this;
        }
    }
}