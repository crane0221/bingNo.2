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
using System.Collections;

namespace 撲克牌
{
    public partial class Form1 : Form
    {
        static int rno, gno;
        static int[] d = new int[100];
        ArrayList card = new ArrayList();
        ArrayList pf = new ArrayList();
        ArrayList pn = new ArrayList();
        ArrayList pn1 = new ArrayList();
        ArrayList bf = new ArrayList();
        ArrayList bn = new ArrayList();
        ArrayList bn1 = new ArrayList();

        string[] suit = new string[4];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rdata();
            sp1();
            sp2();
            sp3();
            wdata();
        }
        private void rdata()
        {
            StreamReader file = new StreamReader(@"C:\Users\user\Desktop\丙檢題本\1060307.sm");
            rno = 0;
            gno = int.Parse(file.ReadLine());
            double[] f = new double[100];
            string[] a = new string[100];            
            while ((a[rno] = file.ReadLine()) != null)
            {
                f[rno] = float.Parse(a[rno]);
                d[rno] = (int)(Math.Floor(f[rno] * 52));
                rno = rno + 1;
            }
            file.Close();
        }
        private void sp1()
        {
            suit[0] = "♠";
            suit[1] = "♥";
            suit[2] = "♦";
            suit[3] = "♣";
        }
        private void sp2()
        {
            for (int i = 1; i <= rno; i++)
            {
                if (!card.Contains(d[i]))
                {
                    card.Add(d[i]);
                }
            }
        }
        private void sp3()
        {
            for (int i = 0; i <= gno * 2 - 1; i++)
            {
                int f = (Convert.ToInt32(card[i]) + 1) / 13;
                int n = Convert.ToInt32(card[i]) % 13 + 1;
                string[] num = { "*", "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
                string n1 = num[n];
                if (n == 1)
                    n = n + 13;
                if (i % 2 == 0)
                {
                    pf.Add(f);
                    pn.Add(n);
                    pn1.Add(n1);
                }
                else
                {
                    bf.Add(f);
                    bn.Add(n);
                    bn1.Add(n1);
                }
            }
        }
        private void wdata()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("序號");
            dt.Columns.Add("玩家");
            dt.Columns.Add("莊家");
            dt.Columns.Add("結果");

            for (int i = 0; i <= gno - 1; i++)
            {
                DataRow dr = dt.NewRow();
                int f = Convert.ToInt32(pf[i]);
                int p = Convert.ToInt32(pn[i]);
                int b = Convert.ToInt32(bn[i]);
                dr[0] = i + 1;
                dr[1] = suit[f] + pn1[i];
                dr[2] = suit[f] + bn1[i];
                if (p > b)
                {
                    dr[3] = "玩家贏";
                }
                else if (p == b)
                {
                    dr[3] = "莊家贏";
                }
                else if (p < b)
                {
                    dr[3] = "平手";
                }
                dt.Rows.Add(dr);
            }
            dgv.DataSource = dt;
        }
    }
}
