using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Video.Watermark.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

//        public IActionResult ParseVideoUrl(string url)
//        {
//            var DOU_YIN_BASE_URL = "";
//            url = HttpUtility.UrlDecode(url).Replace("url=", "");

//            /**
//             * 1、短连接重定向后的 URL
//             */
//            String redirectUrl = getLocation(url);
//            /**
//             * 2、拿到视频对应的 ItemId
//             */
//            String videoUrl = "";
//            String musicUrl = "";
//            String videoPic = "";
//            String desc = "";

//            if (!string.IsNullOrEmpty(redirectUrl))
//            {
//                /**
//                * 3、用 ItemId 拿视频的详细信息，包括无水印视频url
//                * https://www.iesdouyin.com/web/api/v2/aweme/iteminfo/?item_ids=6820792802394262795
//                */
//                var itemidsStr = redirectUrl.Substring(redirectUrl.LastIndexOf('?') + 1);
//                String itemId = itemidsStr.Split('=')[1];
//                StringBuilder sb = new StringBuilder();

//                HttpClient client = new HttpClient();
//                var dyResult = client.GetAsync();

//                sb.append(DOU_YIN_BASE_URL).append(itemId);
//                String videoResult = CommonUtils.httpGet(sb.toString());
//                DYResult dyResult = JSON.parseObject(videoResult, DYResult.class);


//            }
//}


        public String getLocation(String url)
        {
            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                myHttpWebRequest.AllowAutoRedirect = false;
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                string location = myHttpWebResponse.Headers.Get("Location");
                return location;
            }
            catch (Exception e)
            {
                _logger.LogInformation($"重定向异常:{e.ToString() + e.StackTrace}");
            }
            return "";
        }

        public IActionResult Error()
        {
            //获取异常细节
            var exceptionHandler = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            ViewBag.ExceptionPath = exceptionHandler.Path;
            ViewBag.ExceptionMessage = exceptionHandler.Error.Message;
            ViewBag.StackTrace = exceptionHandler.Error.StackTrace;
            return View("~/Views/Shared/Error.cshtml");
        }

    }
}
