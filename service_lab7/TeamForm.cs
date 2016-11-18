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
    public partial class TeamForm : Form
    {
        Dictionary<int, string> coachs = new Dictionary<int, string>();

        private ServiceReference1.BasketballTeams teamds;
        private ServiceReference1.BasketballTeams coachds;

        BindingSource bindingSource = new BindingSource();
        ServiceReference1.BLogicSoapClient callWebService = new ServiceReference1.BLogicSoapClient();

        public TeamForm()
        {
            InitializeComponent();
        }

        private void TeamForm_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            teamds = callWebService.readTeams();
            coachds = callWebService.readCoachs();

            bindingSource.AllowNew = true;
            bindingSource.DataSource = teamds;
            bindingSource.DataMember = "Team";
            dataGridView1.DataSource = bindingSource;

            foreach (DataRow row in coachds.Coach.Rows)
            {
                coachs.Add(Convert.ToInt32(row[0]), row[1].ToString());
            }
            
            comboBox1.DataSource = coachds.Coach;
            comboBox1.DisplayMember = "Surname";
            comboBox1.ValueMember = "Id";
            
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column.Name == "Id")
                {
                    column.Visible = false;
                }
            }

            textBox1.DataBindings.Add(new Binding("Text", bindingSource, "Name"));
            textBox2.DataBindings.Add(new Binding("Text", bindingSource, "YearFoundation"));
            textBox3.DataBindings.Add(new Binding("Text", bindingSource, "City"));
            comboBox1.DataBindings.Add(new Binding("SelectedValue", bindingSource, "IdCoach"));

            comboBox2.DataSource = coachds.Coach;
            comboBox2.DisplayMember = "Surname";
            comboBox2.ValueMember = "Id";
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
                        teamds.Team.AddTeamRow(1, "Lakers", 1923, "Los Angeles", 11, Convert.ToInt32(comboBox2.SelectedValue.ToString()));
                        break;
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
                    default:
                        teamds.Team.AddTeamRow(1, "Warriors", 1923, "California", 11, Convert.ToInt32(comboBox2.SelectedValue.ToString()));
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
                callWebService.updateTeam(teamds);
                teamds.AcceptChanges();
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
            teamds.RejectChanges();
        }
    }
}
