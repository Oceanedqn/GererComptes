using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WindowsGerer.Properties;
using EUS = WindowsGerer.Bdd.SqlConst.User;
using ECI = WindowsGerer.Bdd.SqlConst.City;
using EPA = WindowsGerer.Bdd.SqlConst.Payement;
using EMP = WindowsGerer.Bdd.SqlConst.Means;
using WindowsGerer.Objets;

namespace WindowsGerer.Bdd
{
    public class Bdd_Users
    {
        private MySqlConnection connection;

        public Bdd_Users()
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


        // Méthode pour ajouter un utilisateur
        public void AddUser(Utilisateur user)
        {
            // Ouverture de la conexion SQL
            this.connection.Open();

            // Création d'une commande SQL en fonction de l'objet connection
            using (MySqlCommand cmd = this.connection.CreateCommand())
            {
                // Requête SQL
                cmd.CommandText = $"INSERT INTO {EUS.T_USER}({EUS.NAME}, {EUS.FIRSTNAME},{EUS.DATESTART}, {EUS.EMAIL}, {EUS.ID_ECI}, {EUS.ID_EMP}) VALUES (@{EUS.NAME}, @{EUS.FIRSTNAME}, @{EUS.DATESTART}, @{EUS.EMAIL}, @{EUS.ID_ECI}, @{EUS.ID_EMP})";

                // Utilisation de l'objet passé en paramètre                
                cmd.Parameters.AddWithValue($"@{EUS.NAME}", user.Name);
                cmd.Parameters.AddWithValue($"@{EUS.FIRSTNAME}", user.Firstname);
                cmd.Parameters.AddWithValue($"{EUS.DATESTART}", DateTime.UtcNow);
                cmd.Parameters.AddWithValue($"@{EUS.EMAIL}", user.Email);
                cmd.Parameters.AddWithValue($"@{EUS.ID_ECI}", user.City.Id);
                cmd.Parameters.AddWithValue($"@{EUS.ID_EMP}", user.MeansOfPayement.Id);

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();               
            }

            // Fermeture de la connexion
            this.connection.Close();
        }

        // Méthode pour modifier un utilisateur
        public void ModifyUser(Utilisateur user)
        {
            // Ouverture de la conexion SQL
            this.connection.Open();

            // Création d'une commande SQL en fonction de l'objet connection
            using (MySqlCommand cmd = this.connection.CreateCommand())
            {
                // Requête SQL
                cmd.CommandText = $"UPDATE {EUS.T_USER} SET {EUS.NAME} = @{EUS.NAME}, {EUS.FIRSTNAME} = @{EUS.FIRSTNAME}, {EUS.EMAIL} = @{EUS.EMAIL}, {EUS.ID_ECI} = @{EUS.ID_ECI}, {EUS.ID_EMP} = @{EUS.ID_EMP} WHERE {EUS.ID} = @{EUS.ID}";

                // Utilisation de l'objet passé en paramètre    
                cmd.Parameters.AddWithValue($"@{EUS.ID}", user.Id);
                cmd.Parameters.AddWithValue($"@{EUS.NAME}", user.Name);
                cmd.Parameters.AddWithValue($"@{EUS.FIRSTNAME}", user.Firstname);
                cmd.Parameters.AddWithValue($"@{EUS.EMAIL}", user.Email);
                cmd.Parameters.AddWithValue($"@{EUS.ID_ECI}", user.City.Id);
                cmd.Parameters.AddWithValue($"@{EUS.ID_EMP}", user.MeansOfPayement.Id);


                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();
            }

            // Fermeture de la connexion
            this.connection.Close();
        }

