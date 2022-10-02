using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CountryProject.Models
{
    public class CountryModel
    {
        public int Id { get; set; }
        public Name Name { get; set; }
        public string Ccn3 { get; set; }
        public List<string> Capital { get; set; }
        public string Region { get; set; }
        public string Subregion { get; set; }
        public Translations Translations { get; set; }
        public List<string> Borders { get; set; }
        public int Population { get; set; }
        public List<string> Continents { get; set; }
        public Flags Flags { get; set; }
        public List<CountryModel> Neighbors { get; set; } = new List<CountryModel>();
    }
    
    public class German
    {
        public string Official { get; set; }
        public string Common { get; set; }
    }

    public class Flags
    {
        public string Png { get; set; }
        public string Svg { get; set; }
    }

    public class French
    {
        public string Official { get; set; }
        public string Common { get; set; }
    }

    public class Italian
    {
        public string Official { get; set; }
        public string Common { get; set; }
    }

    public class Japan
    {
        public string Official { get; set; }
        public string Common { get; set; }
    }

    public class Name
    {
        public string Common { get; set; }
        public string Official { get; set; }
    }


    public class Spanish
    {
        public string Official { get; set; }
        public string Common { get; set; }
    }

    public class Translations
    {
        public German Deu { get; set; }
        public French Fra { get; set; }
        public Italian Ita { get; set; }
        public Japan Jpn { get; set; }
        public Spanish Spa { get; set; }
    }

}
