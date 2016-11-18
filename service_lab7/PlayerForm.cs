using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace service_lab7
{
    public partial class PlayerForm : Form
    {
        Dictionary<int, string> managers = new Dictionary<int, string>();
        Dictionary<int, string> teams = new Dictionary<int, string>();

        private ServiceReference1.BasketballTeams teamds;
        private ServiceReference1.BasketballTeams managerds;
        private ServiceReference1.BasketballTeams playerds;

        BindingSource bindingSource = new BindingSource();
        ServiceReference1.BLogicSoapClient callWebService = new ServiceReference1.BLogicSoapClient();

        public PlayerForm()
        {
            InitializeComponent();
        }

        private void PlayerForm_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            teamds = callWebService.readTeams();
            managerds = callWebService.readManagers();
            playerds = callWebService.readPlayers();

            bindingSource.AllowNew = true;
            bindingSource.DataSource = playerds;
            bindingSource.DataMember = "Player";
            dataGridView1.DataSource = bindingSource;

            comboBox1.DataSource = teamds.Team;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id";

            comboBox2.DataSource = managerds.Manager;
            comboBox2.DisplayMember = "Surname";
            comboBox2.ValueMember = "Id";

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column.Name == "Id")
                {
                    column.Visible = false;
                }
            }

            textBox1.DataBindings.Add(new Binding("Text", bindingSource, "Name"));
            textBox2.DataBindings.Add(new Binding("Text", bindingSource, "Surname"));
            textBox3.DataBindings.Add(new Binding("Text", bindingSource, "Age"));
            textBox4.DataBindings.Add(new Binding("Text", bindingSource, "NumberOfTeam"));
            comboBox1.DataBindings.Add(new Binding("SelectedValue", bindingSource, "IdTeam"));
            comboBox2.DataBindings.Add(new Binding("SelectedValue", bindingSource, "IdManager"));

            comboBox3.DataSource = teamds.Team;
            comboBox3.DisplayMember = "Name";
            comboBox3.ValueMember = "Id";

            comboBox4.DataSource = managerds.Manager;
            comboBox4.DisplayMember = "Surname";
            comboBox4.ValueMember = "Id";
        }

        // Добавление
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Random rand = new Random();
                switch (rand.Next(0, 5))
                {
                    case 0:
                        playerds.Player.AddPlayerRow(1, "Danil", "Shakhov", 22, 5, Convert.ToInt32(comboBox3.SelectedValue.ToString()), Convert.ToInt32(comboBox4.SelectedValue.ToString()));
                        break;
                        /*
                    case 1:
                        teamds.Team.AddTeamRow(1, "Orlandos", 1944, "Orlandodo", 11, Convert.ToInt32(comboBox2.SelectedValue.ToString()));
                        break;
                    case 2:
                        teamds.Team.AddTeamRow(1, "Pinguanes", 1978, "Pitsburg", 11, Convert.ToInt32(comboBox2.SelectedValue.ToString()));
                        break;
                    case 3:
                        teamds.Team.AddTeamRow(1, "Clippers", 1965, "Los Angeles", 11, Convert.ToInt32(comboBox2.SelectedValue.ToString()));
                        break;
                    case 4:
                        teamds.Team.AddTeamRow(1, "Washington", 1923, "Washington", 11, Convert.ToInt32(comboBox2.SelectedValue.ToString()));
                        break;
                        */
                    default:
                        playerds.Player.AddPlayerRow(1, "Danil", "Shakhov", 22, 5, Convert.ToInt32(comboBox3.SelectedValue.ToString()), Convert.ToInt32(comboBox4.SelectedValue.ToString()));
                        break;
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка.");
            }
        }

        // Удаление
        private void button2_Click(object sender, EventArgs e)
        {
            bindingSource.RemoveCurrent();
        }

        // Сохранение
        private void button3_Click(object sender, EventArgs e)
        {
            bindingSource.EndEdit();
            try
            {
                callWebService.updatePlayer(playerds);
                playerds.AcceptChanges();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка.");
            }
        }

        // Отмена
        private void button4_Click(object sender, EventArgs e)
        {
            bindingSource.CancelEdit();
            playerds.RejectChanges();
        }
    }
}
