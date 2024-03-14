using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    internal class Group:BaseEntity
    {
        public int Name { get; set; }
        public int Teacher { get; set; }
        public string Room { get; set; }
    }
}
