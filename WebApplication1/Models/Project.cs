using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    [Serializable]
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual User User{ get; set; }

        public Project()
        {
            Name = "";
        }
        public Project(string name,User us)
        {
            Name = name;
            User = us;
        }
    }


}
