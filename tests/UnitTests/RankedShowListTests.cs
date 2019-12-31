using Xunit;
using RelativeRank.Entities;
using System.Collections.Generic;
using System;

namespace RelativeRankTests.UnitTests
{
    public class RankedShowListTests
    {
        [Fact]
        public void Enumeration_ShouldWork()
        {
            // Arrange
            var showList = new RankedShowList();

            // Act and Assert
            foreach (var show in showList);
        }
        [Fact]
        public void AddingNullShow_InAddMethod_ThrowsArgumentNullException()
        {
            // Arrange
            var showList = new RankedShowList();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => showList.Add(null));
        }

        [Fact]
        public void AddingShowToList_ShouldIncreaseNumberOfShowsInList_ByOne()
        {
            var showList = new RankedShowList();
            var showsInListBeforeAddingShow = showList.NumberOfShowsInList;

            showList.Add(new RankedShow());
            var showsInListAfterAddingShow = showList.NumberOfShowsInList;

            Assert.Equal(showsInListBeforeAddingShow + 1, showsInListAfterAddingShow);
        }

        [Fact]
        public void RankOfEachShow_InRankedShowList_ShouldMatch_IndexInShowListPlusOne()
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
        public void AddingShow_WithRankOne_ShouldIncrementAllOtherShowsRank_ByOne()
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

            var startingRanks = new List<int>();
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
        public void AddingShow_WithRankEqualToNumberOfShowsInList_ShouldLeaveEachShowInListsRankUnchanged()
        {
            var showList = new RankedShowList();
            var rankedShows = new List<RankedShow>();

            var numberOfShowsToTest = 3;
            for (var i = 0; i < numberOfShowsToTest; i++)
            {
                showList.Add(new RankedShow() { Name = $"{i}" });
                rankedShows.Add(showList[i]);
            }

            var startingRanks = new List<int>();
            foreach (var show in rankedShows)
            {
                startingRanks.Add(show.Rank);
            }

            showList.Add(new RankedShow() { Rank = (int) numberOfShowsToTest });

            for (var i = 0; i < numberOfShowsToTest; i++)
            {
                Assert.Equal(startingRanks[i], showList[i].Rank);
            }
        }

        [Fact]
        public void AddingShow_WithRankHigherThanNumberOfShowsInList_ShouldChangeShowsRank_ToNumberOfShowsInList()
        {
            var showList = new RankedShowList();

            var showsInList = 3;
            for (var i = 0; i < showsInList; i++)
            {
                showList.Add(new RankedShow());
            }

            Assert.Equal(showsInList, showList.NumberOfShowsInList);

            var showWithRank100 = new RankedShow() { Rank = 100 };
            showList.Add(showWithRank100);

            Assert.Equal(showList.NumberOfShowsInList, showWithRank100.Rank);
        }

        [Fact]
        public void GetPercentileRankAtIndexMethod_ShouldCorrectlyCalculate_ThePercentileRankOfEachShow()
        {
            var showList = new RankedShowList();

            var showsInList = 3;
            for (var i = 0; i < showsInList; i++)
            {
                showList.Add(new RankedShow());
            }

            for (var i = 0; i < showsInList; i++)
            {
                Assert.Equal(1 - (1 / ((double) (showsInList + 1))) * (i + 1), showList.GetPercentileRankAtIndex(i));
            }
        }

