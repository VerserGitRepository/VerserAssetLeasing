﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VerserAssetleasingServiceInterface.Models
{
    public class UserRoleModel
    {
        public List<string> Usermodel { get; set; }
        public SelectList UserIDs { get; set; }
        public List<RolesModel> Rolemodel { get; set; }
        public SelectList Companylist { get; set; }
        public List<int?> ResourceIDs { get; set; }
        public bool IsAdmin { get; set; }
        public bool CanEdit { get; set; }
        public string UserName { get; set; }
    }
}