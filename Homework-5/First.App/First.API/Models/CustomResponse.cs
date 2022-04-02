using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace First.API.Models
{
    public class CustomResponse
    {
        public object Data { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}
