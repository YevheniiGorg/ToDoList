using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TaskController : Controller
    {
        // GET: Task
        Context context;

        public TaskController()
        {
            context = new Context();
        }
       

        void AddTask(string nameTask,string date, string nameProject)
        {          
            nameProject = nameProject.Trim();           
            List<Project> pr = (from item in context.Projects where item.Name == nameProject select item).ToList<Project>();
            context.Tasks.Add(new Task(nameTask, DateTime.Parse(date), pr[0]));
            context.SaveChanges();
        }

        // http://localhost:61212/Task?act=AddTask&nameTask=myTask&date=12.12.2017&nameProject=MyProject

       public void RemoveTask(string nameTask, string nameProject)
        {
            nameProject = nameProject.Trim();
            Task task = (from item in context.Tasks where item.Name == nameTask where item.Project.Name== nameProject select item).ToArray<Task>()[0];
            context.Tasks.Remove(task);
            context.SaveChanges();
        }
      
        // http://localhost:61212/Task?act=RemoveTask&nameTask=myTask&nameProject=new%20Best%20PR

        void RenamePriority(string nameTask, string nameProject, string priority)
        {
            nameProject = nameProject.Trim();
            Task task = (from item in context.Tasks where item.Name == nameTask where item.Project.Name == nameProject select item).ToArray<Task>()[0];
            task.Priority =Convert.ToInt32(priority);           
            context.SaveChanges();
        }
       
        // http://localhost:61212/Task?act=RenamePriority&nameTask=myTask&nameProject=new%20Best%20PR&priority=500

        void RenameTask(string nameTask, string nameProject, string newTask)
        {
            nameProject = nameProject.Trim();
            Task task = (from item in context.Tasks where item.Name == nameTask where item.Project.Name == nameProject select item).ToArray<Task>()[0];
            task.Name = newTask;
            context.SaveChanges();
        }
        // http://localhost:61212/Task?act=RenameTask&nameTask=myTask&nameProject=Best&newTask=newTask
        string ShowAll(string NameProj)
        {
            //var id = Convert.ToInt32(idProj);
            Project Project_ = (from item in context.Projects where item.Name == NameProj select item).ToArray<Project>()[0];           
            var task = (from item in context.Tasks where item.Project.Id == Project_.Id  select item).ToArray<Task>();

            return JsonConvert.SerializeObject(task);
        }
        //http://localhost:61212/Task?act=ShowAll&idProj=6
        void RenameStatus(string nameTask, string nameProject)
        {
            nameProject = nameProject.Trim();
            Task task = (from item in context.Tasks where item.Name == nameTask where item.Project.Name == nameProject select item).ToArray<Task>()[0];
            if (task.Status == true)
                task.Status = false;
            else
                task.Status = true;

            context.SaveChanges();
        }
        public string Index()
        {
            switch (Request.QueryString["act"])
            {
                case "AddTask":
                    AddTask(Request.QueryString["nameTask"], Request.QueryString["date"], Request.QueryString["nameProject"]);
                    break;
                case "RemoveTask":
                    RemoveTask(Request.QueryString["nameTask"], Request.QueryString["nameProject"]);
                    break;
                case "RenameStatus":
                    RenameStatus(Request.QueryString["nameTask"], Request.QueryString["nameProject"]);
                    break;
                case "RenamePriority":
                    RenamePriority(Request.QueryString["nameTask"], Request.QueryString["nameProject"],Request.QueryString["priority"]);
                    break;
                case "RenameTask":
                    RenameTask(Request.QueryString["nameTask"], Request.QueryString["nameProject"], Request.QueryString["newTask"]);
                    break;
                case "ShowAll":
                   return ShowAll(Request.QueryString["idProj"]);
                    
            }
            return null;
        }
    }
}