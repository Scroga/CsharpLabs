namespace InternetoveKnihkupectvi;

#nullable enable

public enum RequestType { EndOfInput = 0, Invalid, Valid }

public readonly record struct Request(int CustId, string Url, RequestType Type = RequestType.Valid) 
{
    public Request(RequestType type) : this(-1, "", type) { }
}
