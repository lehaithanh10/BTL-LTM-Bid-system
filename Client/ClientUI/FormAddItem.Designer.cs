namespace ClientUI
{
    partial class FormAddItem
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
            this.listItems = new System.Windows.Forms.ListBox();
            this.contextListItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextItemDeleteCurrent = new System.Windows.Forms.ToolStripMenuItem();
            this.contextItemClearAll = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.textName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.tipFormAddItem = new System.Windows.Forms.ToolTip(this.components);
            this.numInitialPrice = new System.Windows.Forms.NumericUpDown();
            this.numBuyNowPrice = new System.Windows.Forms.NumericUpDown();
            this.buttonOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.contextListItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numInitialPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBuyNowPrice)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listItems
            // 
            this.listItems.BackColor = System.Drawing.Color.Bisque;
            this.listItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listItems.ContextMenuStrip = this.contextListItem;
            this.listItems.Dock = System.Windows.Forms.DockStyle.Top;
            this.listItems.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listItems.FormattingEnabled = true;
            this.listItems.ItemHeight = 25;
            this.listItems.Location = new System.Drawing.Point(0, 44);
            this.listItems.Margin = new System.Windows.Forms.Padding(0);
            this.listItems.Name = "listItems";
            this.listItems.Size = new System.Drawing.Size(320, 375);
            this.listItems.TabIndex = 1;
            this.tipFormAddItem.SetToolTip(this.listItems, "Danh sách vật phẩm đã tạo (chưa được gửi)");
            this.listItems.SelectedIndexChanged += new System.EventHandler(this.listItems_SelectedIndexChanged);
            // 
            // contextListItem
            // 
            this.contextListItem.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextListItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextItemDeleteCurrent,
            this.contextItemClearAll});
            this.contextListItem.Name = "listItemContext";
            this.contextListItem.Size = new System.Drawing.Size(146, 52);
            // 
            // contextItemDeleteCurrent
            // 
            this.contextItemDeleteCurrent.Name = "contextItemDeleteCurrent";
            this.contextItemDeleteCurrent.Size = new System.Drawing.Size(145, 24);
            this.contextItemDeleteCurrent.Text = "Xóa";
            this.contextItemDeleteCurrent.Click += new System.EventHandler(this.contextItemDeleteCurrent_Click);
            // 
            // contextItemClearAll
            // 
            this.contextItemClearAll.Name = "contextItemClearAll";
            this.contextItemClearAll.Size = new System.Drawing.Size(145, 24);
            this.contextItemClearAll.Text = "Xóa tất cả";
            this.contextItemClearAll.Click += new System.EventHandler(this.contextItemClearAll_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Cornsilk;
            this.label1.Location = new System.Drawing.Point(33, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên vật phẩm";
            // 
            // textName
            // 
            this.textName.Location = new System.Drawing.Point(210, 77);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(273, 30);
            this.textName.TabIndex = 2;
            this.tipFormAddItem.SetToolTip(this.textName, "Không được để giá trị mặc định \'*\'");
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.LightSalmon;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(529, 44);
            this.label2.TabIndex = 0;
            this.label2.Text = "Thông tin vật phẩm";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textDescription
            // 
            this.textDescription.Location = new System.Drawing.Point(210, 147);
            this.textDescription.Multiline = true;
            this.textDescription.Name = "textDescription";
            this.textDescription.Size = new System.Drawing.Size(273, 141);
            this.textDescription.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Cornsilk;
            this.label3.Location = new System.Drawing.Point(33, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 27);
            this.label3.TabIndex = 0;
            this.label3.Text = "Mô tả";
            this.tipFormAddItem.SetToolTip(this.label3, "Thông tin về vật phẩm");
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Cornsilk;
            this.label4.Location = new System.Drawing.Point(33, 327);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 27);
            this.label4.TabIndex = 0;
            this.label4.Text = "Giá khởi điểm";
            this.tipFormAddItem.SetToolTip(this.label4, "Giá trị đấu giá cho vật phẩm hiện tại không thấp hơn Giá khởi điểm");
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Cornsilk;
            this.label5.Location = new System.Drawing.Point(33, 397);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(152, 27);
            this.label5.TabIndex = 0;
            this.label5.Text = "Giá bán ngay";
            this.tipFormAddItem.SetToolTip(this.label5, "Giá trị mà người tham gia có thể trả ngay cho người bán để lấy vật phẩm không qua" +
        " đấu giá");
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.BackColor = System.Drawing.Color.Yellow;
            this.buttonConfirm.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonConfirm.Location = new System.Drawing.Point(210, 447);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(171, 43);
            this.buttonConfirm.TabIndex = 6;
            this.buttonConfirm.Text = "Thêm vật phẩm";
            this.tipFormAddItem.SetToolTip(this.buttonConfirm, "Thêm vật phẩm vào danh sách vật phẩm muốn đấu giá");
            this.buttonConfirm.UseVisualStyleBackColor = false;
            this.buttonConfirm.Click += new System.EventHandler(this.buttonConfirm_Click);
            // 
            // numInitialPrice
            // 
            this.numInitialPrice.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numInitialPrice.Location = new System.Drawing.Point(210, 324);
            this.numInitialPrice.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numInitialPrice.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numInitialPrice.Name = "numInitialPrice";
            this.numInitialPrice.Size = new System.Drawing.Size(273, 30);
            this.numInitialPrice.TabIndex = 4;
            this.numInitialPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numInitialPrice.ThousandsSeparator = true;
            this.tipFormAddItem.SetToolTip(this.numInitialPrice, "Ít nhất 1,000 và lẻ đến hàng nghìn");
            this.numInitialPrice.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numInitialPrice.ValueChanged += new System.EventHandler(this.PriceValueChanged);
            // 
            // numBuyNowPrice
            // 
            this.numBuyNowPrice.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numBuyNowPrice.Location = new System.Drawing.Point(210, 394);
            this.numBuyNowPrice.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numBuyNowPrice.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numBuyNowPrice.Name = "numBuyNowPrice";
            this.numBuyNowPrice.Size = new System.Drawing.Size(273, 30);
            this.numBuyNowPrice.TabIndex = 5;
            this.numBuyNowPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numBuyNowPrice.ThousandsSeparator = true;
            this.tipFormAddItem.SetToolTip(this.numBuyNowPrice, "Ít nhất 1,000 và lẻ đến hàng nghìn");
            this.numBuyNowPrice.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numBuyNowPrice.ValueChanged += new System.EventHandler(this.PriceValueChanged);
            // 
            // buttonOk
            // 
            this.buttonOk.BackColor = System.Drawing.Color.Chartreuse;
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOk.Location = new System.Drawing.Point(68, 447);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(171, 43);
            this.buttonOk.TabIndex = 7;
            this.buttonOk.Text = "Hoàn thành";
            this.tipFormAddItem.SetToolTip(this.buttonOk, "Xác nhận và gửi danh sách hiện tại");
            this.buttonOk.UseVisualStyleBackColor = false;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Bisque;
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Controls.Add(this.listItems);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(320, 512);
            this.panel1.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(320, 44);
            this.label6.TabIndex = 0;
            this.label6.Text = "Danh sách vật phẩm";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.numBuyNowPrice);
            this.panel2.Controls.Add(this.textName);
            this.panel2.Controls.Add(this.numInitialPrice);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.buttonConfirm);
            this.panel2.Controls.Add(this.textDescription);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(320, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(529, 512);
            this.panel2.TabIndex = 14;
            // 
            // FormAddItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OrangeRed;
            this.ClientSize = new System.Drawing.Size(849, 512);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FormAddItem";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm vật phẩm";
            this.contextListItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numInitialPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBuyNowPrice)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listItems;
        private System.Windows.Forms.ContextMenuStrip contextListItem;
        private System.Windows.Forms.ToolStripMenuItem contextItemDeleteCurrent;
        private System.Windows.Forms.ToolStripMenuItem contextItemClearAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.ToolTip tipFormAddItem;
        private System.Windows.Forms.NumericUpDown numInitialPrice;
        private System.Windows.Forms.NumericUpDown numBuyNowPrice;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Panel panel2;
    }
}