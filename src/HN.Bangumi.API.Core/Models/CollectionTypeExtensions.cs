namespace HN.Bangumi.API.Models
{
    public static class CollectionTypeExtensions
    {
        public static string GetValue(this CollectionType collectionType)
        {
            switch (collectionType)
            {
                case CollectionType.Watching:
                    return "watching";

                case CollectionType.AllWatching:
                    return "all_watching";

                default:
                    return collectionType.ToString();
            }
        }
    }
}
