using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindingNemo.Models
{
    public class BaseEntity
    {
        public DateTime CreateData { get; set; }

        public DateTime? EditDate { get; set; }
    }
}