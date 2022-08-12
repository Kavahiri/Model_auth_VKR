using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace model_auth
{
    public partial class ProcessForm : Form
    {
        public string ProcessText
        {
            get { return processTextBox.Text; }
            set { processTextBox.AppendText(value+"\n"); }
        }
 
        public ProcessForm()
        {
            InitializeComponent();
        }

        private void SaveInTXT_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile1 = new SaveFileDialog();
            saveFile1.DefaultExt = "*.txt";
            saveFile1.Filter = "Text files|*.txt";
            if (saveFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
                saveFile1.FileName.Length > 0)
            {
                using (StreamWriter sw = new StreamWriter(saveFile1.FileName, true))
                {
                    sw.WriteLine(processTextBox.Text);
                    sw.Close();
                }
            }
            this.Close();
        }
    }
}
