using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pd.Core.Areas.Pd.Models.Base;
using Pd.Core.Areas.Pd.Models.BaseModel;
using Pd.Core.Helper;
using Pd.Core.Helper.Attribute;
using Pd.Core.Models;


namespace Pd.Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly PDBaseContext _pDBaseContext;

        public HomeController(PDBaseContext pDBaseContext)
        {
            _pDBaseContext = pDBaseContext;
        }
        [Authorize]
        public IActionResult Index()
        {
          string sss= User.FindFirstValue(ClaimTypes.Sid);//获取ID
            return View();
        }
       
        public IActionResult Login()
        {
            return View();
        }
       
        public JsonResult LoginChecking(string code, string pwd)
        {
            if(string.IsNullOrEmpty(code) ||string.IsNullOrEmpty(pwd))
            {
                return Json(new { success = 0, message = "参数为空!" });
            }
            string Pwd_ = Encryption.GenerateMD5(pwd);
            var userInfo = _pDBaseContext.UserInfo.Where(p => p.UserCode == code && p.UserPwd == Pwd_).FirstOrDefault();
            if (userInfo != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity("Forms");

                identity.AddClaim(new Claim(ClaimTypes.Sid, userInfo.UserId));
                identity.AddClaim(new Claim(ClaimTypes.Name, userInfo.UserCode));

                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            }
            return Json(new { success = 1, message = "OK" });
        }
        public IActionResult Error()
        {
            return View();
        }
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public JsonResult Logout()
        {
            HttpContext.SignOutAsync().Wait();//注销
            return Json(new { success = 1, message = "OK" });
        }
    }
}
