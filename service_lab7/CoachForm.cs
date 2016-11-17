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
    public partial class CoachForm : Form
    {
        private ServiceReference1.BasketballTeams ds;
        BindingSource bindingSource = new BindingSource();
        ServiceReference1.BLogicSoapClient callWebService = new ServiceReference1.BLogicSoapClient();

        public CoachForm()
        {
            InitializeComponent();
        }

        private void CoachForm_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ds = callWebService.readCoachs();

            bindingSource.AllowNew = true;
            bindingSource.DataSource = ds;
            bindingSource.DataMember = "Coach";
            dataGridView1.DataSource = bindingSource;

            textBox1.DataBindings.Add(new Binding("Text", bindingSource, "Name"));
            textBox2.DataBindings.Add(new Binding("Text", bindingSource, "Surname"));
            textBox3.DataBindings.Add(new Binding("Text", bindingSource, "NumberOfChampions"));

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.Width = 120;
                if (column.Name == "Id")
                {
                    column.Visible = false;
                }
            }

        }

        // Добавление
        private void button1_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            switch (rand.Next(0, 5))
            {
                case 0: ds.Coach.AddCoachRow(1, "Kareem", "Abdul-Jabbar", rand.Next(5, 15));
                        break;
                case 1:
                    ds.Coach.AddCoachRow(1, "Dave", "Bing", rand.Next(5, 15));
                    break;
                case 2:
                    ds.Coach.AddCoachRow(1, "Bill", "Bradley", rand.Next(5, 15));
                    break;
                case 3:
                    ds.Coach.AddCoachRow(1, "Wilt", "Chambelmaeit", rand.Next(5, 15));
                    break;
                case 4:
                    ds.Coach.AddCoachRow(1, "Kobe", "Bryan", rand.Next(0, 15));
                    break;
                default:
                    ds.Coach.AddCoachRow(1, "Lil", "Wayne", rand.Next(0, 15));
                    break;
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
            callWebService.updateCoach(ds);
            ds.AcceptChanges();
        }

        // Отмена
        private void button4_Click(object sender, EventArgs e)
        {
            bindingSource.CancelEdit();
            ds.RejectChanges();
        }
    }
}
