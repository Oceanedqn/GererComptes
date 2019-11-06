using System;

namespace WindowsGerer.Objets
{
    public class Logs
    {
        public DateTime date { get; set; }
        public string type { get; set; }
        public string message { get; set; }



        public Logs()
        {
            date = DateTime.UtcNow;

           
        }


        
    }
}
