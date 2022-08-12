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

namespace model_auth
{
    public partial class TableForm : Form
    {
        DataSet dataset = new DataSet();
        public TableForm()
        {
            InitializeComponent();
        }
        public DataSet DataSetForm
        {
            get {return dataset;}
            set {dataset = value;}
        }

        private void TableForm_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataMember = DataSetForm.Tables[0].TableName;
            dataGridView1.DataSource = DataSetForm.Tables[0].DefaultView;
        }
    }
}
