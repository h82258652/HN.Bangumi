namespace HN.Bangumi.API.Models
{
    public static class ResponseGroupExtensions
    {
        public static string GetValue(this ResponseGroup responseGroup)
        {
            switch (responseGroup)
            {
                case ResponseGroup.Small:
                    return "small";

                case ResponseGroup.Medium:
                    return "medium";

                case ResponseGroup.Large:
                    return "large";

                default:
                    return responseGroup.ToString();
            }
        }
    }
}
