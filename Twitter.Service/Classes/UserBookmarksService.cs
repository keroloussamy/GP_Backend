﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Data.DTOs;
using Twitter.Data.Models;
using Twitter.Repository.Interfaces;
using Twitter.Service.Interfaces;

namespace Twitter.Service.Classes
{
    public class UserBookmarksService : BaseService, IUserBookmarksService
    {
        private readonly IUserBookmarksRepository _userBookmarksRepository;
        private readonly IUserLikesRepository _userLikesRepository;
        private readonly IUserFollowingRepository _userFollowingRepository;

        public UserBookmarksService(
            IUserBookmarksRepository userBookmarksRepository,
            IUserLikesRepository userLikesRepository,
            IUserFollowingRepository userFollowingRepository,
            IMapper mapper) : base(mapper)
        {
            _userBookmarksRepository = userBookmarksRepository;
            _userLikesRepository = userLikesRepository;
            _userFollowingRepository = userFollowingRepository;
        }

        public void BookMark(UserBookmarks userBookmarks)
        {
            _userBookmarksRepository.BookMark(userBookmarks);
        }

        public void RemoveBookMark(UserBookmarks userBookmarks)
        {
            _userBookmarksRepository.RemoveBookMark(userBookmarks);
        }

        public List<UserInteractionDetails> GetTweetBookmarks(int pageSize, int pageNumber, int tweetID)
        {
            return Mapper.Map<UserInteractionDetails[]>(_userBookmarksRepository.GetTweetBookmarks(pageSize, pageNumber, tweetID)).ToList();
        }

        public IEnumerable<TweetDetails> GetUserBookmarkedTweets(int pageSize, int pageNumber, string userID, string currentUserID)
        {
            var tweets = _userBookmarksRepository.GetUserBookmarkedTweets(pageSize, pageNumber, userID);
            // trival solution
            var tweetsDetails = Mapper.Map<TweetDetails[]>(tweets);
            for (int i = 0; i < tweetsDetails.Count(); i++)
            {
                tweetsDetails[i].IsLiked = _userLikesRepository.LikeExists(currentUserID, tweetsDetails[i].Id);
                tweetsDetails[i].IsBookmarked = _userBookmarksRepository.BookmarkExists(currentUserID, tweetsDetails[i].Id);
                tweetsDetails[i].Author.IsFollowedByCurrentUser = (currentUserID == tweetsDetails[i].Author.Id) || _userFollowingRepository.FollowingExists(currentUserID, tweetsDetails[i].Author.Id);
            }
            return tweetsDetails;
        }

        public bool BookmarkExists(string userId, int tweetId)
        {
            return _userBookmarksRepository.BookmarkExists(userId, tweetId);
        }
    }
}
