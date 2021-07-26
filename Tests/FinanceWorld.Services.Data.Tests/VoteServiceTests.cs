namespace FinanceWorld.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Common.Repositories;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Votes;
    using Moq;

    using Xunit;

    public class VoteServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<Vote>> mockRepo;
        private readonly List<Vote> list;
        private readonly VotesService votesService;

        public VoteServiceTests()
        {
            this.list = new List<Vote>();
            this.mockRepo = new Mock<IDeletableEntityRepository<Vote>>();
            this.mockRepo.Setup(x => x.All()).Returns(this.list.AsQueryable());
            this.mockRepo.Setup(x => x.AddAsync(It.IsAny<Vote>())).Callback((Vote vote) => this.list.Add(vote));
            this.mockRepo.Setup(x => x.AllAsNoTracking()).Returns(this.list.AsQueryable());
            this.votesService = new VotesService(this.mockRepo.Object);
        }

        [Fact]
        public async Task WhenUsersVoteTwoTimesOnlyOneShouldBeCounted()
        {
            await this.votesService.SetVote("1", "1", true);
            await this.votesService.SetVote("1", "1", true);

            Assert.Single(this.list);
        }

        [Fact]
        public async Task WhenUserVotesWithUpVoteOnceCountOfLikesShouldBeOne()
        {
            await this.votesService.SetVote("1", "1", true);
            int likesCount = this.votesService.GetLikes("1");

            Assert.True(likesCount == 1);
        }

        [Fact]
        public async Task WhenUserVoteFirstTimeWithUpVoteAndSecondTimeWithDownVoteSecondVoteShouldBeCounted()
        {
            await this.votesService.SetVote("1", "1", true);
            await this.votesService.SetVote("1", "1", false);
            int dislikesCount = this.votesService.GetDislikes("1");

            Assert.True(dislikesCount == 1);
        }

        [Fact]
        public async Task WhenTwoUsersVotesEveryOneOfThemShouldBeCouted()
        {
            await this.votesService.SetVote("1", "1", true);
            await this.votesService.SetVote("1", "2", false);

            Assert.Equal(2, this.list.Count);
        }

        [Fact]
        public async Task WhenManyUsersVoteEveryOneOfThemShouldBeCountedAndLikesAndDislikesShouldBeCorrect()
        {
            await this.votesService.SetVote("1", "1", true);
            await this.votesService.SetVote("1", "2", false);
            await this.votesService.SetVote("1", "3", true);
            await this.votesService.SetVote("1", "4", false);
            await this.votesService.SetVote("1", "5", false);
            await this.votesService.SetVote("1", "6", false);

            Assert.Equal(2, this.votesService.GetLikes("1"));
            Assert.Equal(4, this.votesService.GetDislikes("1"));
            Assert.Equal(6, this.list.Count);
        }
    }
}
