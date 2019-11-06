using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsGerer.Bdd;
using WindowsGerer.Objets;

namespace WindowsGerer
{
    public partial class F_Main : Form
    {
        Bdd_Users bdd_users = new Bdd_Users();
        Bdd_City bdd_city = new Bdd_City();
        Bdd_Means bdd_means = new Bdd_Means();
        Bdd_Money bdd_money = new Bdd_Money();

        Utilisateur userFull;

        private bool addUserMode = false;



        public F_Main()
        {
            InitializeComponent();
            UpdateViewUser();
            UpdateViewCity();
            UpdateViewMeans();

            label2.Text = "Utilisateur selectionné";
            label3.Text = "Administration";
            label4.Text = "Informations";

           

            tabPage2.Text = "Utilisateur";
            tabPage3.Text = "Paiements";

            validate.Text = "Nouveau";
           

        }

        public void ViewUserTabControl()
        {

            label5.Text =   "Utilisateur : " + userFull.FullName + "\n\n" + "Lieu : " + userFull.City.Ville + "\n\n" + "Email : " + userFull.Email + "\n\n" + "Moyen de paiement : " + userFull.MeansOfPayement.Means;
            label7.Text =  "Date de début de contrat : " + userFull.DateStart.ToShortDateString() + "\n\n" +   "Dernier paiement le " + userFull.Payement.Date.ToShortDateString() + "\n\n" + "Montant du dernier paiement : " + userFull.Payement.Amount + " euro(s)";

        }


        public void ViewPayementTabControl()
        {

            label10.Text =  "Date de début de contrat : " + userFull.DateStart.ToShortDateString() + "\n\n"
                           + "Abonnement spotify payé jusqu'en : " + CalculMoisPaye().ToString("MMM yyyy"); 
                            

            label6.Text = "Liste des dernieres paiements";

            listBox2.DataSource = bdd_money.ViewMoneyUser(userFull.Id);
            listBox2.DisplayMember = "Informations";
            listBox2.ValueMember = "Id";
            listBox2.SelectedIndex = -1;
        }


        

        private DateTime CalculMoisPaye()
        {
            //var const amountPay = 3;
            // SI CA FONCTIONNE PLUS C EST PSQ J AI PLUSIEURS VALEURS
            DateTime dateFinal;

            if (userFull.Payement.Amount == 0)
            {
                dateFinal = DateTime.UtcNow;
            }
            else
            {
                int moisPaiement = userFull.Payement.Date.Month;
                int moisPaye = userFull.Payement.Amount / 3;
                dateFinal = userFull.Payement.Date.AddMonths(moisPaye - 1);
            }

            return dateFinal;
        }


        // Update les utilisateurs dans la listbox
        public void UpdateViewUser()
        {
            label1.Text = "Utilisateurs";
            listBox1.DataSource = bdd_users.ViewUserFullName();
            listBox1.DisplayMember = "FullName";
            listBox1.ValueMember = "Id";
            listBox1.Refresh();
            listBox1.SelectedIndex = -1;

            LogView("UPDATE", "** Mise à jour des utilisateurs **");
        }
         
        public void UpdateViewCity()
        {          
            comboBox1.DataSource = bdd_city.ViewCity();
            comboBox1.DisplayMember = "Ville";
            comboBox1.ValueMember = "Id";
            comboBox1.Refresh();
            comboBox1.SelectedIndex = -1;

            LogView("UPDATE", "** Mise à jour des villes **");
        }

        public void UpdateViewMeans()
        {
            comboBox2.DataSource = bdd_means.ViewMeansOfPayement();
            comboBox2.DisplayMember = "Means";
            comboBox2.ValueMember = "Id";
            comboBox2.Refresh();
            comboBox2.SelectedIndex = -1;

            LogView("UPDATE", "** Mise à jour des moyens de paiements **");
        }

