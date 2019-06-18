using Xunit;
using RelativeRank.Entities;
using RelativeRank.Models;

namespace RelativeRankTests
{
    public class RankedShowListTests
    {
        [Fact]
        public void AddingShowToListShouldIncreaseShowsInListByOne()
        {
            var showList = new RankedShowList();
            var showsInListBeforeAddingShow = showList.ShowsInList;

            showList.Add(new RankedShow());
            var showsInListAfterAddingShow = showList.ShowsInList;

            Assert.Equal(showsInListBeforeAddingShow + 1, showsInListAfterAddingShow);
        }
    }
}
