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

        public double rating {
            get
            {
                return ((double)(likeCount - dislikeCount) / likeCount) * 100;
            }
        }

        public YoutubeStatistics(int view, int like, int dislike)
        {

        }
    }
}
