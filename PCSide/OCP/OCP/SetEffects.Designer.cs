namespace OCP
{
    partial class SetEffects
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
            this.components = new System.ComponentModel.Container();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.ComboBox_category = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.ComboBox_position = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.ComboBox_effect = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.btn_ok = new MetroFramework.Controls.MetroButton();
            this.btn_cancel = new MetroFramework.Controls.MetroButton();
            this.tableLayoutPanel_effects = new System.Windows.Forms.TableLayoutPanel();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.ComboBox_audioDevice = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.tableLayoutPanel_effects.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
            this.metroStyleManager1.Style = MetroFramework.MetroColorStyle.Green;
            this.metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel1.Location = new System.Drawing.Point(23, 86);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(384, 19);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "Элемент для которого вы хотите установить эффект: ";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(50, 109);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(78, 19);
            this.metroLabel2.TabIndex = 1;
            this.metroLabel2.Text = "Категория: ";
            // 
            // ComboBox_category
            // 
            this.ComboBox_category.FormattingEnabled = true;
            this.ComboBox_category.ItemHeight = 23;
            this.ComboBox_category.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.ComboBox_category.Location = new System.Drawing.Point(134, 106);
            this.ComboBox_category.Name = "ComboBox_category";
            this.ComboBox_category.Size = new System.Drawing.Size(46, 29);
            this.ComboBox_category.TabIndex = 2;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(50, 147);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(54, 19);
            this.metroLabel3.TabIndex = 1;
            this.metroLabel3.Text = "Место: ";
            // 
            // ComboBox_position
            // 
            this.ComboBox_position.FormattingEnabled = true;
            this.ComboBox_position.ItemHeight = 23;
            this.ComboBox_position.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.ComboBox_position.Location = new System.Drawing.Point(134, 144);
            this.ComboBox_position.Name = "ComboBox_position";
            this.ComboBox_position.Size = new System.Drawing.Size(46, 29);
            this.ComboBox_position.TabIndex = 2;
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel4.Location = new System.Drawing.Point(24, 200);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(66, 19);
            this.metroLabel4.TabIndex = 3;
            this.metroLabel4.Text = "Эффект:";
            // 
            // ComboBox_effect
            // 
            this.ComboBox_effect.DisplayMember = "nn";
            this.ComboBox_effect.FormattingEnabled = true;
            this.ComboBox_effect.ItemHeight = 23;
            this.ComboBox_effect.Items.AddRange(new object[] {
            "Громкость устройства",
            "Яркость монитора",
            "Чувствительность мыши"});
            this.ComboBox_effect.Location = new System.Drawing.Point(96, 197);
            this.ComboBox_effect.Name = "ComboBox_effect";
            this.ComboBox_effect.Size = new System.Drawing.Size(208, 29);
            this.ComboBox_effect.TabIndex = 2;
            this.ComboBox_effect.SelectedIndexChanged += new System.EventHandler(this.ComboBox_effect_SelectedIndexChanged);
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel5.Location = new System.Drawing.Point(24, 248);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(152, 19);
            this.metroLabel5.TabIndex = 4;
            this.metroLabel5.Text = "Настройки эффекта:";
            // 
            // btn_ok
            // 
            this.btn_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ok.Location = new System.Drawing.Point(466, 566);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 5;
            this.btn_ok.Text = "Ок";
            this.btn_ok.Click += new System.EventHandler(this.Btn_ok_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(385, 566);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 5;
            this.btn_cancel.Text = "Отмена";
            this.btn_cancel.Click += new System.EventHandler(this.Btn_cancel_Click);
            // 
            // tableLayoutPanel_effects
            // 
            this.tableLayoutPanel_effects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel_effects.ColumnCount = 3;
            this.tableLayoutPanel_effects.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0F));
            this.tableLayoutPanel_effects.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_effects.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0F));
            this.tableLayoutPanel_effects.Controls.Add(this.metroPanel1, 1, 0);
            this.tableLayoutPanel_effects.Location = new System.Drawing.Point(24, 271);
            this.tableLayoutPanel_effects.Name = "tableLayoutPanel_effects";
            this.tableLayoutPanel_effects.RowCount = 1;
            this.tableLayoutPanel_effects.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_effects.Size = new System.Drawing.Size(516, 289);
            this.tableLayoutPanel_effects.TabIndex = 6;
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.ComboBox_audioDevice);
            this.metroPanel1.Controls.Add(this.metroLabel6);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(3, 3);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(510, 283);
            this.metroPanel1.TabIndex = 7;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // ComboBox_audioDevice
            // 
            this.ComboBox_audioDevice.FormattingEnabled = true;
            this.ComboBox_audioDevice.ItemHeight = 23;
            this.ComboBox_audioDevice.Location = new System.Drawing.Point(105, 14);
            this.ComboBox_audioDevice.Name = "ComboBox_audioDevice";
            this.ComboBox_audioDevice.Size = new System.Drawing.Size(233, 29);
            this.ComboBox_audioDevice.TabIndex = 3;
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.Location = new System.Drawing.Point(18, 14);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(81, 19);
            this.metroLabel6.TabIndex = 2;
            this.metroLabel6.Text = "Устройство:";
            // 
            // SetEffects
            // 
            this.AcceptButton = this.btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(564, 612);
            this.Controls.Add(this.tableLayoutPanel_effects);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.ComboBox_position);
            this.Controls.Add(this.ComboBox_effect);
            this.Controls.Add(this.ComboBox_category);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.MaximizeBox = false;
            this.Name = "SetEffects";
            this.Resizable = false;
            this.Text = "SetEffects";
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.tableLayoutPanel_effects.ResumeLayout(false);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroComboBox ComboBox_position;
        private MetroFramework.Controls.MetroComboBox ComboBox_category;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_effects;
        private MetroFramework.Controls.MetroButton btn_cancel;
        private MetroFramework.Controls.MetroButton btn_ok;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroComboBox ComboBox_effect;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroComboBox ComboBox_audioDevice;
        private MetroFramework.Controls.MetroLabel metroLabel6;
    }
}