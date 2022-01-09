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
    public class GVGiangVienController : ApiController
    {
        MongoDBConnect connect = new MongoDBConnect("ThiHocKy");
        public IEnumerable<giangvien> Get()
        {
            return connect.DisplayAll<giangvien>("giangvien", "MaLT");
        }

        // GET: api/GVLopHoc/5
        [Route("getValueGiangVien/{idGV}")]
        public giangvien Get(int idGV)
        {
            return connect.DisplayOne<giangvien, int>("giangvien", idGV, "MaGV");
        }

        // POST: api/GVLopHoc
        [Route("api/GVGiangVien/P")]
        public string Post([FromBody] giangvien value)
        {
            giangvienonly gv = connect.DisplayOne<giangvienonly, int>("giangvienonly", value.MaGV, "MaGV");
            lopthi lt = connect.DisplayOne<lopthi, int>("lopthi", value.MaLT, "MaLT");
            var db = connect.Getdb();
            var collection = db.GetCollection<giangvien>("giangvien");
            List<giangvien> list_gvlt = collection.Find<giangvien>(x => x.MaLT == value.MaLT).ToList();
            if (list_gvlt.Count() >= 2)
            {
                return "Lớp thi này đã đủ giảng viên trông thi!";
            }
            else
            {
                if (lt == null || gv == null || gv.TenGV != value.TenGV)
                {
                    return "Dữ liệu bạn nhập không thỏa mãn";
                }
                else
                {
                    connect.InsertDB("giangvien", value);
                }
            }
            return "Thành công!";
        }
        [Route("api/GVGiangVien/P")]
        // PUT: api/GVLopHoc/5
        public string Put([FromBody] giangvien value)
        {
            var gv = connect.DisplayOne<giangvienonly, int>("giangvienonly", value.MaGV, "MaGV");
            if (gv == null)
            {
                return "Dữ liệu giảng viên này không tồn tại!";
            }
            else
            {
                var db = connect.Getdb();
                var collection = db.GetCollection<giangvien>("giangvien");
                List<giangvien> gv_temp = collection.Find(x => x.MaGV == value.MaGV).ToList();
                giangvien gvien = gv_temp.Find(x => x.MaLT == value.MaLT);
            var filter = Builders<giangvien>.Filter.Eq("MaLT", gvien.MaLT);
            var data = Builders<giangvien>.Update
                .Set("MaGV", gvien.MaGV)
                .Set("TenGV", gvien.TenGV);
            collection.UpdateOne(filter, data);
        }
            return "Thành công!";
        }
        [Route("api/GVGiangVien/Delete/{MaGV}")]
        // DELETE: api/GVLopHoc/5
        public void Delete(int MaGV)
        {
            connect.DeleteDB<giangvien>("giangvien", MaGV, "MaGV");
        }
    }
}
