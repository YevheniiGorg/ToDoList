using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    [Serializable]
    public class Task
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }
        public virtual Project Project {get;set; }
        public Task() { Name = ""; }
        public Task(string name, DateTime date, Project idProject) {
            Priority = 400;
            Name = name;
            Date = date;
            Project = idProject;
            Status = true;
        }

    }
}
