using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsGerer.Bdd;
using WindowsGerer.Objets;

namespace WindowsGerer
{
    public partial class F_Means : Form
    {
        Bdd_Means bdd_means = new Bdd_Means();

        private bool addMeansMode = false;
        public F_Means()
        {
            InitializeComponent();
            updateViewMeans();

            label1.Text = "Moyen de paiement";
            label2.Text = "ID";
            label3.Text = "Moyen de paiement";

            label5.Text = "Gestions des moyens de paiements";

            nouveau.Text = "Nouveau";
            modifier.Text = "Modifier";
            supprimer.Text = "Supprimer";
            valider.Text = "VALIDER";
            valider.BackColor = Color.Green;
            valider.ForeColor = Color.White;

        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            object temp = listBox1.SelectedItem;
            MeansOfPayement means = (MeansOfPayement)temp;


            textBox3.Text = means.Id.ToString();
            textBox1.Text = means.Means.ToString();

            modifier.Enabled = true;
            supprimer.Enabled = true;
            textBox1.Enabled = true;

        }



        private void ClearTexteBox()
        {
            textBox3.Clear();
            textBox1.Clear();
        }

        public void addMeansModeIsTrue()
        {
            nouveau.Text = "Nouveau";

            modifier.Text = "Modifier";
            modifier.Enabled = false;

            supprimer.Visible = true;
            supprimer.Enabled = false;

            textBox1.Enabled = false;

            ClearTexteBox();
            updateViewMeans();
        }

        public void addMeansModeIsFalse()
        {
            nouveau.Text = "Ajouter";

            modifier.Text = "Retour";
            modifier.Enabled = true;

            supprimer.Visible = false;
            supprimer.Enabled = false;

            textBox1.Enabled = true;

            ClearTexteBox();
            updateViewMeans();
        }




        private void nouveau_Click(object sender, EventArgs e)
        {
            // Pour utiliser le bouton Nouveau
            if (addMeansMode == false)
            {
                ChangeAddMeansMode();
                addMeansModeIsFalse();
            }

            // Pour utiliser le bouton Ajouter
            else if (addMeansMode == true)
            {
                MeansOfPayement Means = new MeansOfPayement
                {
                    
                    Means = textBox1.Text
                };

                Bdd_Means bdd_means = new Bdd_Means();
                bdd_means.AddMeans(Means);
                addMeansModeIsTrue();
            }
        }
        private void modifier_Click(object sender, EventArgs e)
        {
            if (addMeansMode == true)
            {
                ChangeAddMeansMode();
                addMeansModeIsTrue();
            }

            else if (addMeansMode == false)
            {
                object temp = listBox1.SelectedItem;
                MeansOfPayement Means = (MeansOfPayement)temp;

                //Means.Id = int.Parse(textBox3.Text);
                Means.Means = textBox1.Text;

                Bdd_Means bdd_Means = new Bdd_Means();
                bdd_Means.ModifyMeansOfPayement(Means);
            }
        }
        private void supprimer_Click(object sender, EventArgs e)
        {
            object temp = listBox1.SelectedItem;
            MeansOfPayement Means = (MeansOfPayement)temp;

            //Means.Id = int.Parse(textBox3.Text);

            Bdd_Means bdd_Means = new Bdd_Means();
            bdd_Means.DeleteMeansOfPayement(Means);
            updateViewMeans();
            ClearTexteBox();
        }
        private void valider_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        // Inverser le boolean
        private void ChangeAddMeansMode()
        {
            addMeansMode = !addMeansMode;
        }

        public void updateViewMeans()
        {

            listBox1.DataSource = bdd_means.ViewMeansOfPayement();
            listBox1.DisplayMember = "Means";
            listBox1.ValueMember = "Id";
            listBox1.Refresh();
            listBox1.SelectedIndex = -1;

        }

    }


}
