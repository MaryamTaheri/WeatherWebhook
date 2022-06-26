namespace WeatherWebhook.Domain.Events;

public static class Globals
{
    public static class Channels
    {
        public const string WeatherChannel = "WEATHER";
    }

    public static class Events
    {
        public static class Routes
        {
            public const string WeatherRoute = "ICX_ALBB_WEATHER";
        }

        public const string NotificationsBus = "NOTIFICATIONS";
        public const string StateChangesBus = "STATE_CHANGES";
    }

    public static class WeatherApi
    {
        public const string WeatherArea = "weather";
    }
}