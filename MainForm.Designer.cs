
namespace model_auth
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.AddTag = new System.Windows.Forms.Button();
            this.AddReader = new System.Windows.Forms.Button();
            this.Model_Start = new System.Windows.Forms.Button();
            this.serverDataSet1 = new model_auth.serverDataSet();
            this.labelTag = new System.Windows.Forms.Label();
            this.labelReader = new System.Windows.Forms.Label();
            this.textBoxTag = new System.Windows.Forms.TextBox();
            this.textBoxReader = new System.Windows.Forms.TextBox();
            this.groupBoxAttacks = new System.Windows.Forms.GroupBox();
            this.groupBoxTime = new System.Windows.Forms.GroupBox();
            this.timeWD = new System.Windows.Forms.RadioButton();
            this.time54 = new System.Windows.Forms.RadioButton();
            this.time43 = new System.Windows.Forms.RadioButton();
            this.time32 = new System.Windows.Forms.RadioButton();
            this.time21 = new System.Windows.Forms.RadioButton();
            this.replaceMa4 = new System.Windows.Forms.CheckedListBox();
            this.replaceMa3 = new System.Windows.Forms.CheckedListBox();
            this.replaceMa2 = new System.Windows.Forms.CheckedListBox();
            this.replaceMa1 = new System.Windows.Forms.CheckedListBox();
            this.labelMa4 = new System.Windows.Forms.Label();
            this.labelMa3 = new System.Windows.Forms.Label();
            this.labelMa2 = new System.Windows.Forms.Label();
            this.labelMa1 = new System.Windows.Forms.Label();
            this.getHistory = new System.Windows.Forms.Button();
            this.getTagInSys = new System.Windows.Forms.Button();
            this.getReaderInSys = new System.Windows.Forms.Button();
            this.groupBoxWorkSys = new System.Windows.Forms.GroupBox();
            this.groupBoxParams = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.serverDataSet1)).BeginInit();
            this.groupBoxAttacks.SuspendLayout();
            this.groupBoxTime.SuspendLayout();
            this.groupBoxWorkSys.SuspendLayout();
            this.groupBoxParams.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddTag
            // 
            this.AddTag.Location = new System.Drawing.Point(20, 25);
            this.AddTag.Name = "AddTag";
            this.AddTag.Size = new System.Drawing.Size(130, 60);
            this.AddTag.TabIndex = 0;
            this.AddTag.Text = "Добавить метку";
            this.AddTag.UseVisualStyleBackColor = true;
            this.AddTag.Click += new System.EventHandler(this.AddTag_Click);
            // 
            // AddReader
            // 
            this.AddReader.Location = new System.Drawing.Point(156, 25);
            this.AddReader.Name = "AddReader";
            this.AddReader.Size = new System.Drawing.Size(130, 60);
            this.AddReader.TabIndex = 1;
            this.AddReader.Text = "Добавить считыватель";
            this.AddReader.UseVisualStyleBackColor = true;
            this.AddReader.Click += new System.EventHandler(this.AddReader_Click);
            // 
            // Model_Start
            // 
            this.Model_Start.Location = new System.Drawing.Point(483, 415);
            this.Model_Start.Name = "Model_Start";
            this.Model_Start.Size = new System.Drawing.Size(210, 60);
            this.Model_Start.TabIndex = 2;
            this.Model_Start.Text = "Начать моделирование";
            this.Model_Start.UseVisualStyleBackColor = true;
            this.Model_Start.Click += new System.EventHandler(this.Model_Start_Click);
            // 
            // serverDataSet1
            // 
            this.serverDataSet1.DataSetName = "serverDataSet";
            this.serverDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // labelTag
            // 
            this.labelTag.AutoSize = true;
            this.labelTag.Location = new System.Drawing.Point(6, 25);
            this.labelTag.Name = "labelTag";
            this.labelTag.Size = new System.Drawing.Size(60, 16);
            this.labelTag.TabIndex = 3;
            this.labelTag.Text = "id метки";
            // 
            // labelReader
            // 
            this.labelReader.AutoSize = true;
            this.labelReader.Location = new System.Drawing.Point(6, 57);
            this.labelReader.Name = "labelReader";
            this.labelReader.Size = new System.Drawing.Size(106, 16);
            this.labelReader.TabIndex = 4;
            this.labelReader.Text = "id считывателя";
            // 
            // textBoxTag
            // 
            this.textBoxTag.Location = new System.Drawing.Point(125, 25);
            this.textBoxTag.Name = "textBoxTag";
            this.textBoxTag.Size = new System.Drawing.Size(285, 22);
            this.textBoxTag.TabIndex = 5;
            // 
            // textBoxReader
            // 
            this.textBoxReader.Location = new System.Drawing.Point(125, 57);
            this.textBoxReader.Name = "textBoxReader";
            this.textBoxReader.Size = new System.Drawing.Size(285, 22);
            this.textBoxReader.TabIndex = 6;
            // 
            // groupBoxAttacks
            // 
            this.groupBoxAttacks.Controls.Add(this.groupBoxTime);
            this.groupBoxAttacks.Controls.Add(this.replaceMa4);
            this.groupBoxAttacks.Controls.Add(this.replaceMa3);
            this.groupBoxAttacks.Controls.Add(this.replaceMa2);
            this.groupBoxAttacks.Controls.Add(this.replaceMa1);
            this.groupBoxAttacks.Controls.Add(this.labelMa4);
            this.groupBoxAttacks.Controls.Add(this.labelMa3);
            this.groupBoxAttacks.Controls.Add(this.labelMa2);
            this.groupBoxAttacks.Controls.Add(this.labelMa1);
            this.groupBoxAttacks.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBoxAttacks.Location = new System.Drawing.Point(12, 135);
            this.groupBoxAttacks.Name = "groupBoxAttacks";
            this.groupBoxAttacks.Size = new System.Drawing.Size(1133, 274);
            this.groupBoxAttacks.TabIndex = 17;
            this.groupBoxAttacks.TabStop = false;
            this.groupBoxAttacks.Text = "Возможные атаки злоумышленника";
            // 
            // groupBoxTime
            // 
            this.groupBoxTime.Controls.Add(this.timeWD);
            this.groupBoxTime.Controls.Add(this.time54);
            this.groupBoxTime.Controls.Add(this.time43);
            this.groupBoxTime.Controls.Add(this.time32);
            this.groupBoxTime.Controls.Add(this.time21);
            this.groupBoxTime.Location = new System.Drawing.Point(741, 45);
            this.groupBoxTime.Name = "groupBoxTime";
            this.groupBoxTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBoxTime.Size = new System.Drawing.Size(324, 149);
            this.groupBoxTime.TabIndex = 25;
            this.groupBoxTime.TabStop = false;
            this.groupBoxTime.Text = "Задержка времени";
            // 
            // timeWD
            // 
            this.timeWD.AutoSize = true;
            this.timeWD.Location = new System.Drawing.Point(6, 123);
            this.timeWD.Name = "timeWD";
            this.timeWD.Size = new System.Drawing.Size(118, 20);
            this.timeWD.TabIndex = 18;
            this.timeWD.Text = "нет задержки";
            this.timeWD.UseVisualStyleBackColor = true;
            // 
            // time54
            // 
            this.time54.AutoSize = true;
            this.time54.Location = new System.Drawing.Point(6, 99);
            this.time54.Name = "time54";
            this.time54.Size = new System.Drawing.Size(283, 20);
            this.time54.TabIndex = 17;
            this.time54.TabStop = true;
            this.time54.Text = "между считывателем (T4) и меткой (T5)";
            this.time54.UseVisualStyleBackColor = true;
            // 
            // time43
            // 
            this.time43.AutoSize = true;
            this.time43.Location = new System.Drawing.Point(6, 73);
            this.time43.Name = "time43";
            this.time43.Size = new System.Drawing.Size(303, 20);
            this.time43.TabIndex = 17;
            this.time43.TabStop = true;
            this.time43.Text = "между сервером (Т3) и считывателем (T4) ";
            this.time43.UseVisualStyleBackColor = true;
            // 
            // time32
            // 
            this.time32.AutoSize = true;
            this.time32.Location = new System.Drawing.Point(6, 47);
            this.time32.Name = "time32";
            this.time32.Size = new System.Drawing.Size(294, 20);
            this.time32.TabIndex = 17;
            this.time32.TabStop = true;
            this.time32.Text = "между считывателем(T2) и сервером(T3)";
            this.time32.UseVisualStyleBackColor = true;
            // 
            // time21
            // 
            this.time21.AutoSize = true;
            this.time21.Location = new System.Drawing.Point(6, 21);
            this.time21.Name = "time21";
            this.time21.Size = new System.Drawing.Size(277, 20);
            this.time21.TabIndex = 0;
            this.time21.TabStop = true;
            this.time21.Text = "между меткой(T1) и считывателем(T2)";
            this.time21.UseVisualStyleBackColor = true;
            // 
            // replaceMa4
            // 
            this.replaceMa4.FormattingEnabled = true;
            this.replaceMa4.Items.AddRange(new object[] {
            "хэш V4",
            "зашифрованный AID Zt"});
            this.replaceMa4.Location = new System.Drawing.Point(358, 156);
            this.replaceMa4.Name = "replaceMa4";
            this.replaceMa4.Size = new System.Drawing.Size(255, 89);
            this.replaceMa4.TabIndex = 24;
            // 
            // replaceMa3
            // 
            this.replaceMa3.FormattingEnabled = true;
            this.replaceMa3.Items.AddRange(new object[] {
            "хэш V3",
            "хэш V4",
            "зашифрованный AID Zt"});
            this.replaceMa3.Location = new System.Drawing.Point(358, 45);
            this.replaceMa3.Name = "replaceMa3";
            this.replaceMa3.Size = new System.Drawing.Size(255, 89);
            this.replaceMa3.TabIndex = 23;
            // 
            // replaceMa2
            // 
            this.replaceMa2.FormattingEnabled = true;
            this.replaceMa2.Items.AddRange(new object[] {
            "случайное число Ny",
            "хэш V2",
            "идентификатор считывателя Ri"});
            this.replaceMa2.Location = new System.Drawing.Point(53, 156);
            this.replaceMa2.Name = "replaceMa2";
            this.replaceMa2.Size = new System.Drawing.Size(255, 89);
            this.replaceMa2.TabIndex = 22;
            // 
            // replaceMa1
            // 
            this.replaceMa1.FormattingEnabled = true;
            this.replaceMa1.Items.AddRange(new object[] {
            "случайное число Nx",
            "хэш V1",
            "одноразовый идентификатор AID"});
            this.replaceMa1.Location = new System.Drawing.Point(53, 45);
            this.replaceMa1.Name = "replaceMa1";
            this.replaceMa1.Size = new System.Drawing.Size(255, 89);
            this.replaceMa1.TabIndex = 21;
            // 
            // labelMa4
            // 
            this.labelMa4.AutoSize = true;
            this.labelMa4.Location = new System.Drawing.Point(385, 137);
            this.labelMa4.Name = "labelMa4";
            this.labelMa4.Size = new System.Drawing.Size(195, 16);
            this.labelMa4.TabIndex = 20;
            this.labelMa4.Text = "замена данных в пакете Ma4";
            // 
            // labelMa3
            // 
            this.labelMa3.AutoSize = true;
            this.labelMa3.Location = new System.Drawing.Point(385, 26);
            this.labelMa3.Name = "labelMa3";
            this.labelMa3.Size = new System.Drawing.Size(195, 16);
            this.labelMa3.TabIndex = 19;
            this.labelMa3.Text = "замена данных в пакете Ma3";
            // 
            // labelMa2
            // 
            this.labelMa2.AutoSize = true;
            this.labelMa2.Location = new System.Drawing.Point(88, 137);
            this.labelMa2.Name = "labelMa2";
            this.labelMa2.Size = new System.Drawing.Size(195, 16);
            this.labelMa2.TabIndex = 18;
            this.labelMa2.Text = "замена данных в пакете Ma2";
            // 
            // labelMa1
            // 
            this.labelMa1.AutoSize = true;
            this.labelMa1.Location = new System.Drawing.Point(88, 26);
            this.labelMa1.Name = "labelMa1";
            this.labelMa1.Size = new System.Drawing.Size(195, 16);
            this.labelMa1.TabIndex = 17;
            this.labelMa1.Text = "замена данных в пакете Ma1";
            // 
            // getHistory
            // 
            this.getHistory.Location = new System.Drawing.Point(292, 25);
            this.getHistory.Name = "getHistory";
            this.getHistory.Size = new System.Drawing.Size(130, 60);
            this.getHistory.TabIndex = 18;
            this.getHistory.Text = "История подключений";
            this.getHistory.UseVisualStyleBackColor = true;
            this.getHistory.Click += new System.EventHandler(this.getHistory_Click);
            // 
            // getTagInSys
            // 
            this.getTagInSys.Location = new System.Drawing.Point(428, 25);
            this.getTagInSys.Name = "getTagInSys";
            this.getTagInSys.Size = new System.Drawing.Size(130, 60);
            this.getTagInSys.TabIndex = 19;
            this.getTagInSys.Text = "Список меток в системе";
            this.getTagInSys.UseVisualStyleBackColor = true;
            this.getTagInSys.Click += new System.EventHandler(this.getTagInSys_Click);
            // 
            // getReaderInSys
            // 
            this.getReaderInSys.Location = new System.Drawing.Point(564, 25);
            this.getReaderInSys.Name = "getReaderInSys";
            this.getReaderInSys.Size = new System.Drawing.Size(130, 60);
            this.getReaderInSys.TabIndex = 20;
            this.getReaderInSys.Text = "Список считывателей в системе";
            this.getReaderInSys.UseVisualStyleBackColor = true;
            this.getReaderInSys.Click += new System.EventHandler(this.getReaderInSys_Click);
            // 
            // groupBoxWorkSys
            // 
            this.groupBoxWorkSys.Controls.Add(this.AddReader);
            this.groupBoxWorkSys.Controls.Add(this.getReaderInSys);
            this.groupBoxWorkSys.Controls.Add(this.AddTag);
            this.groupBoxWorkSys.Controls.Add(this.getHistory);
            this.groupBoxWorkSys.Controls.Add(this.getTagInSys);
            this.groupBoxWorkSys.Location = new System.Drawing.Point(434, 12);
            this.groupBoxWorkSys.Name = "groupBoxWorkSys";
            this.groupBoxWorkSys.Size = new System.Drawing.Size(711, 100);
            this.groupBoxWorkSys.TabIndex = 21;
            this.groupBoxWorkSys.TabStop = false;
            this.groupBoxWorkSys.Text = "Работа с системой";
            // 
            // groupBoxParams
            // 
            this.groupBoxParams.Controls.Add(this.labelTag);
            this.groupBoxParams.Controls.Add(this.labelReader);
            this.groupBoxParams.Controls.Add(this.textBoxTag);
            this.groupBoxParams.Controls.Add(this.textBoxReader);
            this.groupBoxParams.Location = new System.Drawing.Point(12, 12);
            this.groupBoxParams.Name = "groupBoxParams";
            this.groupBoxParams.Size = new System.Drawing.Size(416, 100);
            this.groupBoxParams.TabIndex = 22;
            this.groupBoxParams.TabStop = false;
            this.groupBoxParams.Text = "Параметры для моделирования процесса";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1157, 487);
            this.Controls.Add(this.groupBoxParams);
            this.Controls.Add(this.groupBoxWorkSys);
            this.Controls.Add(this.groupBoxAttacks);
            this.Controls.Add(this.Model_Start);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.serverDataSet1)).EndInit();
            this.groupBoxAttacks.ResumeLayout(false);
            this.groupBoxAttacks.PerformLayout();
            this.groupBoxTime.ResumeLayout(false);
            this.groupBoxTime.PerformLayout();
            this.groupBoxWorkSys.ResumeLayout(false);
            this.groupBoxParams.ResumeLayout(false);
            this.groupBoxParams.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button AddTag;
        private System.Windows.Forms.Button AddReader;
        private System.Windows.Forms.Button Model_Start;
        private serverDataSet serverDataSet1;
        private System.Windows.Forms.Label labelTag;
        private System.Windows.Forms.Label labelReader;
        private System.Windows.Forms.TextBox textBoxTag;
        private System.Windows.Forms.TextBox textBoxReader;
        private System.Windows.Forms.GroupBox groupBoxAttacks;
        private System.Windows.Forms.GroupBox groupBoxTime;
        private System.Windows.Forms.RadioButton timeWD;
        private System.Windows.Forms.RadioButton time54;
        private System.Windows.Forms.RadioButton time43;
        private System.Windows.Forms.RadioButton time32;
        private System.Windows.Forms.RadioButton time21;
        private System.Windows.Forms.CheckedListBox replaceMa4;
        private System.Windows.Forms.CheckedListBox replaceMa3;
        private System.Windows.Forms.CheckedListBox replaceMa2;
        private System.Windows.Forms.CheckedListBox replaceMa1;
        private System.Windows.Forms.Label labelMa4;
        private System.Windows.Forms.Label labelMa3;
        private System.Windows.Forms.Label labelMa2;
        private System.Windows.Forms.Label labelMa1;
        private System.Windows.Forms.Button getHistory;
        private System.Windows.Forms.Button getTagInSys;
        private System.Windows.Forms.Button getReaderInSys;
        private System.Windows.Forms.GroupBox groupBoxWorkSys;
        private System.Windows.Forms.GroupBox groupBoxParams;
    }
}

