using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
namespace ThiHocKy.Models
{
    public class giangvien
    {
        [BsonId]
        public int Id { get; set; }
        public string TenGV { get; set; }
        public int MaGV { get; set; }
        public int MaLT { get; set; }

    }
}