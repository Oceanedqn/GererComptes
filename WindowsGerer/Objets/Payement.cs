using System;

namespace WindowsGerer.Objets
{
    public class Payement
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public string Comment { get; set; }

        public int Id_EUS { get; set; }

        public string Informations => Date.ToShortDateString() + " : " + Amount + " euros";

        public Payement()
        {

        }

    }
}