        private void UpdateViewMoney()
        {
            // a mettre dans les bon label etc
            //comboBox2.DataSource = bdd_money.ViewMoney();
            //comboBox2.DisplayMember = "Informations";
            //comboBox2.ValueMember = "Id";
            //comboBox2.Refresh();
            //comboBox2.SelectedIndex = -1;

            LogView("UPDATE", "** Mise à jour des paiements de l'utilisateur " + userFull.FullName + " **");
        }

        // Remplir les textBox en fonction de l'utilisateur choisi
        private void ListBox1_Click(object sender, EventArgs e)
        {
            EnabledTexteBox();

            object temp = listBox1.SelectedItem;
            Utilisateur usertemp = (Utilisateur)temp;

            userFull = bdd_users.GetUserById(usertemp.Id);

            textBox3.Text = userFull.Id.ToString();
            textBox2.Text = userFull.Name.ToString();
            textBox4.Text = userFull.Firstname.ToString();
            textBox1.Text = userFull.Email.ToString();
            comboBox1.SelectedValue = userFull.City.Id;
            comboBox2.SelectedValue = userFull.MeansOfPayement.Id;

            modify.Enabled = true;
            delete.Enabled = true;
            button3.Enabled = true;

            ViewUserTabControl();
            ViewPayementTabControl();

            LogView("SUCCESS", "Selection de l'utilisateur" + " "+ userFull.Name + " " + userFull.Firstname);
        }


        // Vide les textbox
        private void ClearTexteBox()
        {
            textBox3.Clear();
            textBox2.Clear();
            textBox4.Clear();
            textBox1.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;


            LogView("SUCCESS", "** Suppression des éléments dans les textbox **");
        }

        public void AddUserModeIsTrue()
        {
            validate.Text = "Nouveau";

            modify.Text = "Modifier";
            modify.Enabled = false;

            delete.Visible = true;
            delete.Enabled = true;

            //button3.Enabled = false;

            ClearTexteBox();

            UpdateViewUser();            
        }

        public void AddUserModeIsFalse()
        {
            validate.Text = "Ajouter";

            modify.Text = "Retour";
            modify.Enabled = true;

            delete.Visible = false;
            delete.Enabled = false;
            button3.Enabled = false;

            ClearTexteBox();
           
            UpdateViewUser();
        }

        


        // Bouton Nouveau et Ajouter
        private void Validate_Click(object sender, EventArgs e)
        {
            // Pour utiliser le bouton Nouveau
            if (addUserMode == false)
            {
                EnabledTexteBox();
                ChangeAddUserMode();
                
                AddUserModeIsFalse();

                LogView("ATTENTE", "** En attente d'ajout d'un nouvel utilisateur **");

                label5.Text = "Informations de l'utilisateur";
                label7.Text = "";
                label10.Text = "";
            }

            // Pour utiliser le bouton Ajouter
            else if (addUserMode == true)
            {
                             
                object temp1 = comboBox1.SelectedItem;
                City city = (City)temp1;

                object temp2 = comboBox2.SelectedItem;
                MeansOfPayement meansOfPayement = (MeansOfPayement)temp2;


                Utilisateur user = new Utilisateur
                {
                    Name = textBox2.Text,
                    Firstname = textBox4.Text,
                    Email = textBox1.Text,
                    City = city,
                    MeansOfPayement = meansOfPayement
                  
                };

                Bdd_Users bdd = new Bdd_Users();
                bdd.AddUser(user);

                AddUserModeIsTrue();

                LogView("SUCCESS", "** Utilisateur" + " " + user.Name + " " + user.Firstname + " " + "ajouté **");

            }

           
        }

