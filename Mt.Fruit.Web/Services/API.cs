namespace Mt.Fruit.Web.Services
{
    public static partial class API
    {
        public static class Category
        {
            public static string GetCategories(string baseUrl, string type) => $"{baseUrl}/categories/2/3/{type}";
            
            public static string GetCategory(string baseUrl, int id) => $"{baseUrl}/categories/find/{id}";

            public static string GetHotCategories(string baseUrl, string type) => $"{baseUrl}/categories/hot/2/3/{type}";

        }
        public static class Article
        {
            public static string Create(string baseUrl) => $"{baseUrl}/article/create";

            public static string Delete(string baseUrl, int id) => $"{baseUrl}/article/delete/{id}";

            public static string GetArticles(string baseUrl, int categoryId,int page, int size) => $"{baseUrl}/articles/2/{categoryId}/3/{page}/{size}";

        }


        public static class Auth
        {
            public static string Create(string baseUrl) => $"{baseUrl}/create";

            public static string UserLogin(string baseUrl) => $"{baseUrl}/signin";
        }

    }
}
