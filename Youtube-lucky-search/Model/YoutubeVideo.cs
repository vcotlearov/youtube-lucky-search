using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Youtube_lucky_search.Model
{
    public class YoutubeVideo
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public string id { get; set; }
        public YoutubeStatistics statistics { get; set; }


        public YoutubeVideo() { }
        public YoutubeVideo(string id)
        {
            this.id = id;
        }
    }
}
