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
    public class GVLopThiController : ApiController
    {
        MongoDBConnect connect = new MongoDBConnect("ThiHocKy");
        public IEnumerable<lopthi> Get()
        {
            return connect.DisplayAll<lopthi>("lopthi", "MaLT");
        }
        // GET: api/GVLopHoc/5
        [Route("getValueLopThi/{MaLT}")]
        public object Get(int MaLT)
        {
            return connect.DisplayOne<lopthi, int>("lopthi", MaLT, "MaLT");
        }

        // POST: api/GVLopHoc
        [Route("api/GVLopThi/P")]
        public string Post([FromBody] lopthi value)
        {
            giangvienonly gv1 = connect.DisplayOne<giangvienonly, int>("giangvienonly", value.MaGV1, "MaGV");
            giangvienonly gv2 = connect.DisplayOne<giangvienonly, int>("giangvienonly", value.MaGV2, "MaGV");
            lophoc lh = connect.DisplayOne<lophoc, int>("lophoc", value.MaLH, "MaLH");
            lopthi lt = connect.DisplayOne<lopthi, int>("lopthi", value.MaLT, "MaLT");
            if (lt != null)
            {
                return "Lớp thi này đã tồn tại!";
            }
            else
            {
                if (lh == null || gv1 == null || gv2 == null)
                {
                    return "Dữ liệu bạn nhập không thỏa mãn!";
                }
                else
                {
                    connect.InsertDB("lopthi", value);
                }
            }
            return "Thành công!";
        }
        [Route("api/GVLopThi/P")]
        // PUT: api/GVLopHoc/5
        public string Put([FromBody] lopthi value)
        {
            var gv1 = connect.DisplayOne<giangvienonly, int>("giangvienonly", value.MaGV1, "MaGV");
            var gv2 = connect.DisplayOne<giangvienonly, int>("giangvienonly", value.MaGV2, "MaGV");
            lophoc lh = connect.DisplayOne<lophoc, int>("lophoc", value.MaLH, "MaLH");
            if (gv1 == null || gv2 == null || lh == null)
            {
                return "Dữ liệu nhập không thỏa mãn!";
            }
            else
            {
                var db = connect.Getdb();
                var collection = db.GetCollection<lopthi>("lopthi");
                var filter = Builders<lopthi>.Filter.Eq("MaLT", value.MaLT);
                var data = Builders<lopthi>.Update
                    .Set("MaLH", value.MaLH)
                    .Set("SoGV", value.SoGV)
                    .Set("MaGV1", value.MaGV1)
                    .Set("MaGV2", value.MaGV2)
                    .Set("Time", value.Time)
                    .Set("Location", value.Location)
                    .Set("HinhThuc", value.HinhThuc);
                collection.UpdateOne(filter, data);
            }
            return "Thành công!";
        }
        [Route("api/GVLopThi/Delete/{value}")]
        // DELETE: api/GVLopHoc/5
        public void Delete(int value)
        {
            connect.DeleteDB<lopthi>("lopthi", value, "MaLT");
        }
    }
}
