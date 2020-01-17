using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VerserAssetleasingServiceInterface.Models
{
    public class UserRoleModel
    {
        public List<UserModel> Usermodel { get; set; }
        public List<RolesModel> Rolemodel { get; set; }
        public List<ListItemViewModel> Companylist { get; set; }
    }
}