        [Fact]
        public void EachShowsPercentileRank_ShouldMatchValue_FromGetPercentileRankAtIndex()
        {
            var showList = new RankedShowList();

            var showsInList = 3;
            for (var i = 0; i < showsInList; i++)
            {
                showList.Add(new RankedShow());
            }

            for (var i = 0; i < showsInList; i++)
            {
                var expected = showList.GetPercentileRankAtIndex(i);
                var actual = showList[i].PercentileRank;
                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void PassingNullShowEnumerable_ToReplaceAllMethod_ThrowsArgumentNullException()
        {
            // Arrange
            var showList = new RankedShowList();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => showList.ReplaceAll(null));
        }

        [Fact]
        public void PassingEnumerable_ThatContainsMultipleShowsWithTheSameRank_ToReplaceAllMethod_ThrowsArgumentException()
        {
            // Arrange
            var showList = new RankedShowList();

            var replacementList = new List<RankedShow>();

            replacementList.Add(new RankedShow
            {
                Name = "show1",
                Rank = 1
            });

            replacementList.Add(new RankedShow
            {
                Name = "show2",
                Rank = 1
            });

            // Act and Assert
            Assert.Throws<ArgumentException>(() => showList.ReplaceAll(replacementList));
        }

        [Fact]
        public void AfterReplaceAll_RankOfEachShow_InRankedShowList_ShouldMatch_IndexInShowListPlusOne()
        {
            // Arrange
            var showList = new RankedShowList();

            var showsInList = 3;
            for (var i = 0; i < showsInList; i++)
            {
                showList.Add(new RankedShow());
            }

            var replacementList = new List<RankedShow>();

            replacementList.Add(new RankedShow
            {
                Name = "show1",
                Rank = 2
            });

            replacementList.Add(new RankedShow
            {
                Name = "show2",
                Rank = 1
            });

            replacementList.Add(new RankedShow
            {
                Name = "show3",
                Rank = 3
            });

            // Act
            showList.ReplaceAll(replacementList);

            // Assert
            for (var i = 0; i < showsInList; i++)
            {
                Assert.Equal(i + 1, showList[i].Rank);
            }
        }

        [Fact]
        public void AfterReplaceAll_EachShowsPercentileRank_ShouldMatchValue_FromGetPercentileRankAtIndex()
        {
            // Arrange
            var showList = new RankedShowList();

            var showsInList = 3;
            for (var i = 0; i < showsInList; i++)
            {
                showList.Add(new RankedShow());
            }

            var replacementList = new List<RankedShow>();

            replacementList.Add(new RankedShow
            {
                Name = "show1",
                Rank = 2
            });

            replacementList.Add(new RankedShow
            {
                Name = "show2",
                Rank = 1
            });

            replacementList.Add(new RankedShow
            {
                Name = "show3",
                Rank = 3
            });

            // Act
            showList.ReplaceAll(replacementList);

            // Assert
            for (var i = 0; i < 3; i++)
            {
                var expected = showList.GetPercentileRankAtIndex(i);
                var actual = showList[i].PercentileRank;
                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void ListConstructedBy_IEnumerableOfRankedShowConstructor_ShouldHaveValid_Ranks()
        {
            // Arrange
            var shows = new List<RankedShow>();

            shows.Add(new RankedShow
            {
                Name = "show1",
                Rank = 2
            });

            shows.Add(new RankedShow
            {
                Name = "show2",
                Rank = 1
            });

            shows.Add(new RankedShow
            {
                Name = "show3",
                Rank = 3
            });

            // Act
            var showList = new RankedShowList(shows);

            // Assert
            for (var i = 0; i < 3; i++)
            {
                Assert.Equal(i + 1, showList[i].Rank);
            }
        }

        [Fact]
        public void ListConstructedBy_IEnumerableOfRankedShowConstructor_ShouldHaveValid_PercentileRanks()
        {
            // Arrange
            var shows = new List<RankedShow>();

            shows.Add(new RankedShow
            {
                Name = "show1",
                Rank = 2
            });

            shows.Add(new RankedShow
            {
                Name = "show2",
                Rank = 1
            });

            shows.Add(new RankedShow
            {
                Name = "show3",
                Rank = 3
            });

            // Act
            var showList = new RankedShowList(shows);

            // Assert
            for (var i = 0; i < 3; i++)
            {
                var expected = showList.GetPercentileRankAtIndex(i);
                var actual = showList[i].PercentileRank;
                Assert.Equal(expected, actual);
            }
        }
    }
}
