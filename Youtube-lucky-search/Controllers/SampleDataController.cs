using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Youtube_lucky_search.Model;

namespace Youtube_lucky_search.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        [HttpGet("[action]")]
        public YoutubeVideo YoutubeLuckySearch()
        {
            YoutubeVideo videoSample = new YoutubeVideo()
            {
                kind = "",
                id = "YE7VzlLtp-4",
                statistics = new YoutubeStatistics(1000, 70, 3)
            };
            return videoSample;
        }
    }
}
