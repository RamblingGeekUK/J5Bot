﻿using System;

namespace RG
{
    public static class Settings
    {
        public static string Twitch_channel = Environment.GetEnvironmentVariable("Twitch_channel");
        public static string Twitch_botusername = Environment.GetEnvironmentVariable("Twitch_botusername");
        public static string Twitch_token = Environment.GetEnvironmentVariable("Twitch_token");
    }
}

