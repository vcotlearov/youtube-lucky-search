using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Youtube_lucky_search.Model;
using Google.Apis.YouTube.v3;
using Google.Apis.Services;

namespace Youtube_lucky_search.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static Random _rand = new Random();
        [HttpGet("[action]")]
        public YoutubeVideo YoutubeLuckySearch(string keywords, string videoLength, string views, string rating)
        {
            var LuckyParams = ParseParams(keywords, videoLength, views, rating);

            var videoList = GetVideos(LuckyParams);
            if (videoList.Count() == 0)
            {
                var rickRoll = new YoutubeVideo("dQw4w9WgXcQ"); // the one and famous ID just for every taste ;)
                rickRoll.statistics = new YoutubeStatistics(0, 0, 0);
                return rickRoll;
            }
            int luckyID = _rand.Next(0, videoList.Count());

            return videoList[luckyID];
        }

        List<YoutubeVideo> videoList;

        private List<YoutubeVideo> GetVideos(LuckySetup luckyParams)
        {
            videoList = new List<YoutubeVideo>();
            try
            {
                GetYoutubeVideos(luckyParams).Wait();
            }
            catch (AggregateException ex)
            {
                List<string> errors = new List<string>();
                foreach (var e in ex.InnerExceptions)
                {
                    errors.Add(e.Message);
                }
            }
            return videoList.Where(v=>IsLucky(luckyParams.minRating, luckyParams.maxRating, v.statistics.rating) && IsLucky(luckyParams.minViews, luckyParams.maxViews, v.statistics.viewCount)).ToList();
        }

        private async Task GetYoutubeVideos(LuckySetup luckyParams)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "YOUR_API_KEY",
                ApplicationName = this.GetType().ToString()
            });

            //Search for videos first
            var videoListRequest = youtubeService.Search.List("id");
            videoListRequest.Q = luckyParams.keywords;
            videoListRequest.MaxResults = 50;
            videoListRequest.Type = "video";
            videoListRequest.VideoDuration = luckyParams.videoLength;
            videoListRequest.VideoEmbeddable = SearchResource.ListRequest.VideoEmbeddableEnum.True__;

            //This is "random" part of the application as youtube API does not outright support the random feature
            //If neccessary can be further refined by randomizing the pick of Order type as well the applicability of particular params
            videoListRequest.Order = SearchResource.ListRequest.OrderEnum.Date;
            videoListRequest.PublishedBefore = RandomDay();

            var videoListResponse = await videoListRequest.ExecuteAsync();
            var videoIDs = videoListResponse.Items.Select(a=>a.Id.VideoId);

            //Then retreive statistics for each video
            var searchListRequest = youtubeService.Videos.List("id, statistics");
            searchListRequest.Id = videoIDs.Aggregate("", (a,b)=> a += b + ",").TrimEnd(',');

            var searchListResponse = await searchListRequest.ExecuteAsync();

            foreach (var searchResult in searchListResponse.Items)
            {
                var tmpVideo = new YoutubeVideo(searchResult.Id);
                tmpVideo.statistics = new YoutubeStatistics(searchResult.Statistics.ViewCount, searchResult.Statistics.LikeCount, searchResult.Statistics.DislikeCount);
                videoList.Add(tmpVideo);
            }
        }

        /// <summary>
        /// Stone-age randomizer - at least you will hardly ever see a duplicate, right? :)
        /// </summary>
        /// <returns></returns>
        private DateTime RandomDay()
        {
            DateTime start = new DateTime(2015, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(_rand.Next(range));
        }

        private bool IsLucky(ulong? min, ulong? max, ulong? value)
        {
            return min <= value && value <= max ? true : false;
        }

        private LuckySetup ParseParams(string keywords, string videoLength, string views, string rating)
        {
            var lucky = new LuckySetup
            {
                keywords = keywords
            };

            switch (videoLength)
            {
                case ("Short"):
                    {
                        lucky.videoLength = SearchResource.ListRequest.VideoDurationEnum.Short__;
                        break;
                    }
                case ("Medium"):
                    {
                        lucky.videoLength = SearchResource.ListRequest.VideoDurationEnum.Medium;
                        break;
                    }
                case ("Long"):
                    {
                        lucky.videoLength = SearchResource.ListRequest.VideoDurationEnum.Long__;
                        break;
                    }
                default:
                    {
                        lucky.videoLength = SearchResource.ListRequest.VideoDurationEnum.Any;
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
                        lucky.minRating = 0;
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
                        lucky.minRating = 0;
                        lucky.maxRating = 100;
                        break;
                    }
            }

            return lucky;
        }
    }

    public class LuckySetup
    {
        public string keywords;
        public SearchResource.ListRequest.VideoDurationEnum videoLength;
        public ulong? minViews;
        public ulong? maxViews;
        public ulong? minRating;
        public ulong? maxRating;
    }

}