        // Méthode pour supprimer un utilisateur
        public void DeleteUser(int id)
        {
                // Ouverture de la conexion SQL
                this.connection.Open();

                // Création d'une commande SQL en fonction de l'objet connection
                using (MySqlCommand cmd = this.connection.CreateCommand())
                {

                // Requête SQL
                cmd.CommandText = $" SET FOREIGN_KEY_CHECKS=0; DELETE EPA.*, EUS.*  FROM {EPA.T_PAYEMENT} AS EPA, {EUS.T_USER} AS EUS  WHERE  EPA.{EPA.ID_EUS} = @{EUS.ID} AND EUS.{EUS.ID} = @{EUS.ID}; SET FOREIGN_KEY_CHECKS=1;";

                
                
                //cmd.CommandText = $"DELETE FROM {EUS.T_USER} WHERE {EUS.ID} = @{EUS.ID}";



                    // Utilisation de l'objet passé en paramètre                
                    cmd.Parameters.AddWithValue($"@{EUS.ID}", id);

                    // Exécution de la commande SQL
                    cmd.ExecuteNonQuery();
                }

                // Fermeture de la connexion
                this.connection.Close();

        }

        public void DeleteUser(Utilisateur user)
        {
            DeleteUser(user.Id);
        }

        public List<Utilisateur> ViewUserFullName()
        {
            List<Utilisateur> userFullNameList = new List<Utilisateur>();
            // Ouverture de la conexion SQL
            this.connection.Open();

            // Création d'une commande SQL en fonction de l'objet connection
            using (MySqlCommand cmd = this.connection.CreateCommand())
            {
                // Requête SQL
                cmd.CommandText = $"SELECT {EUS.ID}, {EUS.NAME}, {EUS.FIRSTNAME}  " +
                                    $"FROM {EUS.T_USER} " +                                    
                                    $"ORDER BY {EUS.NAME} ASC"; // add and user_id = new param
                Console.WriteLine(cmd.CommandText);

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {                       
                        Utilisateur user = new Utilisateur()
                        {
                            Id = dataReader.GetInt32(EUS.ID),
                            Name = dataReader[$"{EUS.NAME}"].ToString(),
                            Firstname = dataReader[$"{EUS.FIRSTNAME}"].ToString(),                           
                        };
                        userFullNameList.Add(user);
                    }
                }
                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();
            }
            // Fermeture de la connexion
            this.connection.Close();

            return userFullNameList;
        }


        



        // Méthode pour afficher les utilisateurs
        public Utilisateur GetUserById(int id_user)
        {

            Utilisateur user = null;


                // Ouverture de la conexion SQL
            this.connection.Open();

            // Création d'une commande SQL en fonction de l'objet connection
            using (MySqlCommand cmd = this.connection.CreateCommand())
            {
                // Requête SQL
                cmd.CommandText = $"SELECT EUS.*, ECI.*, EMP.*, EPA.*" +
                                    $"FROM {EUS.T_USER} AS EUS " +
                                    $"JOIN {ECI.T_CITY} AS ECI ON EUS.{EUS.ID_ECI} = ECI.{ECI.ID} " +
                                    $"JOIN {EMP.T_MEANS} AS EMP ON EUS.{EUS.ID_EMP} = EMP.{EMP.ID} " +                                    
                                    $"LEFT JOIN {EPA.T_PAYEMENT} as EPA ON EUS.{EUS.ID} = EPA.{EPA.ID_EUS} " +
                                    $"WHERE EUS.{EUS.ID} = @{EUS.ID} ";


                cmd.Parameters.AddWithValue($"{EUS.ID}", id_user);
                Console.WriteLine(cmd.CommandText);

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {

                    
                    while (dataReader.Read())
                    {
                        Payement payement = new Payement();
                        if (string.IsNullOrEmpty(dataReader[EPA.DATE].ToString()))
                        {

                            payement.Date = DateTime.UtcNow;
                            payement.Amount = 0;
                            payement.Comment = "N/A";

                        }
                        else
                        {

                            payement.Id = dataReader.GetInt32(EPA.ID);
                            payement.Date = dataReader.GetDateTime(EPA.DATE);
                            payement.Amount = dataReader.GetInt32(EPA.AMOUNT);
                            payement.Comment = dataReader[$"{ EPA.COMMENT }"].ToString();

                        }
                        MeansOfPayement meansOfPayement = new MeansOfPayement()
                        {
                            Id = dataReader.GetInt32(EUS.ID_EMP),
                            Means = dataReader[$"{EMP.MEANS}"].ToString()
                        };

                        City city = new City()
                        {
                            Id = dataReader.GetInt32(EUS.ID_ECI),
                            Ville = dataReader[$"{ECI.CITY}"].ToString(),
                            CP = dataReader.GetInt32(ECI.CP)
                        };

                        user = new Utilisateur()
                        {
                            Id = dataReader.GetInt32(EUS.ID),
                            Name = dataReader[$"{EUS.NAME}"].ToString(),
                            Firstname = dataReader[$"{EUS.FIRSTNAME}"].ToString(),
                            DateStart = dataReader.GetDateTime(EUS.DATESTART),
                            Email = dataReader[$"{EUS.EMAIL}"].ToString(),
                            City = city,
                            MeansOfPayement = meansOfPayement,
                            Payement = payement

                        };

                        // userList.Add(user);
                    }


                }                  

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();
            }

                

                // Fermeture de la connexion
                this.connection.Close();
            

            return user;
        }



