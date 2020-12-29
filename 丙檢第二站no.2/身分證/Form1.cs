using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace 身分證
{
    public partial class Form1 : Form
    {
        static int rno;
        static string ec;
        static string [,] d = new string [100,4];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rdata();
            for (int i = 0;i < rno;i++)
            {
                ec = "";
                sp1(i);
                if (ec == "")
                    sp2(i);
                if (ec == "")
                    sp3(i);
                d[i, 3] = ec;
            }
            wdata();
        }
        private void rdata()
        {
            StreamReader file = new StreamReader(@".\sm\1060306.sm");
            rno = 0;
            string a;
            string[] b;
            while((a = file.ReadLine()) != null)
            {                       
                b = a.Split(',');
                d[rno, 0] = b[0];
                d[rno, 1] = b[1];
                d[rno, 2] = b[2];
                rno += 1;        
            }
        }
        private void sp1(int i)
        {
            string idno = d[i, 0];
            int m1 = idno.Length;
            if (m1 != 10)
            {
                ec = "FORMATE ERROR";
            }
            if (idno[0] < 'A' || idno[0] > 'Z')
            {
                ec = "FORMATE ERROR";
            }
            for (int j = 2;j <= 10;j++)
            {
                if (idno [j - 1] < '0' || idno [j - 1] > '9')
                {
                    ec = "FORMATE ERROR";
                }
            }
        }
        private void sp2(int i)
        {
            string str = d[i, 2];
            string x = str.Substring(0, 1);
            string str1 = d[i, 0];
            string y = str1.Substring(1, 1);
            string z = y + x;
            if (z != "1M" && z != "2F")
            {
                ec = "SEX CODE ERROR";
            }
        }
        private void sp3(int i)
        {
            string str = d[i, 0];
            string L1 = str.Substring(0, 1);
            string S26 = "ABCDEFGHJKLMNPQRSTUVXYWZIO";
            int m1 = S26.IndexOf(L1) + 10;
            int x1 = m1 / 10;
            int x2 = m1 % 10;
            int[] a = new int[10];
            for (int j = 1;j <= 9;j++)
            {
                a[j] = d[i, 0][j] - '0';
            }
            int Y = x1 + x2 * 9 + a[1] * 8 + a[2] * 7 + a[3] * 6 + a[4] * 5 + a[5] * 4 + a[6] * 3 + a[7] * 2 + a[8] + a[9];
            if (Y % 10 != 0)
            {
                ec = "CHECK SUM ERROR";
            }            
        }
        private void wdata()
        {
            DataTable dt = new DataTable ();
            dt.Columns.Add("ID_NO");
            dt.Columns.Add("NAME");
            dt.Columns.Add("SEX");
            dt.Columns.Add("ERROR");
            for (int i = 1;i <= rno;i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = d[i, 0];
                dr[1] = d[i, 1];
                dr[2] = d[i, 2];
                dr[3] = d[i, 3];
                dt.Rows.Add(dr);
            }
            dgv.DataSource = dt;
            dgv.Sort(dgv.Columns[0], 0);
        }
    }
}
