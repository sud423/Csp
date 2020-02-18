using Csp.Web.Extensions;

namespace Csp.Web
{
    /// <summary>
    /// 统一返回结果信息
    /// </summary>
    public class OptResult
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="success"></param>
        /// <param name="msg"></param>
        public OptResult(bool success, string msg)
        {
            Succeed = success;
            Msg = msg;
        }

        /// <summary>
        /// 如果操作成功，则为true
        /// </summary>
        public bool Succeed { get; set; }

        /// <summary>
        /// 返回提示消息
        /// </summary>
        public string Msg { get; set; }

        public override string ToString()
        {
            return this.ToJson();
        }

        /// <summary>
        /// 静态返回成功方法
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static OptResult Success(string msg = "操作成功")
        {
            return new OptResult(true, msg);
        }

        /// <summary>
        /// 静态添加失败
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static OptResult Failed(string msg = "操作失败")
        {
            return new OptResult(false, msg);
        }
    }
}
