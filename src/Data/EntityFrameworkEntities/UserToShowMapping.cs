namespace RelativeRank.EntityFrameworkEntities
{
    public partial class UserToShowMapping
    {
        public int UserId { get; set; }
        public int ShowId { get; set; }
        public int Rank { get; set; }
        public double PercentileRank { get; set; }

        public Show ShowNavigation { get; set; }
        public User UserNavigation { get; set; }
    }
}
