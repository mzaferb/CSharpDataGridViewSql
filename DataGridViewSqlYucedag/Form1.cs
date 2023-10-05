using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DataGridViewSqlYucedag
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-OOL3ENM;Initial Catalog=OgrencilerYucedag;Integrated Security=True");

        public void getdata(string datas)
        {
            SqlDataAdapter da = new SqlDataAdapter(datas, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
        }

        public void clear()
        {
            tbID.Clear();
            tbName.Clear();
            tbSurname.Clear();
            tbAge.Clear();
            tbDistrict.Clear();
            tbProfession.Clear();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            getdata("Select * from Kisiler");
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            conn.Open();
            //SqlCommand insert = new SqlCommand("Insert into Kisiler Values ('" + tbName.Text + "', '" + tbSurname.Text + "', '" + tbAge.Text + "', '" + tbDistrict.Text + "', '" + tbProfession.Text + "')", conn);
            SqlCommand insert = new SqlCommand("Insert into Kisiler Values (@id, @name, @surname, @age, @district, @profession)", conn);
            insert.Parameters.AddWithValue("@id", tbID.Text);
            insert.Parameters.AddWithValue("@name", tbName.Text);
            insert.Parameters.AddWithValue("@surname", tbSurname.Text);
            insert.Parameters.AddWithValue("@age", tbAge.Text);
            insert.Parameters.AddWithValue("@district", tbDistrict.Text);
            insert.Parameters.AddWithValue("@profession", tbProfession.Text);
            insert.ExecuteNonQuery();
            getdata("Select * from Kisiler");
            conn.Close();
            clear();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            tbID.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();   
            tbName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            tbSurname.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            tbAge.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            tbDistrict.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            tbProfession.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand delete = new SqlCommand("Delete from Kisiler where ID = @id", conn);
            delete.Parameters.AddWithValue("@id", tbID.Text);
            delete.ExecuteNonQuery();
            getdata("Select * from Kisiler");
            conn.Close();
            clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand update = new SqlCommand("Update Kisiler set Name = @name, Surname = @surname, Age = @age, District = @district, Profession = @profession Where ID = @id", conn);
            update.Parameters.AddWithValue("@id", tbID.Text);
            update.Parameters.AddWithValue("@name", tbName.Text);
            update.Parameters.AddWithValue("@surname", tbSurname.Text);
            update.Parameters.AddWithValue("@age", tbAge.Text);
            update.Parameters.AddWithValue("@district", tbDistrict.Text);
            update.Parameters.AddWithValue("@profession", tbProfession.Text);
            update.ExecuteNonQuery();
            getdata("Select * from Kisiler");
            conn.Close();
            clear();
        }
    }
}
