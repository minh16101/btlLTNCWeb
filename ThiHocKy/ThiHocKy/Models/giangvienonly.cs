using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThiHocKy.Models
{
    public class giangvienonly
    {
        [BsonId]
        public Guid Id { get; set; }
        public int MaGV { get; set; }
        public string TenGV { get; set; }

    }
}