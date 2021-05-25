using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace Lab4
{
    public partial class Form1 : Form
    {
        int currentRow = -1;
        static string name;
        static string size;
        static string type;
        static string weight;
        public List<string> test = new List<string>();

        public Form1()
        {
            InitializeComponent();
            DataBase.SqlConnect();
            ShowTable();
        }

        void ShowTable()
        {
            dataGridView1.Rows.Clear();
            DataBase.connection.Open();
            SQLiteCommand cmd = new SQLiteCommand(DataBase.connection);
            DataTable dt = new DataTable();
            cmd.CommandText = "select * from Space";
            cmd.ExecuteNonQuery();
            SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(cmd);
            sQLiteDataAdapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                    dataGridView1.Rows.Add(dt.Rows[i].ItemArray);
            }
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
            DataBase.connection.Close();
            textBox1.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
            textBox2.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.Rows[0].Cells[4].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            comboBox1.Text = comboBox1.Items[0].ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                name = textBox1.Text;
                type = comboBox1.Text.ToString();
                size = textBox2.Text;
                weight = textBox3.Text;
                if (textBox1.Text == "" || comboBox1.Text == "")
                {
                    throw new Exception();
                }
                else if (SearchName(name, dataGridView1))
                {
                    throw new Exception();
                }
                DataBase.AddRow(name, type, size, weight);
                ShowTable();
            }
            catch
            {
                MessageBox.Show("Некорректные данные.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            currentRow = dataGridView1.CurrentCell.RowIndex;
            if (currentRow < 0)
            {
                return;
            }
            string message = "Вы уверены, что хотите удалить эту строку?";
            string title = "Удаление строки";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {

                int id = int.Parse(dataGridView1.Rows[currentRow].Cells[0].Value.ToString());
                DataBase.DeleteRow(id);
                ShowTable();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            currentRow = dataGridView1.CurrentCell.RowIndex;
            int currentId = int.Parse(dataGridView1.Rows[currentRow].Cells[0].Value.ToString());
            try
            {
                name = textBox1.Text;
                type = comboBox1.Text.ToString();
                size = textBox2.Text;
                weight = textBox3.Text;
                if (textBox1.Text == "" || comboBox1.Text == "")
                {
                    throw new Exception();
                }
                else if (SearchNameEdit(currentId, name, dataGridView1))
                {
                    throw new Exception();
                }
                DataBase.EditRow(currentId, name, type, size, weight);
                ShowTable();
            }
            catch
            {
                MessageBox.Show("Некорректные данные.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            saveToFile(saveFileDialog1, dataGridView1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form f2 = new Form2();
            f2.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            //f2.timer1.Enabled = false;
            Properties.Settings.Default.timer = false;
            Properties.Settings.Default.Save();
            f2.Show();
            Properties.Settings.Default.timer = true;
            Properties.Settings.Default.Save();
        }

        public static bool SearchName(string newName, DataGridView dg)
        {
            for (int i = 0; i < dg.Rows.Count; i++)
            {
                if (dg.Rows[i].Cells[1].Value.ToString() == newName)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool SearchNameEdit(int id, string name, DataGridView dg)
        {
            for (int i = 0; i < dg.Rows.Count; i++)
            {
                if (int.Parse(dg.Rows[i].Cells[0].Value.ToString()) == id) continue;
                if (dg.Rows[i].Cells[1].Value.ToString() == name)
                {
                    return true;
                }
            }
            return false;
        }

        public static void saveToFile(SaveFileDialog sf, DataGridView dg)
        {
            sf.Title = "Save text Files";
            if (sf.ShowDialog() != DialogResult.OK)
                return;
            string line;
            using (StreamWriter writer = new StreamWriter(sf.FileName + ".txt"))
            {
                line = "ID, Имя,    Тип,   Размер,        Масса";
                writer.WriteLine(line);
                for (int i = 0; i < dg.Rows.Count; i++)
                {
                    line = dg.Rows[i].Cells[0].Value.ToString() + ", " + dg.Rows[i].Cells[1].Value.ToString() + ", " + dg.Rows[i].Cells[2].Value.ToString() + ", " + dg.Rows[i].Cells[3].Value.ToString() + ", " + dg.Rows[i].Cells[4].Value.ToString();
                    writer.WriteLine(line);
                }


            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            currentRow = dataGridView1.CurrentCell.RowIndex;
            textBox1.Text = dataGridView1.Rows[currentRow].Cells[1].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[currentRow].Cells[2].Value.ToString();
            textBox2.Text = dataGridView1.Rows[currentRow].Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.Rows[currentRow].Cells[4].Value.ToString();
        }

        public int CountRows()
        {
            dataGridView1.Rows.Clear();
            DataBase.connection.Open();
            SQLiteCommand cmd = new SQLiteCommand(DataBase.connection);
            DataTable dt = new DataTable();
            cmd.CommandText = "select * from Space";
            cmd.ExecuteNonQuery();
            SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(cmd);
            sQLiteDataAdapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                    dataGridView1.Rows.Add(dt.Rows[i].ItemArray);
            }
            DataBase.connection.Close();
            return dt.Rows.Count;
        }

        public int SearchID(string name)
        {
            int id=-1;
            ShowTable();
            for(int i=0; i<dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[1].Value.ToString() == name)
                {
                    id= int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                }
            }
            return id;
        }

        public void SearchRow()
        {
            test = new List<string>(); 
            int count = -1;
            ShowTable();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[1].Value.ToString() == "test1")
                {
                    count = i;
                    break;
                }
            }
            test.Add(dataGridView1.Rows[count].Cells[1].Value.ToString());
            test.Add(dataGridView1.Rows[count].Cells[2].Value.ToString());
            test.Add(dataGridView1.Rows[count].Cells[3].Value.ToString());
            test.Add(dataGridView1.Rows[count].Cells[4].Value.ToString());
        }
    }
}
