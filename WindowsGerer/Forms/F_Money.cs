using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsGerer.Bdd;
using WindowsGerer.Objets;

namespace WindowsGerer
{
    public partial class F_Money : Form
    {
        Bdd_Money bdd_money = new Bdd_Money();
        Utilisateur user;

        private bool addMoneyMode = false;
        public F_Money()
        {
            

        }

        public F_Money (Utilisateur userfull)
        {
            InitializeComponent();
            //updateViewMoney();

            label1.Text = "Utilisateur";
            label2.Text = "ID";
            label3.Text = "Montant";
            label4.Text = "Commentaire";
            label6.Text = userfull.FullName;
            label7.Text = "Paiements";

            label5.Text = "Gestions des paiements des utilisateurs";

            nouveau.Text = "Nouveau";
            modifier.Text = "Modifier";
            supprimer.Text = "Supprimer";
            valider.Text = "VALIDER";
            valider.BackColor = Color.Green;
            valider.ForeColor = Color.White;
            textBox3.Text = userfull.Id.ToString();

            user = userfull;

            ListBoxViewMoneyUser();
        }



        // Afficher les paiements de l'utilisateur
        private void ListBoxViewMoneyUser()
        {
            listBox1.DataSource = bdd_money.ViewMoneyUser(user.Id);
            listBox1.DisplayMember = "Informations";
            listBox1.ValueMember = "Id";
            listBox1.Refresh();
            listBox1.SelectedIndex = -1;
        }


        // Selectionner un paiement
        private void ListBox1_Click(object sender, EventArgs e)
        {
            object temp = listBox1.SelectedItem;
            Payement payement = (Payement)temp;

            //user = bdd_money.ViewMoneyUser(user.Id);

            textBox3.Text = user.Id.ToString();
            textBox1.Text = payement.Amount.ToString();
            textBox2.Text = payement.Comment.ToString();
            modifier.Enabled = true;
            supprimer.Enabled = true;

            textBox1.Enabled = true;
            textBox2.Enabled = true;
        }


        // Vider les textbox
        private void ClearTexteBox()
        {            
            textBox1.Clear();
            textBox2.Clear();
        }

        
        public void AddMoneyModeIsTrue()
        {
            nouveau.Text = "Nouveau";

            modifier.Text = "Modifier";
            modifier.Enabled = false;

            supprimer.Visible = true;
            supprimer.Enabled = false;

            textBox1.Enabled = false;
            textBox2.Enabled = false;

            ClearTexteBox();
            UpdateViewMoney();
        }

        public void AddMoneyModeIsFalse()
        {
            nouveau.Text = "Ajouter";

            modifier.Text = "Retour";
            modifier.Enabled = true;

            supprimer.Visible = false;
            supprimer.Enabled = false;

            textBox1.Enabled = true;
            textBox2.Enabled = true;

            ClearTexteBox();
            UpdateViewMoney();
        }




        private void Nouveau_Click(object sender, EventArgs e)
        {
            // Pour utiliser le bouton Nouveau
            if (addMoneyMode == false)
            {
                ChangeAddMoneyMode();
                AddMoneyModeIsFalse();
            }

            // Pour utiliser le bouton Ajouter
            else if (addMoneyMode == true)
            {

                user.Payement.Amount = int.Parse(textBox1.Text);
                user.Payement.Comment = textBox2.Text;

                Bdd_Money bdd = new Bdd_Money();
                bdd.AddMoney(user);
                AddMoneyModeIsTrue();

            }
        }
        private void Modifier_Click(object sender, EventArgs e)
        {
            if (addMoneyMode == true)
            {
                ChangeAddMoneyMode();
                AddMoneyModeIsTrue();
            }

            else if (addMoneyMode == false)
            {
                object temp = listBox1.SelectedItem;
                Payement payement = (Payement)temp;

                payement.Id_EUS = int.Parse(textBox3.Text);
                payement.Amount = int.Parse(textBox1.Text);
                payement.Comment = textBox2.Text;

                
                bdd_money.ModifyMoney(payement);
            }
        }
        private void Supprimer_Click(object sender, EventArgs e)
        {
            object temp = listBox1.SelectedItem;
            Payement payement = (Payement)temp;

            //money.Id = int.Parse(textBox3.Text);

            Bdd_Money bdd = new Bdd_Money();
            bdd.DeleteMoney(payement.Id);

            UpdateViewMoney();
            ClearTexteBox();
        }
        private void Valider_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        // Inverser le boolean
        private void ChangeAddMoneyMode()
        {
            addMoneyMode = !addMoneyMode;
        }



        public void UpdateViewMoney()
        {

            listBox1.DataSource = bdd_money.ViewMoneyUser(user.Id);
            listBox1.DisplayMember = "Informations";
            listBox1.ValueMember = "Id";
            listBox1.Refresh();
            listBox1.SelectedIndex = -1;

        }

    }


}

