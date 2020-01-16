using Csp.Web.Extensions;
using Csp.Wx.Api.Models;
using Csp.Wx.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace Csp.Wx.Api.Controllers
{
    [Route("api/v1/wx")]
    public class WeiXinController : ControllerBase
    {
        readonly IWxService _wxService;

        ILogger _logger;

        public WeiXinController(IWxService wxService,ILogger<WeiXinController> logger)
        {
            _wxService = wxService;
            _logger = logger;
        }


        public IActionResult Index()
        {
            var fullPath = Path.GetFullPath("logs/log20180223.log");

            var ext = Path.GetExtension(fullPath);
            var filePath = fullPath.Replace(ext, $"xxx{ext}");

            _logger.LogInformation(fullPath);
            _logger.LogError(ext);
            _logger.LogDebug(filePath);

            return Ok($"FullPath:{fullPath}\nDirectoryName:{Path.GetDirectoryName(fullPath)}\nFileName:{filePath}");
        }

        /// <summary>
        /// 获取微信用户同意授权地址
        /// </summary>
        /// <param name="url">回调地址</param>
        /// <param name="state">状态码，为空时值为：csp</param>
        /// <returns></returns>
        [Route("get"),HttpGet]
        public IActionResult GetAuthUrl(string url,string state=null)
        {
            if (string.IsNullOrEmpty(url))
                return BadRequest(OptResult.Failed("回调地址不能为空"));

            return Content(_wxService.GetAuthUrl(url, state));
        }

        /// <summary>
        /// 拉取用户信息
        /// </summary>
        /// <param name="code">授权码</param>
        /// <returns></returns>
        [Route("getsnsuser/{code}")]
        [HttpGet]
        public async Task<ActionResult<WxUser>> GetWxUser(string code)
        {
            if (string.IsNullOrEmpty(code))
                return BadRequest(OptResult.Failed("授权码不能为空"));

            return await _wxService.GetSnsApiUserInfo(code);
        }

        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <param name="model">模板信息</param>
        /// <returns></returns>
        [HttpPost,Route("send")]
        public async Task<IActionResult> SendTemp([FromBody]SendTemplateModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.ToFirstError());

            await _wxService.SendTempMsg(model.ToUser, model.TemplateId, model.Data, model.Url, model.Color);

            return Ok();
        }
    }
}
