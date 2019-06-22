using Xunit;
using RelativeRank.Entities;
using System.Collections.Generic;

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

        [Fact]
        public void RankOfEachShowInRankedShowListShouldMatchIndexInShowListPlusOne()
        {
            var showList = new RankedShowList();

            var numberOfShowsToTest = 3;

            for (var i = 0; i < numberOfShowsToTest; i++)
            {
                showList.Add(new RankedShow());
            }

            for (var i = 0; i < numberOfShowsToTest; i++)
            {
                Assert.Equal(i + 1, showList[i].Rank);
            }
        }

        [Fact]
        public void AddingShowWithRankOneShouldIncrementAllOtherShowsRankByOne()
        {
            var showList = new RankedShowList();
            var rankedShows = new List<RankedShow>();

            var numberOfShowsToTest = 3;
            for (var i = 0; i < numberOfShowsToTest; i++)
            {
                showList.Add(new RankedShow() { Name = $"{i}" });

                //getting a reference of each show in the internal list
                rankedShows.Add(showList[i]);
            }

            var startingRanks = new List<short>();
            for (var i = 0; i < numberOfShowsToTest; i++)
            {
                startingRanks.Add(rankedShows[i].Rank);
            }

            //check before adding to showList that each show rank is it's expected value
            foreach (var show in rankedShows)
            {
                Assert.Equal(show.Name, $"{show.Rank - 1}");
            }

            showList.Add(new RankedShow() { Rank = 1 });

            //check after adding show with rank 1 asserting each show rank increased by one
            foreach (var show in rankedShows)
            {
                Assert.Equal(show.Name, $"{show.Rank - 2}");
            }
        }

        [Fact]
        public void AddingShowWithRankEqualToNumberOfShowsInListShouldLeaveEachShowInListsRankUnchanged()
        {
            var showList = new RankedShowList();
            var rankedShows = new List<RankedShow>();

            var numberOfShowsToTest = 3;
            for (var i = 0; i < numberOfShowsToTest; i++)
            {
                showList.Add(new RankedShow() { Name = $"{i}" });
                rankedShows.Add(showList[i]);
            }

            var startingRanks = new List<short>();
            foreach (var show in rankedShows)
            {
                startingRanks.Add(show.Rank);
            }

            showList.Add(new RankedShow() { Rank = (short) numberOfShowsToTest });

            for (var i = 0; i < numberOfShowsToTest; i++)
            {
                Assert.Equal(startingRanks[i], showList[i].Rank);
            }
        }

        [Fact]
        public void AddingShowWithRankHigherThanShowsInListShouldChangeShowsRankToEqualShowsInList()
        {
            var showList = new RankedShowList();

            var showsInList = 3;
            for (var i = 0; i < showsInList; i++)
            {
                showList.Add(new RankedShow());
            }

            Assert.Equal(showsInList, showList.ShowsInList);

            var showWithRank100 = new RankedShow() { Rank = 100 };
            showList.Add(showWithRank100);

            Assert.Equal(showList.ShowsInList, showWithRank100.Rank);
        }
    }
}
