using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using WpfCoreDemo.Data.Domain;

namespace WpfCoreDemo.Data.Services
{
    public class TweetService
    {
        private readonly Func<TweetReaderContext> dbContextFactory;

        public TweetService(Func<TweetReaderContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task SyncTweetsAsync()
        {
            using (var db = dbContextFactory())
            {
                var tweets = Timeline.GetHomeTimeline().ToList();

                var tweetIds = tweets.Select(t => t.IdStr).ToList();
                var existingTweets = db.Tweets.Where(t => tweetIds.Contains(t.TwitterId));

                foreach (var tweet in tweets)
                {
                    if (existingTweets.Any(t => t.TwitterId == tweet.IdStr))
                    {
                        continue;
                    }

                    var entity = new Model.Tweet()
                    {
                        TwitterId = tweet.IdStr,
                        Date = tweet.CreatedAt,
                        AuthorName = tweet.CreatedBy.ScreenName,
                        AuthorImageUrl = tweet.CreatedBy.ProfileImageUrl400x400,
                        Text = tweet.Text
                    };
                    db.Tweets.Add(entity);
                }
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<TweetDisplayData>> GetTweets()
        {
            using (var db = dbContextFactory())
            {
                return await db.Tweets
                    .OrderByDescending(t => t.Date)
                    .Select(t => new TweetDisplayData()
                    {
                        AuthorName = t.AuthorName,
                        AuthorImageUrl = t.AuthorImageUrl,
                        Text = t.Text,
                        Date = t.Date
                    })
                    .Take(20)
                    .ToListAsync();
            }
        }

    }
}
