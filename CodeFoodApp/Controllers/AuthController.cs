using CodeFoodApp.DTO;
using CodeFoodApp.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFoodApp.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        public AuthController(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] LoginParameter param)
        {

            if (string.IsNullOrEmpty(param.username))
            {


                Response.StatusCode = ErrorStatus.REQUIRED_USERNAME;

                ResultDynamicBad result = new ResultDynamicBad();
                result.success = false;
                result.message = MessageWording.REQUIRED_USERNAME;
                return BadRequest(result);
            }
            else if (string.IsNullOrEmpty(param.password))
            {
                Response.StatusCode = ErrorStatus.REQUIRED_PASSWORD;

                ResultDynamicBad result = new ResultDynamicBad();
                result.success = false;
                result.message = MessageWording.REQUIRED_PASSWORD;
                return BadRequest(result);
            }
            else
            {
                var token = jwtAuthenticationManager.Authenticate(param.username, param.password);
                if (token == null)
                {
                    ResultDynamicBad resultBad = new ResultDynamicBad();
                    resultBad.success = false;
                    resultBad.message = MessageWording.INVALID_USERNAME_PASSWORD;
                    Response.StatusCode = ErrorStatus.INVALID_USERNAME_PASSWORD;

                }
                AuthObject authobj = new AuthObject();
                authobj.token = token;

                Response.StatusCode = ErrorStatus.SUCCESS;

                ResultDynamicOK result = new ResultDynamicOK();
                result.data = authobj;
                result.success = true;
                result.message = MessageWording.SUCCESS;
                return Ok(result);
            }
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register([FromBody] LoginParameter param)
        {

            if (string.IsNullOrEmpty(param.username))
            {


                Response.StatusCode = ErrorStatus.REQUIRED_USERNAME;

                ResultDynamicBad result = new ResultDynamicBad();
                result.success = false;
                result.message = MessageWording.REQUIRED_USERNAME;
                return BadRequest(result);
            }
            else if (param.password.Length < 6)
            {
                Response.StatusCode = ErrorStatus.PASSWORD_MINIMUM;

                ResultDynamicBad result = new ResultDynamicBad();
                result.success = false;
                result.message = MessageWording.PASSWORD_MINIMUM;
                return BadRequest(result);
            }
            else
            {
                UserObject userobj = new UserObject();

                Response.StatusCode = ErrorStatus.SUCCESS;

                ResultDynamicOK result = new ResultDynamicOK();
                result.data = userobj;
                result.success = true;
                result.message = MessageWording.SUCCESS;
                return Ok(result);
            }
        }
    }
}
