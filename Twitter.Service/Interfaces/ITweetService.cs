﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Data.DTOs;
using Twitter.Data.Models;

namespace Twitter.Service.Interfaces
{
    public interface ITweetService
    {
        IEnumerable<TweetDetails> GetTweets(int pageSize, int pageNumber);
        public int GetTweetsCount();
        IEnumerable<TweetDetails> GetMyTweets(string id, int pageSize, int pageNumber);
        IEnumerable<TweetDetails> GetHomePageTweets(string id, int pageSize, int pageNumber);
        TweetDetails GetTweet(int id);
        TweetDetails PostTweet(AddTweetModel tweet);
        TweetDetails PostReplyToTweet(int id, AddTweetModel tweet);
        IEnumerable<TweetDetails> GetTweetReplies(int id);
        void DeleteTweet(int id);
        bool TweetExists(int id);
    }
}
