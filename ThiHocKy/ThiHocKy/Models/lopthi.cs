using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThiHocKy.Models
{
    public class lopthi
    {
        [BsonId]
        public Guid Id { get; set; }
        public int MaLT { get; set; }
        public int MaLH { get; set; }
        public string TenLT { get; set; }
        public int SoGV { get; set; }
        public int MaGV1 { get; set; }
        public int MaGV2 { get; set; }
        public string Time { get; set; }
        public string Location { get; set; }
        public bool HinhThuc { get; set; }

    }
}