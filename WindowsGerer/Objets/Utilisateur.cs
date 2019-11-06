using System;

namespace WindowsGerer.Objets
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Firstname { get; set; }

        public DateTime DateStart { get; set; }
        public string Email { get; set; }

        public City City { get; set; }

        public MeansOfPayement MeansOfPayement { get; set; }

        public Payement Payement { get; set; }


        public  string FullName => Name + " "+ Firstname;
        public Utilisateur()
        {

        }
    }
}
