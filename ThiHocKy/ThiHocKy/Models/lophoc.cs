using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThiHocKy.Models
{
    public class lophoc
    {
        [BsonId]
        public Guid Id { get; set; }
        public int MaLH { get; set; }
        public string TenLH { get; set; }
        public int MaGV { get; set; }
        public string MaHP { get; set; }
    }
}