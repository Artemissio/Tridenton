namespace Tridenton.Core.Utilities;

public sealed class MalformedTreidException : Exception
{
    private const string Header = "Malformed TREID - {0}";

    public MalformedTreidException() : base($"TREID is in incorrect format. TREID format is: {Constants.Treid}{Constants.TreidDelimiter}<partition>{Constants.TreidDelimiter}<account-id>{Constants.TreidDelimiter}<service>{Constants.TreidDelimiter}<resource-type>{Constants.TreidDelimiter}<resource-id>") { }

    public MalformedTreidException(string message) : base(string.Format(Header, message)) { }
}