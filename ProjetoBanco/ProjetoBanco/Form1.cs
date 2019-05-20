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

namespace ProjetoBanco
{
    public partial class Form1 : Form
    {
        //declarando a string de conexão de banco de dados
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-F6BBN38;Initial Catalog=ProjetoBanco1;User ID=sa; Password = 1234567");
        SqlCommand comando = new SqlCommand();
        SqlDataReader dr;

        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            comando.Connection = con;
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            con.Open();
            comando.CommandText = "INSERT INTO dados ( id, nome) VALUES('" + txtID.Text + "','" + txtNome.Text + "')";
            comando.ExecuteNonQuery();
            con.Close();
            carregarlista();


            txtID.Text = "";
            txtNome.Text = "";
            
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "" & txtNome.Text != "")
            {
                con.Open();
                comando.CommandText = "DELETE FROM dados WHERE ID = '" + txtID.Text + "' AND NOME = '" + txtNome.Text + "'";
                comando.ExecuteNonQuery();
                con.Close();


                txtID.Text = "";
                txtNome.Text = "";
            }
            carregarlista();
        }

        private void lblNome_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox l = sender as ListBox;
            if (l.SelectedIndex != -1) 
            {
                listBox1.SelectedIndex = l.SelectedIndex;
                listBox2.SelectedIndex = l.SelectedIndex;

                txtID.Text = listBox1.SelectedItem.ToString();
                txtNome.Text = listBox2.SelectedItem.ToString();
            }
            

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void carregarlista()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            con.Open();
            comando.CommandText = "SELECT * FROM dados";
            dr = comando.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    listBox1.Items.Add(dr[0].ToString());
                    listBox2.Items.Add(dr[1].ToString());
                }
            }
            con.Close();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            con.Open();
            comando.CommandText = "UPDATE dados SET ID = '" + txtID.Text + "', NOME = '" + txtNome.Text + "' WHERE ID = '" + listBox1.SelectedItem.ToString() + "' AND NOME = '" + listBox2.SelectedItem.ToString() + "'";
            comando.ExecuteNonQuery();
            con.Close();
            carregarlista();
            txtID.Text = "";
            txtNome.Text = "";
        }
    }
}
