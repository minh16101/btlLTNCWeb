using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ThiHocKy.Models;
namespace ThiHocKy.Controllers
{
    public class LoginController : ApiController
    {
        MongoDBConnect connect = new MongoDBConnect("ThiHocKy");
        // GET: api/Login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        [Route("getvalueLogin/{email}/{password}")]

        public string Get(string email, string password)
        {
            string username = email;
            int count = username.Count(a => a == '@');
            int index = username.IndexOfAny(new[] { '@' });
            string name = username.Remove(0, index);
            if (count != 1 || !name.Equals("@sis.hust.edu.vn"))
            {
                return "Email sai định dạng!";
            }
            else
            {
                Login log = connect.DisplayOne<Login, string>("login", email, "Email");
                if (log == null)
                {
                    return "Sai tên email!";
                }
                else
                {
                    if (password != log.PassWord)
                    {
                        return "Sai mật khẩu!";
                    }
                }
            }

            return "OK!";
        }
        // POST: api/Login
        [Route("api/GVLogin/P")]
        public void Post([FromBody] Login value)
        {
            connect.InsertDB("login", value);
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}
