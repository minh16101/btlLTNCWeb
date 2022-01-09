using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ThiHocKy.Models;

namespace ThiHocKy.Controllers
{
    public class GVGiangVienOnlyController : ApiController
    {
        // GET: api/GVGiangVienOnly
        MongoDBConnect connect = new MongoDBConnect("ThiHocKy");
        public IEnumerable<giangvienonly> Get()
        {
            return connect.DisplayAll<giangvienonly>("giangvienonly", "MaGV");
        }

        // GET: api/GVLopHoc/5
        [Route("getValueGiangVienOnly/{idGV}")]
        public giangvienonly Get(int idGV)
        {
            return connect.DisplayOne<giangvienonly, int>("giangvienonly", idGV, "MaGV");
        }

        // POST: api/GVLopHoc
        [Route("api/GVGiangVienOnly/P")]
        public bool Post([FromBody] giangvienonly value)
        {
            bool check = true;
            giangvienonly gv = connect.DisplayOne<giangvienonly, int>("giangvienonly", value.MaGV, "MaGV");
            if (gv != null)
            {
                check = false;
            }
            else
            {
                connect.InsertDB("giangvienonly", value);
            }
            return check;
        }
    }
}
