using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainingProject.Models;

namespace TrainingProject.ViewModels
{
    public class ChangeRoleViewModel
    {
        public string UserId { get; set; }

        public string UserEmail { get; set; }

        public List<IdentityRole> AllRoles { get; set; }

        public List<CheckBoxListItem> Roles { get; set; }

        public ChangeRoleViewModel()
        {
            AllRoles = new List<IdentityRole>();
            Roles = new List<CheckBoxListItem>();
        }
    }
}