using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM3.Models;

public class BingResponse
{

    public class Rootobject
    {
        public Search_Results[] search_results { get; set; }
        public People_Also_Ask[] people_also_ask { get; set; }
    }

    public class Search_Results
    {
        public string title { get; set; }
        public string url { get; set; }
        public string caption { get; set; }
    }

    public class People_Also_Ask
    {
        public string title { get; set; }
        public string url { get; set; }
        public string description { get; set; }
    }

}
