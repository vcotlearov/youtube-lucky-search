using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Youtube_lucky_search.Model
{
    public class YoutubeStatistics
    {
        public int viewCount { get; set; }
        public int likeCount { get; set; }
        public int dislikeCount { get; set; }

        public int rating {
            get
            {
                if (likeCount <= 0) return 0;
                if (likeCount - dislikeCount <= 0) return 0;
                return (int)(((double)(likeCount - dislikeCount) / likeCount) * 100);
            }
        }

        public YoutubeStatistics(int view, int like, int dislike)
        {
            viewCount = view;
            likeCount = like;
            dislikeCount = dislike;
        }
    }
}
