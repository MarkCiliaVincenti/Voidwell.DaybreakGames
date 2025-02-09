﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Voidwell.DaybreakGames.Domain.Models;

namespace Voidwell.DaybreakGames.Services.Planetside
{
    public interface IFeedService
    {
        Task<IEnumerable<FeedItem>> GetNewsFeed();
        Task<IEnumerable<FeedItem>> GetUpdateFeed();
    }
}
