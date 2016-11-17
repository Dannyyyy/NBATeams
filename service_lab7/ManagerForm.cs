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
    public partial class ManagerForm : Form
    {
        private ServiceReference1.BasketballTeams ds;
        BindingSource bindingSource = new BindingSource();
        ServiceReference1.BLogicSoapClient callWebService = new ServiceReference1.BLogicSoapClient();

        public ManagerForm()
        {
            InitializeComponent();
        }

        private void ManagerForm_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ds = callWebService.readManagers();

            bindingSource.AllowNew = true;
            bindingSource.DataSource = ds;
            bindingSource.DataMember = "Manager";
            dataGridView1.DataSource = bindingSource;

            textBox1.DataBindings.Add(new Binding("Text", bindingSource, "Name"));
            textBox2.DataBindings.Add(new Binding("Text", bindingSource, "Surname"));
            textBox3.DataBindings.Add(new Binding("Text", bindingSource, "TelNumber"));
            textBox4.DataBindings.Add(new Binding("Text", bindingSource, "Email"));

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
                case 0:
                    ds.Manager.AddManagerRow(1, "Roy", "Pitterson", "+" + rand.Next(10000, 99999).ToString(), "r.petterson@mail.ru");
                    break;
                case 1:
                    ds.Manager.AddManagerRow(1, "Mitch", "McMac", "+" + rand.Next(10000, 99999).ToString(), "m.mcm@gmail.com");
                    break;
                case 2:
                    ds.Manager.AddManagerRow(1, "Joe", "Gilmary", "+" + rand.Next(10000, 99999).ToString(), "j.gilmary@ya.ru");
                    break;
                case 3:
                    ds.Manager.AddManagerRow(1, "Sam", "Golder", "+" + rand.Next(10000, 99999).ToString(), "s.gold@list.com");
                    break;
                case 4:
                    ds.Manager.AddManagerRow(1, "Chick", "Chan", "+" + rand.Next(10000, 99999).ToString(), "c.chan@box.com");
                    break;
                default:
                    ds.Manager.AddManagerRow(1, "Phil", "Jasser", "+" + rand.Next(10000, 99999).ToString(), "p.jas@gmail.com");
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
            callWebService.updateManager(ds);
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
