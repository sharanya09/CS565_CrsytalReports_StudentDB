using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
namespace PRG25_STUD_CRYSTAL_DEMO
{
    public partial class Form1 : Form
    {
		SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\dbStudent.mdf;Integrated Security=True");
        SqlCommand com;
        SqlDataAdapter adp = new SqlDataAdapter();
        DataSet ds = new DataSet();
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            con.Open();
            com = new SqlCommand("insert into tbl_student values ("+txt_rollno.Text+",'"+int.Parse(txt_name.Text)+"','"+int.Parse(txt_sub1.Text)+"','"+int.Parse(txt_sub2.Text)+"','"+int.Parse(txt_sub3.Text)+"')",con);
            int x = com.ExecuteNonQuery();
            con.Close();
            if (x > 0)
            {
                ds.Tables[0].Rows.Clear();
                CrystalAddData();
                MessageBox.Show("Record Inserted");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            CrystalAddData();

        }

        public void CrystalAddData()
        {
            adp = new SqlDataAdapter("select * from tbl_student", con);
            adp.Fill(ds);
            ReportDocument rd = new ReportDocument();

			rd.Load(@"C:\Users\SharanyaC\Desktop\PRG25_STUD_CRYSTAL_DEMO\PRG25_STUD_CRYSTAL_DEMO\CrystalReport1.rpt");
            //rd.Load(Application.StartupPath+"CrystalReport1.rpt");
            rd.SetDataSource(ds.Tables[0]);
            crystalReportViewer1.ReportSource = rd;   
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            if (txt_search .Text == "")
            {
                MessageBox.Show("Enter Roll No");
            }
            else
            {
                ds.Tables[0].Rows.Clear();
                adp = new SqlDataAdapter("select * from tbl_student where sno=" + txt_search .Text + "", con);
                adp.Fill(ds);
                ReportDocument rd = new ReportDocument();
            //    rd.Load(Application.StartupPath+"CrystalReport1.rpt");
				rd.Load(@"C:\Users\SharanyaC\Desktop\PRG25_STUD_CRYSTAL_DEMO\PRG25_STUD_CRYSTAL_DEMO\CrystalReport1.rpt");
                rd.SetDataSource(ds.Tables[0]);
                crystalReportViewer1.ReportSource = rd;
            }
        }

        private void btn_all_Click(object sender, EventArgs e)
        {
            ds.Tables[0].Rows.Clear();
            CrystalAddData();
        }
    }
}
