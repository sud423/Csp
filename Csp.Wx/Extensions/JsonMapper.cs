using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Csp.Wx.Extensions
{
    static class JsonMapper
    {
        /// <summary>
        /// 将对象序列化成json格式字符串
        /// </summary>
        /// <param name="t">待序列化对象</param>
        /// <returns></returns>
        internal static string ToJson<T>(this T t)
        {
            var options = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),

                Formatting = Formatting.Indented,

                DateFormatString = "yyyy-MM-dd HH:mm:ss"
            };
            string json = JsonConvert.SerializeObject(t, options);
            return json;

        }

        /// <summary>
        /// 将json格式的字符串反序列化为对象
        /// </summary>
        /// <param name="json">待反序列化字符串</param>
        /// <param name="parentToken">父节点路径，多级用'.'</param>
        /// <returns></returns>
        internal static T FromJson<T>(this string json, string parentToken = null)
        {
            var jsonToParse = string.IsNullOrEmpty(parentToken) ? json : JObject.Parse(json).SelectToken(parentToken).ToString();

            return JsonConvert.DeserializeObject<T>(jsonToParse);
        }

        /// <summary>
        /// 根据json key获取相应的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nodeKey">
        /// 节点key
        /// 如：json.GetValue<string>("root3")
        /// json.GetValue<string>("root.node1")
        /// 中间用‘.’来区别层级
        /// </param>
        /// <returns></returns>
        internal static T GetValue<T>(this string json, string nodeKey)
        {
            var val = JObject.Parse(json).SelectToken(nodeKey).Value<T>();
            return val;
        }
    }
}
