namespace HM2;

public class populationResponse
{
    public class City
    {
        public string name { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string country { get; set; }
        public int population { get; set; }
        public bool is_capital { get; set; }
    }

}
