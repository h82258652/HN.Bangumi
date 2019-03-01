namespace HN.Bangumi.API.Models
{
    public static class CollectionStatusExtensions
    {
        public static string GetValue(this CollectionStatus status)
        {
            switch (status)
            {
                case CollectionStatus.Wish:
                    return "wish";

                case CollectionStatus.Collect:
                    return "collect";

                case CollectionStatus.Do:
                    return "do";

                case CollectionStatus.OnHold:
                    return "on_hold";

                case CollectionStatus.Dropped:
                    return "dropped";

                default:
                    return status.ToString();
            }
        }
    }
}
