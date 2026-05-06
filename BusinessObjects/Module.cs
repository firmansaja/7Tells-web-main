using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Module
    {
        public int ID { get; set; }

        public int? DisplayOrder { get; set; }

        public string ModuleName { get; set; }
        public string FullName { get; set; }

        public string PageIcon { get; set; }

        public string PageUrl { get; set; }

        public string PageSlug { get; set; }

        public bool? IsDeleted { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDefault { get; set; }

        public int? ParentModuleID { get; set; }
    }
}
