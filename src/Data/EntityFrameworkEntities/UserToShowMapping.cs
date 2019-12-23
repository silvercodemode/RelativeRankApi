namespace RelativeRank.EntityFrameworkEntities
{
    public partial class UserToShowMapping
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Showname { get; set; }
        public int Rank { get; set; }

        public Show ShownameNavigation { get; set; }
        public User UsernameNavigation { get; set; }
    }
}
