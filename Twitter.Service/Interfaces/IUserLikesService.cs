﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Data.DTOs;
using Twitter.Data.Models;

namespace Twitter.Service.Interfaces
{
    public interface IUserLikesService
    {
        public void Like(UserLikes userLikes);
        public void DisLike(UserLikes userLikes);
        public List<UserInteractionDetails> GetTweetLikes(int pageSize, int pageNumber, int tweetID);
        public List<TweetDetails> GetUserLikedTweets(int pageSize, int pageNumber, string userID);
    }
}