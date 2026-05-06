using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class User
    {
        public int No { get; set; }
        public Guid? UserId { get; set; }

        public Guid? RoleID { get; set; }

        public string Username { get; set; }

        public byte[] Password { get; set; }

        public string Name { get; set; }

        public string Remark { get; set; }

        public string AdminStatus { get; set; }

        public string FlagActive { get; set; }

        public string UserGroupCode { get; set; }

        public string Site { get; set; }

        public int Level { get; set; }

        public string Email { get; set; }

        public DateTime EntryDate { get; set; }

        public string EntryUser { get; set; }

        public DateTime UpdateDate { get; set; }

        public string UpdateUser { get; set; }

        public DateTime LastLogin { get; set; }

    }
}
