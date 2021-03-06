using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfCoreDemo.Data.Domain;
using WpfCoreDemo.Data.Services;

namespace WpfCoreDemo.App
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly TweetService tweetService;

        private bool syncInProgress;

        public ObservableCollection<TweetDisplayData> Tweets { get; set; } = new ObservableCollection<TweetDisplayData>();

        public ICommand AboutCommand { get; private set; }

        public bool SyncInProgress
        {
            get { return syncInProgress; }
            set
            {
                syncInProgress = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SyncInProgress)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public MainViewModel(TweetService tweetService)
        {
            this.tweetService = tweetService;

            AboutCommand = new RelayCommand(About);
        }


        public async void OnAttached()
        {
            SyncInProgress = true;

            List<TweetDisplayData> tweets = null;
            await Task.Run(async () =>
            {
                await tweetService.SyncTweetsAsync();
                tweets = await tweetService.GetTweets();
            });

            foreach (var tweet in tweets)
            {
                Tweets.Add(tweet);
            }

            SyncInProgress = false;
        }

        private void About()
        {
            var aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }

    }
}