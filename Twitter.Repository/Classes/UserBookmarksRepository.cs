﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Data.Models;
using Twitter.Repository.classes;
using Twitter.Repository.Interfaces;

namespace Twitter.Repository.Classes
{
    public class UserBookmarksRepository : Repository<UserBookmarks>, IUserBookmarksRepository
    {
        private readonly ApplicationDbContext _context;

        public UserBookmarksRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

        public void BookMark(UserBookmarks userBookmarks)
        {
            Insert(userBookmarks);
            Commit();
        }

        public IEnumerable<ApplicationUser> GetTweetBookmarks(int pageSize, int pageNumber, int TweetID)
        {
            return GetPageRecordsWhere(pageSize, pageNumber, u => u.TweetId == TweetID, "ApplicationUser").Select(u => u.ApplicationUser).ToList();
        }

        public IEnumerable<Tweet> GetUserBookmarkedTweets(int pageSize, int pageNumber, string userID)
        {
            return GetPageRecordsWhere(pageSize, pageNumber, u => u.UserId == userID, "Tweet.Author,Tweet.Images").Select(u => u.Tweet).ToList();
        }

        public void RemoveBookMark(UserBookmarks userBookmarks)
        {
            Delete(userBookmarks);
            Commit();
        }
    }
}