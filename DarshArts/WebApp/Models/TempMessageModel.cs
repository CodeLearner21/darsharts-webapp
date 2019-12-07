using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class TempMessageModel
    {
        public string Status { get; set; }
        public string Message { get; set; }

        public TempMessageModel(string status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}
