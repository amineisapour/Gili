namespace AuthenticationMicroservice.Core.Interfaces
{
    public interface IAccountService
    {
        public System.Guid? ValidateJwtToken(string token);
        public Domain.Models.RefreshToken GenerateRefreshToken(string ipAddress);

        public string GenerateJwtToken(Domain.Models.User user);
        public Domain.ViewModels.PasswordViewModel CreatePassword(string password);
        public bool IsPasswordValid(Domain.ViewModels.PasswordViewModel passwordData);

        public Domain.Models.RefreshToken RotateRefreshToken(Domain.Models.RefreshToken refreshToken, string ipAddress);
        public void RevokeDescendantRefreshTokens(Domain.Models.RefreshToken refreshToken, Domain.Models.User user, string ipAddress, string reason);
        public void RevokeRefreshToken(Domain.Models.RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null);
        //public void RemoveOldRefreshTokens(Domain.Models.User user);
        public System.Collections.Generic.List<Domain.Models.RefreshToken> RemoveOldRefreshTokens(Domain.Models.User user);

    }
}
