using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connStr = "server=home.sdlik.ru;port=33033; database = is_17_EKZ; user=st_17;;password=123456789;";
            
            MySqlConnection conn = new MySqlConnection(connStr);
            string sql = $"SELECT COUNT(*)  FROM polzovatel WHERE Username = '{textBox1.Text}' and  Password = '{sha256(textBox2.Text)}'";
            conn.Open();
            MySqlCommand command = new MySqlCommand(sql, conn);
            int count = Convert.ToInt32(command.ExecuteScalar());
            conn.Close();
            if (count > 0)
            {
                MessageBox.Show("Вы вошли в программу");
                Form2 mainWindow = new Form2();
                mainWindow.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Неверные данные авторизации!");
            }

            string sha256(string randomString)
            {
                var crypt = System.Security.Cryptography.SHA256.Create();
                return string.Concat(crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString)).Select(x => $"{x:x2}"));
            }
        }
    }
}
