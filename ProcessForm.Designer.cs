namespace model_auth
{
    partial class ProcessForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessForm));
            this.processTextBox = new System.Windows.Forms.RichTextBox();
            this.SaveInTXT = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // processTextBox
            // 
            this.processTextBox.Location = new System.Drawing.Point(12, 12);
            this.processTextBox.Name = "processTextBox";
            this.processTextBox.Size = new System.Drawing.Size(758, 354);
            this.processTextBox.TabIndex = 0;
            this.processTextBox.Text = "";
            // 
            // SaveInTXT
            // 
            this.SaveInTXT.Location = new System.Drawing.Point(300, 381);
            this.SaveInTXT.Name = "SaveInTXT";
            this.SaveInTXT.Size = new System.Drawing.Size(198, 60);
            this.SaveInTXT.TabIndex = 1;
            this.SaveInTXT.Text = "Сохранить процесс в текстовый файл (TXT)";
            this.SaveInTXT.UseVisualStyleBackColor = true;
            this.SaveInTXT.Click += new System.EventHandler(this.SaveInTXT_Click);
            // 
            // ProcessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 453);
            this.Controls.Add(this.SaveInTXT);
            this.Controls.Add(this.processTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProcessForm";
            this.Text = "Process";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox processTextBox;
        private System.Windows.Forms.Button SaveInTXT;
    }
}