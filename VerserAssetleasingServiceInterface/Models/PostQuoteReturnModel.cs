using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VerserAssetleasingServiceInterface.Models
{
    public class PostQuoteReturnModel
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public int Id { get; set; }
        public string Value { get; set; }
    }
}