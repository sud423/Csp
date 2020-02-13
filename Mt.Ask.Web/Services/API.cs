namespace Mt.Ask.Web.Services
{
    public static partial class API
    {
        public static class Article
        {
            public static string GetArticle(string baseUrl) => $"{baseUrl}/articles/browse";

            public static string GetArticles(string baseUrl,int categoryId,int size) => $"{baseUrl}/articles/2/{categoryId}/1/{size}";

            public static string GetArticles(string baseUrl, int categoryId) => $"{baseUrl}/articles/2/{categoryId}/1";
        }

        public static class Announce
        {
            public static string GetAnnounce(string baseUrl,int id) => $"{baseUrl}/announces/find/{id}";

            public static string GetAnnounces(string baseUrl, int size) => $"{baseUrl}/announces/2?size={size}";
        }

        public static class Course
        {
            public static string GetCourse(string baseUrl, int id) => $"{baseUrl}/courses/find/{id}";

            public static string GetCourses(string baseUrl, int size) => $"{baseUrl}/courses/2/{size}";

            public static string GetCoursesByCondtion(string baseUrl, string academy, string classify) 
                => $"{baseUrl}/courses?tenantId=2&academy={academy}&classify={classify}";

            public static string GetHotCourses(string baseUrl) => $"{baseUrl}/courses/2";

        }

        public static class Auth
        {
            public static string GetAuthUrl(string baseUrl, string redirectUrl) => $"{baseUrl}/getauth?url={redirectUrl}";

            public static string GetUser(string baseUrl, string code) =>$"{baseUrl }/wxlogin/2/1/{code}";

            public static string BindCell(string baseUrl, string cell,int userId) => $"{baseUrl }/bind/{userId}/{cell}";

            public static string UserLogin(string baseUrl) => $"{baseUrl}/signin";
        }
    }
}
