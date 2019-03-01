namespace HN.Bangumi.API.Models
{
    public static class EpStatusExtensions
    {
        public static string GetValue(this EpStatus status)
        {
            switch (status)
            {
                case EpStatus.Watched:
                    return "watched";

                case EpStatus.Queue:
                    return "queue";

                case EpStatus.Drop:
                    return "drop";

                case EpStatus.Remove:
                    return "remove";

                default:
                    return status.ToString();
            }
        }
    }
}
