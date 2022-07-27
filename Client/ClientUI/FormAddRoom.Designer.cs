namespace ClientUI
{
    partial class FormAddRoom
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textRoomName = new System.Windows.Forms.TextBox();
            this.buttonAddItems = new System.Windows.Forms.Button();
            this.listItems = new System.Windows.Forms.ListBox();
            this.contextListItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextItemDeleteCurrent = new System.Windows.Forms.ToolStripMenuItem();
            this.contextItemClearAll = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.contextListItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.LightSalmon;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(558, 44);
            this.label2.TabIndex = 1;
            this.label2.Text = "Thông tin phòng";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Cornsilk;
            this.label1.Location = new System.Drawing.Point(26, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên phòng";
            // 
            // textRoomName
            // 
            this.textRoomName.Enabled = false;
            this.textRoomName.Location = new System.Drawing.Point(203, 62);
            this.textRoomName.Name = "textRoomName";
            this.textRoomName.Size = new System.Drawing.Size(327, 30);
            this.textRoomName.TabIndex = 1;
            this.textRoomName.Text = "Phòng đấu giá";
            // 
            // buttonAddItems
            // 
            this.buttonAddItems.BackColor = System.Drawing.Color.Yellow;
            this.buttonAddItems.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddItems.Location = new System.Drawing.Point(31, 223);
            this.buttonAddItems.Name = "buttonAddItems";
            this.buttonAddItems.Size = new System.Drawing.Size(146, 39);
            this.buttonAddItems.TabIndex = 2;
            this.buttonAddItems.Text = "Thêm";
            this.buttonAddItems.UseVisualStyleBackColor = false;
            this.buttonAddItems.Click += new System.EventHandler(this.buttonAddItems_Click);
            // 
            // listItems
            // 
            this.listItems.BackColor = System.Drawing.Color.Bisque;
            this.listItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listItems.ContextMenuStrip = this.contextListItem;
            this.listItems.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listItems.FormattingEnabled = true;
            this.listItems.ItemHeight = 25;
            this.listItems.Location = new System.Drawing.Point(203, 136);
            this.listItems.Margin = new System.Windows.Forms.Padding(0);
            this.listItems.Name = "listItems";
            this.listItems.Size = new System.Drawing.Size(327, 225);
            this.listItems.TabIndex = 3;
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
            this.contextItemDeleteCurrent.Enabled = false;
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
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Cornsilk;
            this.label3.Location = new System.Drawing.Point(26, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 67);
            this.label3.TabIndex = 17;
            this.label3.Text = "Danh sách vật phẩm";
            // 
            // buttonOk
            // 
            this.buttonOk.BackColor = System.Drawing.Color.Chartreuse;
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOk.Location = new System.Drawing.Point(203, 384);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(170, 43);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Hoàn thành";
            this.buttonOk.UseVisualStyleBackColor = false;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // FormAddRoom
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.OrangeRed;
            this.ClientSize = new System.Drawing.Size(558, 449);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listItems);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textRoomName);
            this.Controls.Add(this.buttonAddItems);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FormAddRoom";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tạo phòng";
            this.contextListItem.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textRoomName;
        private System.Windows.Forms.Button buttonAddItems;
        private System.Windows.Forms.ListBox listItems;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.ContextMenuStrip contextListItem;
        private System.Windows.Forms.ToolStripMenuItem contextItemDeleteCurrent;
        private System.Windows.Forms.ToolStripMenuItem contextItemClearAll;
    }
}