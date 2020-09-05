namespace RelativeRank.DataTransferObjects
{
    public class AdminUpdateUserModel
    {
        public string? UserToUpdateCurrentUsername { get; set; }
        public string? UserToUpdateNewUsername { get; set; }
        public string? UserToUpdateNewPassword { get; set; }
    }
}
