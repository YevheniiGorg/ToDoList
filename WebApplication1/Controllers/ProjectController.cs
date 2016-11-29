using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        Context context;
        public ProjectController()
        {
            context = new Context();
        }
        void AddProject(string name,string id)
        {
            int i = Convert.ToInt32(id);
            User us = (from item in context.Users where item.Id == i select item).ToArray<User>()[0];
            context.Projects.Add(new Project(name, us));
            context.SaveChanges();       
        }

        // localhost:61212/Project?act=AddProject&name=myProject1&user=admin
        void RemoveProject(string name, string idUs, string tasks)
        {
            var MasTasks = tasks.Split('^');
            TaskController TK = new TaskController();
            for (int i = 0; i < MasTasks.Length-1; i++)
            {
                TK.RemoveTask(MasTasks[i], name);
            }
                      
            int ID = Convert.ToInt32(idUs);
            User Us = (from item in context.Users where item.Id == ID select item).ToArray<User>()[0];
            var pr = (from item in context.Projects where item.Name == name where item.User.Id==Us.Id select item).ToArray<Project>();
            for (int i = 0; i < pr.Length; i++)
            {
                context.Projects.Remove(pr[i]);
                context.SaveChanges();
            }                            
        }
        // localhost:61212/Project?act=RemoveProject&name=HomeProject&idUs=1
        void RenameProject(string oldname,string newname)
        {
           for(int i=0;  i<context.Projects.ToArray().Length; i++)
            {
                if (context.Projects.ToArray<Project>()[i].Name==oldname)
                {
                    context.Projects.ToArray<Project>()[i].Name = newname;
                }
            }
            context.SaveChanges();

        }
        // localhost:61212/Project?act=ShowAll
        string ShowAll(string id)
        {
            int ID = Convert.ToInt32(id);
            User us = (from item in context.Users where item.Id == ID select item).ToArray<User>()[0];
            var Proj = (from item in context.Projects where item.User.Id == us.Id select item).ToArray<Project>();

            return JsonConvert.SerializeObject(Proj);
        }

        public string Index()
        {
            switch (Request.QueryString["act"])
            {
                case "AddProject":
                AddProject(Request.QueryString["name"], Request.QueryString["user"]);
                    break;
                case "ShowAll":
                 return ShowAll(Request.QueryString["id"]);
                case "RemoveProject":
                    RemoveProject(Request.QueryString["name"], Request.QueryString["idUs"], Request.QueryString["tasks"]);
                    break;
                case "RenameProject":
                    RenameProject(Request.QueryString["oldname"], Request.QueryString["newname"]);
                    break;
            }
            return null;
        }
    }
}