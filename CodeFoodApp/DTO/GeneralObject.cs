using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFoodApp.DTO
{
    public class GeneralObject
    {

    }
    public class ResultDynamicOK
    {
        public bool success { get; set; }
        public dynamic data { get; set; }
        public string message { get; set; }

    }
    public class ResultDynamicBad
    {
        public bool success { get; set; }
        public string message { get; set; }

    }
}
