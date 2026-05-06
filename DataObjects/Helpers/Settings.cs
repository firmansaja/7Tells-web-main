using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.Helpers
{
    public sealed class Settings
    {
        private static Settings instance = null;
        static readonly object padlock = new object();
        Settings()
        {

        }
        public static Settings Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Settings();
                    }
                    return instance;
                }
            }
        }

        // declare your global variables here
        public Guid? UserId { get; set; }
        public string Site { get; set; }
        public string UserName { get; set; }
        public string DeptID { get; set; }
        public string Dept { get; set; }
    }
}
