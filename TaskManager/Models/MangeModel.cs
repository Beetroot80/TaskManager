using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class ManageModel
    {
        public string ProjectTitle { get; set; }
        public string TaskTitle { get; set; }
        public string AssignToEmail { get; set; }

        public List<string> ProjectActions { get; set; }
        public List<string> TaskAction { get; set; }
    }
}