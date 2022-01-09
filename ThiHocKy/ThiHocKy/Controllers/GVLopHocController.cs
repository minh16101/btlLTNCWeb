using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using ThiHocKy.Models;
using MongoDB.Driver;

namespace ThiHocKy.Controllers
{
    public class GVLopHocController : ApiController
    {
        MongoDBConnect connect = new MongoDBConnect("ThiHocKy");
        // GET: api/GVLopHoc
        public IEnumerable<lophoc> Get()
        {
            return connect.DisplayAll<lophoc>("lophoc", "MaLH");
        }

        // GET: api/GVLopHoc/5
        [Route("getValue/{MaLH}")]
        public object Get(int MaLH)
        {
            return connect.DisplayOne<lophoc, int>("lophoc", MaLH, "MaLH");
        }

        // POST: api/GVLopHoc
        [Route("api/GVLopHoc/P")]
        public string Post([FromBody] lophoc value)
        {
            lophoc lh = connect.DisplayOne<lophoc, int>("lophoc", value.MaLH, "MaLH");
            giangvienonly gv = connect.DisplayOne<giangvienonly, int>("giangvienonly", value.MaGV, "MaGV");
            if (lh != null)
            {
                return "Lớp học này đã tồn tại!";
            }
            else
            {
                if (gv == null)
                {
                    return "Không có mã giảng viên này!";
                }
                else
                {
                    connect.InsertDB<lophoc>("lophoc", value);
                }
            }
            return "Thành công!";
        }
        [Route("api/GVLopHoc/P")]
        // PUT: api/GVLopHoc/5
        public string Put([FromBody] lophoc value)
        {
            giangvienonly gv = connect.DisplayOne<giangvienonly, int>("giangvienonly", value.MaGV, "MaGV");
            if (gv == null)
            {
                return "Không có mã giảng viên này!";
            }
            else
            {
                var db = connect.Getdb();
                var collection = db.GetCollection<lophoc>("lophoc");
                var filter = Builders<lophoc>.Filter.Eq("MaLH", value.MaLH);
                var data = Builders<lophoc>.Update
                    .Set("TenLH", value.TenLH)
                    .Set("MaGV", value.MaGV)
                    .Set("MaHP", value.MaHP);
                collection.UpdateOne(filter, data);
            }

            return "Thành công!";
        }
        [Route("api/GVLopHoc/Delete/{value}")]
        // DELETE: api/GVLopHoc/5
        public void Delete(int value)
        {
            connect.DeleteDB<lophoc>("lophoc", value, "MaLH");
        }
    }
}
