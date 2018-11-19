using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Youtube_lucky_search.Model
{
    public class YoutubeStatistics
    {
        public ulong? viewCount { get; set; }
        public ulong? likeCount { get; set; }
        public ulong? dislikeCount { get; set; }

        public ulong? rating {
            get
            {
                if (!likeCount.HasValue || likeCount <= 0) return 0;
                if (!dislikeCount.HasValue) return 100;
                if (likeCount - dislikeCount <= 0) return 0;
                return (ulong?)(((double)(likeCount - dislikeCount) / likeCount) * 100);
            }
        }

        public YoutubeStatistics(ulong? view, ulong? like, ulong? dislike)
        {
            viewCount = view;
            likeCount = like;
            dislikeCount = dislike;
        }
    }
}
