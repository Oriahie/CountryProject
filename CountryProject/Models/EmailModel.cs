using System.Collections.Generic;

namespace CountryProject.Models
{
    public class EmailModel
    {
        public string Receiver { get; set; }
        public string Subject { get; set; }
        public string HtmlBody { get; set; }
    }
}
