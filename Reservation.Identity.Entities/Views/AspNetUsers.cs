using System;
using System.Collections.Generic;
using System.Text;

namespace MyIhsan.Identity.Entities.Views
{
    public class AspNetUsers
    {
        public string NAMEEN { get; set; }
        public string ENTITYIDINFO { get; set; }
        public string NAME { get; set; }
        public string CITYID { get; set; }
        public string COUNTRYID { get; set; }
        public short? GENDER { get; set; }
        public string ADDRESS { get; set; }
        public string TELNUMBER { get; set; }
        public short? ISBLOCK { get; set; }
        public short? ISDELETED { get; set; }
        public string USERNAME { get; set; }
        public long? ACCESSFAILEDCOUNT { get; set; }
        public short? LOCKOUTENABLED { get; set; }
        public DateTime? LOCKOUTENDDATEUTC { get; set; }
        public short? TWOFACTORENABLED { get; set; }
        public short? PHONENUMBERCONFIRMED { get; set; }
        public string PHONENUMBER { get; set; }
        public string SECURITYSTAMP { get; set; }
        public string PASSWORDHASH { get; set; }
        public short? EMAILCONFIRMED { get; set; }
        public string EMAIL { get; set; }
        public long? ID { get; set; }
    }
}
