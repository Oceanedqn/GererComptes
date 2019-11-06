using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WindowsGerer.Properties;
using ECI = WindowsGerer.Bdd.SqlConst.City;
using WindowsGerer.Objets;

namespace WindowsGerer.Bdd
{
    public class Bdd_City
    {
        private MySqlConnection connection;

        public Bdd_City()
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


        // Méthode pour ajouter une ville
        public void AddCity(City city)
        {
            // Ouverture de la conexion SQL
            this.connection.Open();

            // Création d'une commande SQL en fonction de l'objet connection
            using (MySqlCommand cmd = this.connection.CreateCommand())
            {
                // Requête SQL
                cmd.CommandText = $"INSERT INTO {ECI.T_CITY}({ECI.CITY}, {ECI.CP}) VALUES (@{ECI.CITY}, @{ECI.CP})";

                // Utilisation de l'objet passé en paramètre                
                cmd.Parameters.AddWithValue($"@{ECI.CITY}", city.Ville);
                cmd.Parameters.AddWithValue($"@{ECI.CP}", city.CP);
              

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
                cmd.CommandText = $"UPDATE {ECI.T_CITY} SET {ECI.CITY} = @{ECI.CITY}, {ECI.CP} = @{ECI.CP} WHERE {ECI.ID} = @{ECI.ID}";

                // Utilisation de l'objet passé en paramètre    
                cmd.Parameters.AddWithValue($"@{ECI.ID}", city.Id);
                cmd.Parameters.AddWithValue($"@{ECI.CITY}", city.Ville);
                cmd.Parameters.AddWithValue($"@{ECI.CP}", city.CP);

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();
            }

            // Fermeture de la connexion
            this.connection.Close();
        }

        // Méthode pour supprimer un utilisateur
        public void DeleteCity(int id)
        {
                // Ouverture de la conexion SQL
                this.connection.Open();

                // Création d'une commande SQL en fonction de l'objet connection
                using (MySqlCommand cmd = this.connection.CreateCommand())
                {
                    // Requête SQL
                    cmd.CommandText = $"DELETE FROM {ECI.T_CITY} WHERE {ECI.ID} = @{ECI.ID}";

                    // Utilisation de l'objet passé en paramètre                
                    cmd.Parameters.AddWithValue($"@{ECI.ID}", id);

                    // Exécution de la commande SQL
                    cmd.ExecuteNonQuery();
                }

                // Fermeture de la connexion
                this.connection.Close();

        }

        public void DeleteCity(City city)
        {
            DeleteCity(city.Id);
        }


       // Méthode pour afficher les villes
        public List<City> ViewCity()
        {            
            List<City> cityList = new List<City>();


                // Ouverture de la conexion SQL
                this.connection.Open();

            // Création d'une commande SQL en fonction de l'objet connection
            using (MySqlCommand cmd = this.connection.CreateCommand())
            {
                // Requête SQL
                cmd.CommandText =   $"SELECT {ECI.ID}, {ECI.CITY}, {ECI.CP}  " +
                                    $"FROM {ECI.T_CITY} " +
                                    $"ORDER BY {ECI.CITY}";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    
                    while (dataReader.Read())
                    {

                        City city = new City()
                        {
                            Id = dataReader.GetInt32(ECI.ID),
                            Ville = dataReader[$"{ECI.CITY}"].ToString(),
                            CP = dataReader.GetInt32(ECI.CP)
                        };

                        cityList.Add(city);

                    }
                }                  

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();
            }

                

                // Fermeture de la connexion
                this.connection.Close();
            

            return cityList;
        }
    }
}
