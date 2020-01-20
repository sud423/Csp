namespace Csp.Jwt
{
    public interface IIdentityParser<T>
    {
        T Parse();
    }
}
