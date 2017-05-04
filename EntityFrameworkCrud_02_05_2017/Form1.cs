using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameworkCrud_02_05_2017
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            showAllData();
        }

        public void showAllData()
        {
            using (PersonCrudEntities db = new PersonCrudEntities())
            {
                List<person> v = db.people.ToList();
                foreach (person item in v)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = item.ID;
                    dataGridView1.Rows[n].Cells[1].Value = item.name;
                    dataGridView1.Rows[n].Cells[2].Value = item.email;
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            using (PersonCrudEntities db = new PersonCrudEntities())
            {
               person isExests = db.people.FirstOrDefault(r => r.email == textBoxEmail.Text);
                if (isExests == null)
                {
                    person aPerson = new person
                    {
                        name = textBoxName.Text,
                        email = textBoxEmail.Text
                    };

                    //is email exists or not

                    db.people.Add(aPerson);
                    db.SaveChanges();
                    MessageBox.Show("data saved !");

                }
                else
                {
                    MessageBox.Show("email already exists.");
                }

                showAllData();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            textBoxId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBoxEmail.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            if (textBoxId.Text != "")
            {
                using (PersonCrudEntities db = new PersonCrudEntities())
                {
                    int id = Convert.ToInt32(textBoxId.Text);
                    person isExests = db.people.FirstOrDefault(r => r.email == textBoxEmail.Text);
                    person aPerson = db.people.FirstOrDefault(r => r.ID == id);
                    if (isExests == null)
                    {

                        aPerson.name = textBoxName.Text;
                        aPerson.email = textBoxEmail.Text;


                        //is email exists or not

                        //db.people.Add(aPerson);
                        db.SaveChanges();
                        MessageBox.Show("Updated Successfully !");

                    }
                    else
                    {
                        MessageBox.Show("email already exists.");
                    }

                }
                }
                else{
                    MessageBox.Show("Please Select a member for update.");
                }
                showAllData();
            }
        

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            if(textBoxId.Text != "")
            {            
            using (PersonCrudEntities db = new PersonCrudEntities())
            {
                int id = Convert.ToInt32(textBoxId.Text);
                person aPerson = db.people.FirstOrDefault(r => r.ID == id);


                     db.people.Remove(aPerson);
                     db.SaveChanges();
                    MessageBox.Show("Deleted successfully !");

                

            }
            }
            else
            {
                MessageBox.Show("Please select a Member for delete.");
            }
            showAllData();
        }
    }
}
