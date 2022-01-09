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
    public class GVSinhVienController : ApiController
    {
        MongoDBConnect connect = new MongoDBConnect("ThiHocKy");
        public IEnumerable<sinhvien> Get()
        {
            return connect.DisplayAll<sinhvien>("sinhvien", "MaLH");
        }

        [Route("getValueSinhVien/{MSSV}")]
        public object Get(int MSSV)
        {
            return connect.DisplayOne<sinhvien, int>("sinhvien", MSSV, "MSSV");
        }

        // POST: api/GVLopHoc
        [Route("api/GVSinhVien/P")]
        public string Post([FromBody] sinhvien value)
        {
            lophoc lh = connect.DisplayOne<lophoc, int>("lophoc", value.MaLH, "MaLH");
            sinhvienonly sv = connect.DisplayOne<sinhvienonly, int>("sinhvienonly", value.MSSV, "MSSV");
            var db = connect.Getdb();
            List<sinhvien> temp = db.GetCollection<sinhvien>("sinhvien").Find(x => x.MaLH == value.MaLH).ToList();
            if (lh == null || sv == null || sv.TenSV != value.TenSV)
            {
                return "Dữ liệu nhập này không thỏa mãn!";
            }
            else
            {
                if (temp.Find(x => x.MSSV == value.MSSV).Equals(0))
                {
                    connect.InsertDB("sinhvien", value);
                }
                else
                {
                    return "Lớp học đã có sinh viên này!";
                }
            }
            return "Thành công!";
        }
        //[Route("api/GVSinhVien/P")]
        //// PUT: api/GVLopHoc/5
        //public string Put([FromBody] sinhvien value)
        //{
        //    var sv = connect.DisplayOne<sinhvienonly>("sinhvienonly", value.MSSV, "MSSV");
        //    if (sv == null)
        //    {
        //        return "Không tồn tại mã sinh viên này!";
        //    }
        //    else
        //    {
        //        var db = connect.Getdb();
        //        var collection = db.GetCollection<sinhvien>("sinhvien");
        //        var filter = Builders<sinhvien>.Filter.Eq("MSSV", value.MSSV);
        //        var data = Builders<sinhvien>.Update
        //            .Set("TenSV", value.TenSV)
        //            .Set("MaLH", value.MaLH);
        //        collection.UpdateOne(filter, data);
        //    }
        //    return "Thành công!";
        //}
        [Route("api/GVSinhVien/Delete/{value}")]
        // DELETE: api/GVLopHoc/5
        public void Delete(int value)
        {
            connect.DeleteDB<sinhvien>("sinhvien", value, "MSSV");
        }
    }
}
