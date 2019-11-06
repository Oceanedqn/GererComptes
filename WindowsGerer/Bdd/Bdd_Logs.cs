using MySql.Data.MySqlClient;
using WindowsGerer.Properties;
using WindowsGerer.Objets;

namespace WindowsGerer.Bdd
{
    public class Bdd_Logs
    {
        private MySqlConnection connection;

        public Bdd_Logs()
        {
            this.InitConnexion();
        }

        // Méthode pour initialiser la connexion
        private void InitConnexion()
        {
            // Création de la chaine de connexion
            string connectionString = Settings.Default.connectionString;

            using (this.connection = new MySqlConnection(connectionString))
            {
                connection.Open();
            }

            
        }


        // Méthode pour ajouter un log
        public void AddLogs(Logs logs)
        {
            // Ouverture de la conexion SQL
            this.connection.Open();

            // Création d'une commande SQL en fonction de l'objet connection
            using (MySqlCommand cmd = this.connection.CreateCommand())
            {
                // Requête SQL
                cmd.CommandText = "INSERT INTO E_LOGS_ELO(ELO_DATE, ELO_MESSAGE, ELO_TYPE) VALUES (@ELO_DATE, @ELO_MESSAGE, @ELO_TYPE)";

                // Utilisation de l'objet passé en paramètre                
                cmd.Parameters.AddWithValue("@ELO_DATE", logs.date);
                cmd.Parameters.AddWithValue("@ELO_MESSAGE", logs.message);
                cmd.Parameters.AddWithValue("@ELO_TYPE", logs.type);


                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();               
            }

            // Fermeture de la connexion
            this.connection.Close();
        }

        // Méthode pour modifier une viller
        public void ModifyCity(City city)
        {
            // Ouverture de la conexion SQL
            this.connection.Open();

            // Création d'une commande SQL en fonction de l'objet connection
            using (MySqlCommand cmd = this.connection.CreateCommand())
            {
                // Requête SQL
                cmd.CommandText = "UPDATE e_lieu_eli SET eus_name = @ELI_ville, ELI_CP = @ELI_CP WHERE ELI_id = @ELI_id";

                // Utilisation de l'objet passé en paramètre    
                cmd.Parameters.AddWithValue("@ELI_id", city.Id);
                cmd.Parameters.AddWithValue("@ELI_ville", city.Ville);
                cmd.Parameters.AddWithValue("@ELI_CP", city.CP);

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();
            }

            // Fermeture de la connexion
            this.connection.Close();
        }

        //// Méthode pour supprimer un log
        //public void DeleteCity(string id)
        //{
        //        // Ouverture de la conexion SQL
        //        this.connection.Open();

        //        // Création d'une commande SQL en fonction de l'objet connection
        //        using (MySqlCommand cmd = this.connection.CreateCommand())
        //        {
        //            // Requête SQL
        //            cmd.CommandText = "DELETE FROM e_logs_elo WHERE ELI_id = @ELI_id";

        //            // Utilisation de l'objet passé en paramètre                
        //            cmd.Parameters.AddWithValue("@ELI_id", id);

        //            // Exécution de la commande SQL
        //            cmd.ExecuteNonQuery();
        //        }

        //        // Fermeture de la connexion
        //        this.connection.Close();

        //}

        //public void DeleteCity(City city)
        //{
        //    DeleteCity(city.Id);
        //}


       //// Méthode pour afficher les logs
       // public List<City> ViewCity()
       // {            
       //     List<City> cityList = new List<City>();


       //         // Ouverture de la conexion SQL
       //         this.connection.Open();

       //     // Création d'une commande SQL en fonction de l'objet connection
       //     using (MySqlCommand cmd = this.connection.CreateCommand())
       //     {
       //         // Requête SQL
       //         cmd.CommandText =   "SELECT ELI_id, eli_ville, eli_cp  " +
       //                             "FROM e_lieu_eli " +                                   
       //                             "ORDER BY ELI_ville";

       //         using (MySqlDataReader dataReader = cmd.ExecuteReader())
       //         {
                    
       //             while (dataReader.Read())
       //             {

       //                 City city = new City()
       //                 {
       //                     Id = dataReader["ELI_id"].ToString(),
       //                     Ville = dataReader["ELI_ville"].ToString(),
       //                     CP = dataReader["ELI_cp"].ToString()
       //                 };

       //                 cityList.Add(city);

       //             }
       //         }                  

       //         // Exécution de la commande SQL
       //         cmd.ExecuteNonQuery();
       //     }       
       //         // Fermeture de la connexion
       //         this.connection.Close();
            
       //     return cityList;
       // }
    }
}
