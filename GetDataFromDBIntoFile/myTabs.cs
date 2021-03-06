﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GetDataFromDBIntoFile
{

    public delegate void RequestToReleaseTab(myTabs oTab);

    public partial class myTabs : UserControl
    {
        public string Tablename {
            get { return table; }
            set { table = value; }
        }

        public string SelectCmd
        {
            get { return selectcmd; }
            set { selectcmd = value; }
        }

       

        private string table;
        private string selectcmd;
        private string connstring;
        private SqlDataAdapter oSda = new SqlDataAdapter();
        private DataTable oDT = new DataTable();


        public event RequestToReleaseTab ReleaseMe;
        

        public myTabs(string ctable, string cSelect, string Conn)
        {
            InitializeComponent();
            this.selectcmd = cSelect;
            this.table = ctable;
            this.connstring = Conn;
        }

        private void myTabs_Load(object sender, EventArgs e)
        {
            textBox1.Text = selectcmd;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.selectcmd = textBox1.Text;
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            ReleaseMe(this);

        }

        private void cmdExecute_Click(object sender, EventArgs e)
        {
            QueryData qd = new QueryData(this.connstring, this.selectcmd);
            DataTable dt = qd.ExecuteDataSet();
            this.dataGridView1.DataSource = dt;
        }
    }
}