        // Bouton modifier un utilisateur ou bouton retour
        private void Modify_Click(object sender, EventArgs e)
        {
            if (addUserMode == true)
            {
                ChangeAddUserMode();
                AddUserModeIsTrue();
                DisabledTextBox();               
            }

            else if (addUserMode == false)
            {
                object temp = listBox1.SelectedItem;
                Utilisateur user = (Utilisateur)temp;

                object temp1 = comboBox1.SelectedItem;
                City city = (City)temp1;

                object temp2 = comboBox2.SelectedItem;
                MeansOfPayement means = (MeansOfPayement)temp2;


                user.Id = int.Parse(textBox3.Text);
                user.Name = textBox2.Text;
                user.Firstname = textBox4.Text;
                user.Email = textBox1.Text;
                user.City = city;
                user.MeansOfPayement = means;


                Bdd_Users bdd = new Bdd_Users();
                bdd.ModifyUser(user);
                UpdateViewUser();

                LogView("SUCCESS", "** Utilisateur" + " " + user.Name + " " + user.Firstname + " " + "modifié **");
            }
            

            
        }

        // Bouton supprimer un utilisateur 
        private void Delete_Click(object sender, EventArgs e)
        {
            object temp = listBox1.SelectedItem;
            Utilisateur user = (Utilisateur)temp;

            user.Id = int.Parse(textBox3.Text);

            Bdd_Users bdd = new Bdd_Users();
            bdd.DeleteUser(user);
            UpdateViewUser();
            ClearTexteBox();

            LogView("SUCCESS", "** Utilisateur" + " " + user.Name + " " + user.Firstname + " " + "supprimé **");
        }



        // Bouton ajouter une nouvelle fenêtre pour gérer les villes
        private void Button1_Click(object sender, EventArgs e)
        {
            F_City f_City = new F_City();
            f_City.ShowDialog(this);
            UpdateViewCity();
            
        }

        // Bouton ajouter une nouvelle fenêtre pour gérer les villes
        private void Button2_Click(object sender, EventArgs e)
        {
            F_Means f_Means = new F_Means();
            f_Means.ShowDialog(this);
            UpdateViewMeans();

        }



        // Inverser le boolean
        private void ChangeAddUserMode()
        {
            addUserMode = !addUserMode;
        }


        public void LogView(string type, string messageLog)
        {
            richTextBox1.ScrollToCaret();
            Logs logs = new Logs()
            {
                type = type,
                message = messageLog
            };
            
            Color colorText = Color.Black;


            if(type == "SUCCESS")
            {
               colorText = Color.Green;
            }

            if(type == "ERROR")
            {
                colorText = Color.Red;
            }

            if(type == "ATTENTE")
            {
                colorText = Color.Orange;
            }

            if(type == "UPDATE")
            {
                colorText = Color.Purple;
            }

            richTextBox1.AppendText("[" + logs.date + "]", Color.Gray);
            richTextBox1.AppendText(" ");
            richTextBox1.AppendText(type, colorText);
            richTextBox1.AppendText(" ");
            richTextBox1.AppendText(messageLog, Color.Gray);
            richTextBox1.AppendText(Environment.NewLine);

            
        }

        private void TextBox2_Enter(object sender, EventArgs e)
        {
            if(textBox2.Text == "Name")
            {
                textBox2.Text = "";
            }
        }

        private void TextBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Name";
            }
        }

        private void TextBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Firstname")
            {
                textBox4.Text = "";
            }
        }

        private void TextBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "Firstname";
            }
        }


        private void TextBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "someone@example.com")
            {
                textBox1.Text = "";
            }
        }

        private void TextBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "someone@example.com";
            }
        }


        private void EnabledTexteBox()
        {
            textBox2.Enabled = true;
            textBox4.Enabled = true;
            textBox1.Enabled = true;
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
        }

        private void DisabledTextBox()
        {
            textBox2.Enabled = false;
            textBox4.Enabled = false;
            textBox1.Enabled = false;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;

            delete.Enabled = false;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            F_Money f_Money = new F_Money(userFull);
            f_Money.ShowDialog(this);
            UpdateViewMoney();
        }

        
    }

    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }


}
