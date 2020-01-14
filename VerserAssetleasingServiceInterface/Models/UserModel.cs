using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VerserAssetleasingServiceInterface.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string LoweredUserName { get; set; }
        public bool IsAnonymous { get; set; }
    }
}