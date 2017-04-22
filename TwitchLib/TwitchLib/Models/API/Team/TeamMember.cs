﻿using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.API.Team
{
    /// <summary>Class representing a team member as returned by Twitch API.</summary>
    public class TeamMember
    {
        /// <summary>Property representing whether streamer is live.</summary>
        public bool IsLive { get; protected set; }
        /// <summary>Property representing the various image sizes.</summary>
        public ImageSizes ImageSizes { get; protected set; }
        /// <summary>Property representing the current viewer count.</summary>
        public int CurrentViews { get; protected set; }
        /// <summary>Property representing the current follower count.</summary>
        public int FollowerCount { get; protected set; }
        /// <summary>Property representing the total view count.</summary>
        public int TotalViews { get; protected set; }
        /// <summary>Property representing the channel description.</summary>
        public string Description { get; protected set; }
        /// <summary>Property representing the streamer customized display name.</summary>
        public string DisplayName { get; protected set; }
        /// <summary>Property representing the link to the channel.</summary>
        public string Link { get; protected set; }
        /// <summary>Property representing the meta game of the channel.</summary>
        public string MetaGame { get; protected set; }
        /// <summary>Property representing the name of the channel.</summary>
        public string Name { get; protected set; }
        /// <summary>Property representing the title of the channel.</summary>
        public string Title { get; protected set; }

        /// <summary>TeamMember constructor.</summary>
        public TeamMember(JToken data)
        {
            int currentViews, followerCount, totalViews;

            if (int.TryParse(data.SelectToken("current_viewers").ToString(), out currentViews)) CurrentViews = currentViews;
            if (int.TryParse(data.SelectToken("followers_count").ToString(), out followerCount)) FollowerCount = followerCount;
            if (int.TryParse(data.SelectToken("total_views").ToString(), out totalViews)) TotalViews = totalViews;

            if (data.SelectToken("status").ToString().Trim().ToLower() == "live")
                IsLive = true;

            Description = data.SelectToken("description").ToString();
            DisplayName = data.SelectToken("display_name").ToString();
            ImageSizes = new ImageSizes(data.SelectToken("image"));
            Link = data.SelectToken("link").ToString();
            MetaGame = data.SelectToken("meta_game").ToString();
            Name = data.SelectToken("name").ToString();
            Title = data.SelectToken("title").ToString();
        }
    }
}