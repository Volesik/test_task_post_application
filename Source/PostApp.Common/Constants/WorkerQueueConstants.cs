namespace PostApp.Common.Constants;

public static class WorkerQueueConstants
{
    public const string DefaultQueueName = "default";
    public const string UserQueueName = "userimport";
    public const string PostQueueName = "downloadphoto";

    public static readonly List<string> QueuesForRegistration = new()
    {
        DefaultQueueName,
        UserQueueName,
        PostQueueName
    };
}