namespace HM2;

public class attractionsResponse
{

    public class Rootobject
    {
        public bool status { get; set; }
        public string message { get; set; }
        public long timestamp { get; set; }
        public Datum[] data { get; set; }
    }

    public class Datum
    {
        public string id { get; set; }
        public Explore_Search_Params explore_search_params { get; set; }
        public string suggestion_type { get; set; }
        public string vertical_type { get; set; }
        public string display_name { get; set; }
        public Location location { get; set; }
    }

    public class Explore_Search_Params
    {
        public string place_id { get; set; }
        public string query { get; set; }
    }

    public class Location
    {
        public string location_name { get; set; }
        public string google_place_id { get; set; }
        public string[] types { get; set; }
        public Term[] terms { get; set; }
        public string country_code { get; set; }
        public string countryCode { get; set; }
    }

    public class Term
    {
        public int offset { get; set; }
        public string value { get; set; }
    }

}
