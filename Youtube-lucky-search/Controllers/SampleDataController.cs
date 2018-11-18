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
        private static Random _rand = new Random();
        [HttpGet("[action]")]
        public YoutubeVideo YoutubeLuckySearch(string videoLength, string views, string rating)
        {
            var LuckyParams = ParseParams(videoLength, views, rating);

            var videoList = GetVideos(LuckyParams);
            if (videoList.Count() == 0)
            {
                var rickRoll = new YoutubeVideo("dQw4w9WgXcQ");
                rickRoll.statistics = new YoutubeStatistics(0, 0, 0);
                return rickRoll;
            }
            //ThreadLocalRandom.Instance.Next()
            int luckyID = _rand.Next(0, videoList.Count());

            return videoList[luckyID];
        }

        private List<YoutubeVideo> GetVideos(LuckySetup luckyParams)
        {
            var videoList = PrepareSampleVideos();

            return videoList.Where(v=>IsLucky(luckyParams.minRating, luckyParams.maxRating, v.statistics.rating) && IsLucky(luckyParams.minViews, luckyParams.maxViews, v.statistics.viewCount)).ToList();
        }

        private bool IsLucky(int min, int  max, int value)
        {
            return min <= value && value <= max ? true : false;
        }

        private LuckySetup ParseParams(string videoLength, string views, string rating)
        {
            var lucky = new LuckySetup();

            switch(videoLength)
            {
                case ("Short"):
                    {
                        lucky.videoLength = "short";
                        break;
                    }
                case ("Medium"):
                    {
                        lucky.videoLength = "medium";
                        break;
                    }
                case ("Long"):
                    {
                        lucky.videoLength = "long";
                        break;
                    }
                default:
                    {
                        lucky.videoLength = "any";
                        break;
                    }
            }

            switch (views)
            {
                case ("Private"):
                    {
                        lucky.minViews = 1;
                        lucky.maxViews = 4999;
                        break;
                    }
                case ("Niche"):
                    {
                        lucky.minViews = 5000;
                        lucky.maxViews = 49999;
                        break;
                    }
                case ("Popular"):
                    {
                        lucky.minViews = 50000;
                        lucky.maxViews = 499999;
                        break;
                    }
                case ("Viral"):
                    {
                        lucky.minViews = 500000;
                        lucky.maxViews = 4999999;
                        break;
                    }
                case ("OMG"):
                    {
                        lucky.minViews = 5000000;
                        lucky.maxViews = 999999999;
                        break;
                    }
                default:
                    {
                        lucky.minViews = 1;
                        lucky.maxViews = 999999999;
                        break;
                    }
            }

            switch (rating)
            {
                case ("Disaster"):
                    {
                        lucky.minRating = -99999999;
                        lucky.maxRating = 39;
                        break;
                    }
                case ("Soso"):
                    {
                        lucky.minRating = 40;
                        lucky.maxRating = 70;
                        break;
                    }
                case ("Good"):
                    {
                        lucky.minRating = 71;
                        lucky.maxRating = 90;
                        break;
                    }
                case ("Awesome"):
                    {
                        lucky.minRating = 91;
                        lucky.maxRating = 100;
                        break;
                    }
                default:
                    {
                        lucky.minRating = -99999999;
                        lucky.maxRating = 100;
                        break;
                    }
            }

            return lucky;
        }

        private List<YoutubeVideo> PrepareSampleVideos()
        {
            return new List<YoutubeVideo>()
            {
                new YoutubeVideo()
            {
                kind = "",
                id = "YE7VzlLtp-4",
                statistics = new YoutubeStatistics(100, 70, 3)
            },
                  new YoutubeVideo()
            {
                kind = "",
                id = "YE7VzlLtp-4",
                statistics = new YoutubeStatistics(10, 70, 98)
            },
                    new YoutubeVideo()
            {
                kind = "",
                id = "YE7VzlLtp-4",
                statistics = new YoutubeStatistics(1000, 70, 23)
            },
                      new YoutubeVideo()
            {
                kind = "",
                id = "YE7VzlLtp-4",
                statistics = new YoutubeStatistics(51000, 70, 3)
            },
                        new YoutubeVideo()
            {
                kind = "",
                id = "YE7VzlLtp-4",
                statistics = new YoutubeStatistics(100000, 70, 50)
            },
                          new YoutubeVideo()
            {
                kind = "",
                id = "YE7VzlLtp-4",
                statistics = new YoutubeStatistics(3000000, 70, 3)
            },
                            new YoutubeVideo()
            {
                kind = "",
                id = "YE7VzlLtp-4",
                statistics = new YoutubeStatistics(250000, 70, 8)
            },
                              new YoutubeVideo()
            {
                kind = "",
                id = "YE7VzlLtp-4",
                statistics = new YoutubeStatistics(14000, 70, 3)
            },
                                new YoutubeVideo()
            {
                kind = "",
                id = "YE7VzlLtp-4",
                statistics = new YoutubeStatistics(140000, 70, 20)
            },
                                  new YoutubeVideo()
            {
                kind = "",
                id = "YE7VzlLtp-4",
                statistics = new YoutubeStatistics(750000, 70, 40)
            },
            };
        }
    }

    public class LuckySetup
    {
        public string videoLength;
        public int minViews;
        public int maxViews;
        public int minRating;
        public int maxRating;
    }

}
