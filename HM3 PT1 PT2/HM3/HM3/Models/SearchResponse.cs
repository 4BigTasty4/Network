using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM3.Models;

public class SearchResponse
{

    public class Rootobject
    {
        public string search_term { get; set; }
        public object knowledge_panel { get; set; }
        public Result[] results { get; set; }
        public Related_Keywords related_keywords { get; set; }
    }

    public class Related_Keywords
    {
        public object spelling_suggestion_html { get; set; }
        public object spelling_suggestion { get; set; }
        public Keyword[] keywords { get; set; }
    }

    public class Keyword
    {
        public int position { get; set; }
        public Knowledge knowledge { get; set; }
        public string keyword_html { get; set; }
        public string keyword { get; set; }
    }

    public class Knowledge
    {
        public string title { get; set; }
        public string label { get; set; }
        public object image { get; set; }
    }

    public class Result
    {
        public int position { get; set; }
        public string url { get; set; }
        public string title { get; set; }
        public string description { get; set; }
    }

}
