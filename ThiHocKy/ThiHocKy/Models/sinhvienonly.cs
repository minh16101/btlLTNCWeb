using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThiHocKy.Models
{
    public class sinhvienonly
    {
        [BsonId]
        public Guid Id { get; set; }
        public int MSSV { get; set; }
        public string TenSV { get; set; }
    }
}