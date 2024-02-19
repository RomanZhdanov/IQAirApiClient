namespace IQAirApiClient.Models
{
    public enum ReturnCodes
    {
        // returned when JSON file was generated successfully.
        Success, 
        // returned when minute/monthly limit is reached.
        CallLimitReached,
        // returned when API key is expired.
        ApiKeyExpired,
        // returned when using wrong API key.
        IncorrectApiKey, 
        // returned when service is unable to locate IP address of request.
        IpLocationFailed,
        // returned when there is no nearest station within specified radius.
        NoNearestStation,
        // returned when call requests a feature that is not available in chosen subscription plan.
        FeatureNotAvailable,
        // returned when more than 10 calls per second are made.
        TooManyRequests
    }
}