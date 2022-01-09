using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ThiHocKy.Models;

namespace ThiHocKy.Controllers
{
    public class GVSinhVienOnlyController : ApiController
    {
        // GET: api/GVGiangVienOnly
        MongoDBConnect connect = new MongoDBConnect("ThiHocKy");
        public IEnumerable<sinhvienonly> Get()
        {
            return connect.DisplayAll<sinhvienonly>("sinhvienonly", "MSSV");
        }

        // GET: api/GVLopHoc/5
        [Route("getValueSinhVienOnly/{MSSV}")]
        public sinhvienonly Get(int MSSV)
        {
            return connect.DisplayOne<sinhvienonly, int>("sinhvienonly", MSSV, "MSSV");
        }

        // POST: api/GVLopHoc
        [Route("api/GVSinhVienOnly/P")]
        public bool Post([FromBody] sinhvienonly value)
        {
            bool check = true;
            sinhvienonly sv = connect.DisplayOne<sinhvienonly, int>("sinhvienonly", value.MSSV, "MSSV");
            if (sv != null)
            {
                check = false;
            }
            else
            {
                connect.InsertDB("sinhvienonly", value);
            }
            return check;
        }
    }
}
