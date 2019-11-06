using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsGerer.Bdd;
using WindowsGerer.Objets;

namespace WindowsGerer
{
    public partial class F_City : Form
    {
        Bdd_City bdd_city = new Bdd_City();

        private bool addCityMode = false;
        public F_City()
        {
            InitializeComponent();
            UpdateViewCity();

            label1.Text = "Villes";
            label2.Text = "ID";
            label3.Text = "Ville";
            label4.Text = "Code Postal";

            label5.Text = "Gestions des villes";

            nouveau.Text = "Nouveau";
            modifier.Text = "Modifier";
            supprimer.Text = "Supprimer";
            valider.Text = "VALIDER";
            valider.BackColor = Color.Green;
            valider.ForeColor = Color.White;

        }

        private void ListBox1_Click(object sender, EventArgs e)
        {
            object temp = listBox1.SelectedItem;
            City city = (City)temp;

            textBox3.Text = city.Id.ToString();
            textBox1.Text = city.Ville.ToString();
            textBox2.Text = city.CP.ToString();


            modifier.Enabled = true;
            supprimer.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
        }
       
        private void ClearTexteBox()
        {
            textBox3.Clear();
            textBox1.Clear();
            textBox2.Clear();    
        }

        public void AddCityModeIsTrue()
        {
            nouveau.Text = "Nouveau";

            modifier.Text = "Modifier";
            modifier.Enabled = false;

            supprimer.Visible = true;
            supprimer.Enabled = false;

            textBox1.Enabled = false;
            textBox2.Enabled = false;

            ClearTexteBox();
            UpdateViewCity();
        }

        public void AddCityModeIsFalse()
        {
            nouveau.Text = "Ajouter";

            modifier.Text = "Retour";
            modifier.Enabled = true;

            supprimer.Visible = false;
            supprimer.Enabled = false;


            textBox1.Enabled = true;
            textBox2.Enabled = true;
            ClearTexteBox();
            UpdateViewCity();
        }     

        private void Nouveau_Click(object sender, EventArgs e)
        {
            // Pour utiliser le bouton Nouveau
            if (addCityMode == false)
            {
                ChangeAddCityMode();
                AddCityModeIsFalse();              
            }

            // Pour utiliser le bouton Ajouter
            else if (addCityMode == true)
            {
                City city = new City
                {
                    Ville = textBox1.Text,
                    CP = int.Parse(textBox2.Text)
                };

                Bdd_City bdd = new Bdd_City();
                bdd.AddCity(city);
                AddCityModeIsTrue();                
            }
        }
        private void Modifier_Click(object sender, EventArgs e)
        {
            if (addCityMode == true)
            {
                ChangeAddCityMode();
                AddCityModeIsTrue();
            }

            else if (addCityMode == false)
            {
                object temp = listBox1.SelectedItem;
                City city = (City)temp;

                
                city.Ville = textBox1.Text;
                city.CP = int.Parse(textBox2.Text);

                Bdd_City bdd = new Bdd_City();
                bdd.ModifyCity(city);

                UpdateViewCity();
            }
        }
        private void Supprimer_Click(object sender, EventArgs e)
        {
            object temp = listBox1.SelectedItem;
            City city = (City)temp;

            //city.Id = int.Parse(textBox3.Text);

            Bdd_City bdd = new Bdd_City();
            bdd.DeleteCity(city);
            UpdateViewCity();
            ClearTexteBox();
        }
        private void Valider_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        // Inverser le boolean
        private void ChangeAddCityMode()
        {
            addCityMode = !addCityMode;
        }

        public void UpdateViewCity()
        {

            listBox1.DataSource = bdd_city.ViewCity();
            listBox1.DisplayMember = "Ville";
            listBox1.ValueMember = "Id";
            listBox1.Refresh();
            listBox1.SelectedIndex = -1;

        }

        
    }


}
