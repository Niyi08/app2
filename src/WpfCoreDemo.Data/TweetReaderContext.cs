using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using WpfCoreDemo.Data.Model;

namespace WpfCoreDemo.Data
{
    public class TweetReaderContext : DbContext
    {

        public TweetReaderContext(string nameOrConnectionString) 
            : base(nameOrConnectionString)
        {

        }

        public DbSet<Tweet> Tweets { get; set; }

    }
}
