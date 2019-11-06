using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WindowsGerer.Properties;
using EUS = WindowsGerer.Bdd.SqlConst.User;
using EPA = WindowsGerer.Bdd.SqlConst.Payement;
using WindowsGerer.Objets;

namespace WindowsGerer.Bdd
{
    public class Bdd_Money
    {
        private MySqlConnection connection;

        public Bdd_Money()
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


        // Méthode pour ajouter un paiement
        public void AddMoney(Utilisateur user)
        {
            // Ouverture de la conexion SQL
            this.connection.Open();

            // Création d'une commande SQL en fonction de l'objet connection
            using (MySqlCommand cmd = this.connection.CreateCommand())
            {
                //                SELECT MAX(EPA_id) AS EPA_ID, EUS_id
                //FROM            e_payement_epa AS EPA, e_user_eus AS EUS
                //WHER            E EUS_id = 12



                // Requête SQL
                cmd.CommandText = $"INSERT INTO {EPA.T_PAYEMENT}({EPA.DATE}, {EPA.AMOUNT}, {EPA.COMMENT}, {EPA.ID_EUS}) VALUES (@{EPA.DATE}, @{EPA.AMOUNT}, @{EPA.COMMENT}, @{EPA.ID_EUS})";      

                // Utilisation de l'objet passé en paramètre                
                cmd.Parameters.AddWithValue($"@{EPA.DATE}", DateTime.UtcNow);
                cmd.Parameters.AddWithValue($"@{EPA.AMOUNT}", user.Payement.Amount);
                cmd.Parameters.AddWithValue($"@{EPA.COMMENT}", user.Payement.Comment);
                cmd.Parameters.AddWithValue($"@{EPA.ID_EUS}", user.Id);
                
                



                
               


                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();
            }

            // Fermeture de la connexion
            this.connection.Close();
        }

        // Méthode pour modifier une viller
        public void ModifyMoney(Payement payement)
        {
            // Ouverture de la conexion SQL
            this.connection.Open();

            // Création d'une commande SQL en fonction de l'objet connection
            using (MySqlCommand cmd = this.connection.CreateCommand())
            {
                // Requête SQL
                cmd.CommandText = $"UPDATE {EPA.T_PAYEMENT} SET {EPA.AMOUNT} = @{EPA.AMOUNT}, {EPA.COMMENT} =  @{EPA.COMMENT} WHERE {EPA.ID} = @{EPA.ID}";

                // Utilisation de l'objet passé en paramètre    
                cmd.Parameters.AddWithValue($"@{EPA.ID}", payement.Id);
                cmd.Parameters.AddWithValue($"@{EPA.AMOUNT}", payement.Amount);
                cmd.Parameters.AddWithValue($"@{EPA.COMMENT}", payement.Comment);

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();
            }

            // Fermeture de la connexion
            this.connection.Close();
        }

        // Méthode pour supprimer un utilisateur
        public void DeleteMoney(int id)
        {
            // Ouverture de la conexion SQL
            this.connection.Open();

            // Création d'une commande SQL en fonction de l'objet connection
            using (MySqlCommand cmd = this.connection.CreateCommand())
            {
                // Requête SQL
                cmd.CommandText = $"DELETE FROM {EPA.T_PAYEMENT} WHERE {EPA.ID} = @{EPA.ID}";

                // Utilisation de l'objet passé en paramètre                
                cmd.Parameters.AddWithValue($"@{EPA.ID}", id);

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();
            }

            // Fermeture de la connexion
            this.connection.Close();

        }

        public void DeleteMoney(Utilisateur money)
        {
            DeleteMoney(money.Payement.Id);
        }


        // Méthode pour afficher les villes
        public List<Payement> ViewMoneyUser(int idViewMoney)
        {
            List<Payement> moneyList = new List<Payement>();


            // Ouverture de la conexion SQL
            this.connection.Open();

            // Création d'une commande SQL en fonction de l'objet connection
            using (MySqlCommand cmd = this.connection.CreateCommand())
            {
                // Requête SQL
                cmd.CommandText =   $"SELECT EPA.{EPA.ID}, EPA.{EPA.DATE}, EPA.{EPA.AMOUNT}, EPA.{EPA.COMMENT} " +
                                    $"FROM {EPA.T_PAYEMENT} AS EPA " +                                    
                                    $"INNER JOIN {EUS.T_USER} AS EUS ON EUS.{EUS.ID} = EPA.{EPA.ID_EUS} " +
                                    $"WHERE EUS.{EUS.ID} = @id";

                cmd.Parameters.AddWithValue("@id", idViewMoney);

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {

                    while (dataReader.Read())
                    {

                        Payement payement = new Payement()
                        {
                            Id = dataReader.GetInt32(EPA.ID),
                            Date = dataReader.GetDateTime($"{EPA.DATE}"),
                            Amount = dataReader.GetInt32($"{EPA.AMOUNT}"),
                            Comment = dataReader[$"{EPA.COMMENT}"].ToString(),
                        };

                        moneyList.Add(payement);

                    }
                }

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();
            }



            // Fermeture de la connexion
            this.connection.Close();


            return moneyList;
        }

        public List<Payement> ViewOneMoneyUser(int idViewMoney, int idViewUser)
        {
            List<Payement> moneyList = new List<Payement>();


            // Ouverture de la conexion SQL
            this.connection.Open();

            // Création d'une commande SQL en fonction de l'objet connection
            using (MySqlCommand cmd = this.connection.CreateCommand())
            {
                // Requête SQL
                cmd.CommandText = $"SELECT EPA.{EPA.ID}, EPA.{EPA.DATE}, EPA.{EPA.AMOUNT}, EPA.{EPA.COMMENT} " +
                                    $"FROM {EPA.T_PAYEMENT} AS EPA " +
                                    $"INNER JOIN {EUS.T_USER} AS EUS ON EUS.{EUS.ID} = EPA.{EPA.ID_EUS} " +
                                    $"WHERE EUS.{EUS.ID} = @id, EPA.{EPA.ID} = @id_epa";

                cmd.Parameters.AddWithValue("@id", idViewMoney);
                cmd.Parameters.AddWithValue("@id_epa", idViewUser);

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {

                    while (dataReader.Read())
                    {

                        Payement payement = new Payement()
                        {
                            Id = dataReader.GetInt32(EPA.ID),
                            Date = dataReader.GetDateTime($"{EPA.DATE}"),
                            Amount = dataReader.GetInt32($"{EPA.AMOUNT}"),
                            Comment = dataReader[$"{EPA.COMMENT}"].ToString(),
                        };

                        moneyList.Add(payement);

                    }
                }

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();
            }



            // Fermeture de la connexion
            this.connection.Close();


            return moneyList;
        }
    }
}
