using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            DataTable dt = GetRandomDT();
            DTTextAlignment dTText = new DTTextAlignment(dt, CreateGraphics(), richTextBox1.Font);
            dTText.StartFlag = "#";
            richTextBox1.Text = dTText.GetResultStr(); 
            dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = GetRandomDT();
            DTTextAlignment dTText = new DTTextAlignment(dt, CreateGraphics(), richTextBox1.Font);
            dTText.TextPad = DTTextAlignment.TextPadEnum.Right;
            dTText.EndFlag = "#";
            richTextBox1.Text = dTText.GetResultStr();
            dataGridView1.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable dt = GetRandomDT();
            DTTextAlignment dTText = new DTTextAlignment(dt, CreateGraphics(), richTextBox1.Font);
            dTText.TextPad = DTTextAlignment.TextPadEnum.Middle;
            
            richTextBox1.Text = dTText.GetResultStr();
            dataGridView1.DataSource = dt;
        }

        Random r = new Random();

        public DataTable GetRandomDT(int ColCount = 6, int RowCount = 20)
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < ColCount; i++)
            {
                dt.Columns.Add(i.ToString());
            }
            

            for (int i = 0; i < RowCount; i++)
            {
                DataRow dr = dt.NewRow();
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    dr[c] = GetRandChar(r.Next(1, 10));

                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// 随机生成各种字符，有中文，英文，大小，和数字，字符等。这些数字在不同的字体占用的长度不一样。
        /// </summary>
        char[] EverrChars = new char[] { '中', '大', 'A', 'a', '4', '$', '{', '｛' };

        public string GetRandChar(int lenght)
        {
            char[] result = new char[lenght];
            for (int i = 0; i < lenght; i++)
            {
                result[i] = EverrChars[r.Next(0, EverrChars.Length )];
            }
            return new string(result);
            
        }

       
    }
}
