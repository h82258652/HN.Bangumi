namespace HN.Bangumi.API.Models
{
    public static class SubjectTypeExtensions
    {
        public static string GetValue(this SubjectType subjectType)
        {
            switch (subjectType)
            {
                case SubjectType.Book:
                    return "book";

                case SubjectType.Anime:
                    return "anime";

                case SubjectType.Music:
                    return "music";

                case SubjectType.Game:
                    return "game";

                case SubjectType.Real:
                    return "real";

                default:
                    return subjectType.ToString();
            }
        }
    }
}
