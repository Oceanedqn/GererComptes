using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WindowsGerer.Properties;
using EMP = WindowsGerer.Bdd.SqlConst.Means;
using WindowsGerer.Objets;

namespace WindowsGerer.Bdd
{
    public class Bdd_Means
    {
        private MySqlConnection connection;

        public Bdd_Means()
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
        public void AddMeans(MeansOfPayement meansOfPayement)
        {
            // Ouverture de la conexion SQL
            this.connection.Open();

            // Création d'une commande SQL en fonction de l'objet connection
            using (MySqlCommand cmd = this.connection.CreateCommand())
            {
                // Requête SQL
                cmd.CommandText = $"INSERT INTO {EMP.T_MEANS}({EMP.MEANS}) VALUES (@{EMP.MEANS})";

                // Utilisation de l'objet passé en paramètre                
                //cmd.Parameters.AddWithValue($"@{EMP.ID}", meansOfPayement.Id);
                cmd.Parameters.AddWithValue($"@{EMP.MEANS}", meansOfPayement.Means);
              

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();               
            }

            // Fermeture de la connexion
            this.connection.Close();
        }

        // Méthode pour modifier une viller
        public void ModifyMeansOfPayement(MeansOfPayement MeansOfPayement)
        {
            // Ouverture de la conexion SQL
            this.connection.Open();

            // Création d'une commande SQL en fonction de l'objet connection
            using (MySqlCommand cmd = this.connection.CreateCommand())
            {
                // Requête SQL
                cmd.CommandText = $"UPDATE {EMP.T_MEANS} SET {EMP.MEANS} = @{EMP.MEANS}";

                // Utilisation de l'objet passé en paramètre    
                cmd.Parameters.AddWithValue($"@{EMP.ID}", MeansOfPayement.Id);
                cmd.Parameters.AddWithValue($"@{EMP.MEANS}", MeansOfPayement.Means);

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();
            }

            // Fermeture de la connexion
            this.connection.Close();
        }

        // Méthode pour supprimer un utilisateur
        public void DeleteMeansOfPayement(int id)
        {
                // Ouverture de la conexion SQL
                this.connection.Open();

                // Création d'une commande SQL en fonction de l'objet connection
                using (MySqlCommand cmd = this.connection.CreateCommand())
                {
                    // Requête SQL
                    cmd.CommandText = $"DELETE FROM {EMP.T_MEANS} WHERE {EMP.ID} = @{EMP.ID}";

                    // Utilisation de l'objet passé en paramètre                
                    cmd.Parameters.AddWithValue($"@{EMP.ID}", id);

                    // Exécution de la commande SQL
                    cmd.ExecuteNonQuery();
                }

                // Fermeture de la connexion
                this.connection.Close();

        }

        public void DeleteMeansOfPayement(MeansOfPayement MeansOfPayement)
        {
            DeleteMeansOfPayement(MeansOfPayement.Id);
        }


       // Méthode pour afficher les villes
        public List<MeansOfPayement> ViewMeansOfPayement()
        {            
            List<MeansOfPayement> meansOfPayementList = new List<MeansOfPayement>();


                // Ouverture de la conexion SQL
                this.connection.Open();

            // Création d'une commande SQL en fonction de l'objet connection
            using (MySqlCommand cmd = this.connection.CreateCommand())
            {
                // Requête SQL
                cmd.CommandText =   $"SELECT {EMP.ID}, {EMP.MEANS} "+
                                    $"FROM {EMP.T_MEANS} " +
                                    $"ORDER BY {EMP.MEANS}";

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    
                    while (dataReader.Read())
                    {

                        MeansOfPayement means = new MeansOfPayement()
                        {
                            Id = dataReader.GetInt32(EMP.ID),
                            Means = dataReader[$"{EMP.MEANS}"].ToString()                            
                        };

                        meansOfPayementList.Add(means);

                    }
                }                  

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();
            }

                

                // Fermeture de la connexion
                this.connection.Close();
            

            return meansOfPayementList;
        }
    }
}
