﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace TwitchLib.Common
{
    /// <summary>
    /// Static class of helper functions used around the project.
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Function that converts Image object to a base64 string.
        /// </summary>
        /// <param name="image">Image object represting the image to turn to base64 string.</param>
        /// <returns>Base64 string of image.</returns>
        public static string ImageToBase64(Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, image.RawFormat);
            return Convert.ToBase64String(ms.ToArray());
        }

        /// <summary>
        /// Function to check if a jtoken is null.
        /// Credits: http://stackoverflow.com/questions/24066400/checking-for-empty-null-jtoken-in-a-jobject
        /// </summary>
        /// <param name="token">JToken to check if null or not.</param>
        /// <returns>Boolean on whether true or not.</returns>
        public static bool JsonIsNullOrEmpty(JToken token)
        {
            return (token == null) ||
                   (token.Type == JTokenType.Array && !token.HasValues) ||
                   (token.Type == JTokenType.Object && !token.HasValues) ||
                   (token.Type == JTokenType.String && token.ToString() == String.Empty) ||
                   (token.Type == JTokenType.Null);
        }

        /// <summary>Takes date time string received from Twitch API and converts it to DateTime object.</summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime DateTimeStringToObject(string dateTime)
        {
            if (dateTime == null)
                return new DateTime();
            return Convert.ToDateTime(dateTime);
        }

        /// <summary>
        /// Parses out strings that have quotes, ideal for commands that use quotes for parameters
        /// </summary>
        /// <param name="message">Input string to attempt to parse.</param>
        /// <returns>List of contents of quotes from the input string</returns>
        public static List<string> ParseQuotesAndNonQuotes(string message)
        {
            List<string> args = new List<string>();

            // Return if empty string
            if (message == "")
                return new List<string>();

            bool previousQuoted = message[0] != '"';
            // Parse quoted text as a single argument
            foreach (string arg in message.Split('"'))
            {
                if (string.IsNullOrEmpty(arg))
                    continue;

                // This arg is a quoted arg, add it right away
                if (!previousQuoted)
                {
                    args.Add(arg);
                    previousQuoted = true;
                    continue;
                }

                if (!arg.Contains(" "))
                    continue;

                // This arg is non-quoted, iterate through each split and add it if it's not empty/whitespace
                foreach (string dynArg in arg.Split(' '))
                {
                    if (string.IsNullOrWhiteSpace(dynArg))
                        continue;

                    args.Add(dynArg);
                    previousQuoted = false;
                }
            }
            return args;
        }
    }
}