        // Méthode pour afficher les utilisateurs
        public List<Utilisateur> ViewUser()
        {

            List<Utilisateur> userList = new List<Utilisateur>();
            List<City> cityList = new List<City>();


            // Ouverture de la conexion SQL
            this.connection.Open();

            // Création d'une commande SQL en fonction de l'objet connection
            using (MySqlCommand cmd = this.connection.CreateCommand())
            {
                // Requête SQL
                cmd.CommandText = $"SELECT {EUS.T_USER}.*, {ECI.T_CITY}.*, {EMP.T_MEANS}.*, {EPA.T_PAYEMENT}.* " +
                                    $"FROM {EUS.T_USER}, {ECI.T_CITY}, {EMP.T_MEANS}, {EPA.T_PAYEMENT}" +
                                    $"WHERE {EUS.T_USER}.{EUS.ID_ECI} = {ECI.T_CITY}.{EUS.ID_ECI} " +
                                    $"AND {EUS.T_USER}.{EUS.ID_EMP} = {EMP.T_MEANS}.{EUS.ID_EMP} " +
                                    $"AND {EPA.T_PAYEMENT}.{EPA.ID_EUS} = {EUS.T_USER}.{EUS.ID} " +
                                    $"ORDER BY {EUS.NAME} ASC"; // add and user_id = new param

                Console.WriteLine(cmd.CommandText);

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {

                    while (dataReader.Read())
                    {
                        Payement payement = new Payement()
                        {
                            Id = dataReader.GetInt32(EPA.ID),
                            Date = dataReader.GetDateTime(EPA.DATE),
                            Amount = dataReader.GetInt32(EPA.AMOUNT),
                            Comment = dataReader[$"{ EPA.COMMENT }"].ToString()
                        };

                        MeansOfPayement meansOfPayement = new MeansOfPayement()
                        {
                            Id = dataReader.GetInt32(EUS.ID_EMP),
                            Means = dataReader[$"{EMP.MEANS}"].ToString()
                        };

                        City city = new City()
                        {
                            Id = dataReader.GetInt32(EUS.ID_ECI),
                            Ville = dataReader[$"{ECI.CITY}"].ToString(),
                            CP = dataReader.GetInt32(ECI.CP)
                        };

                        Utilisateur user = new Utilisateur()
                        {
                            Id = dataReader.GetInt32(EUS.ID),
                            Name = dataReader[$"{EUS.NAME}"].ToString(),
                            Firstname = dataReader[$"{EUS.FIRSTNAME}"].ToString(),
                            DateStart = dataReader.GetDateTime(EUS.DATESTART),
                            Email = dataReader[$"{EUS.EMAIL}"].ToString(),
                            City = city,
                            MeansOfPayement = meansOfPayement,
                            Payement = payement

                        };

                        userList.Add(user);
                    }
                }

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();
            }



            // Fermeture de la connexion
            this.connection.Close();


            return userList;
        }
    }
}
