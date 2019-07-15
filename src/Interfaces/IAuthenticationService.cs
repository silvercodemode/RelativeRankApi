namespace RelativeRank.Interfaces
{
    public interface IAuthenticationService
    {
        string Hash(string s);
        bool ValidateHash(string hash, string nonHashedString);
        string GenerateJwt(string claimName, string secret);
    }
}
