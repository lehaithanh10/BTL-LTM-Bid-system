namespace ClientUI
{
    partial class FormRoom
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
            this.panelRoomInfo = new System.Windows.Forms.Panel();
            this.buttonLeaveRoom = new System.Windows.Forms.Button();
            this.buttonAddItem = new System.Windows.Forms.Button();
            this.labelUsersCount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelItemsCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelCreator = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelRoomName = new System.Windows.Forms.Label();
            this.panelSessionInfo = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelBuyNowPrice = new System.Windows.Forms.Label();
            this.buttonBuyNow = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.labelHighestPrice = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.numPrice = new System.Windows.Forms.NumericUpDown();
            this.labelTime = new System.Windows.Forms.Label();
            this.buttonBid = new System.Windows.Forms.Button();
            this.labelHighestBuyer = new System.Windows.Forms.Label();
            this.labelItemName = new System.Windows.Forms.Label();
            this.tipRoom = new System.Windows.Forms.ToolTip(this.components);
            this.labelServerNotification = new System.Windows.Forms.Label();
            this.timerRoom = new System.Windows.Forms.Timer(this.components);
            this.panelRoomInfo.SuspendLayout();
            this.panelSessionInfo.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).BeginInit();
            this.SuspendLayout();
            // 
            // panelRoomInfo
            // 
            this.panelRoomInfo.BackColor = System.Drawing.Color.SeaGreen;
            this.panelRoomInfo.Controls.Add(this.buttonLeaveRoom);
            this.panelRoomInfo.Controls.Add(this.buttonAddItem);
            this.panelRoomInfo.Controls.Add(this.labelUsersCount);
            this.panelRoomInfo.Controls.Add(this.label5);
            this.panelRoomInfo.Controls.Add(this.labelItemsCount);
            this.panelRoomInfo.Controls.Add(this.label3);
            this.panelRoomInfo.Controls.Add(this.labelCreator);
            this.panelRoomInfo.Controls.Add(this.label1);
            this.panelRoomInfo.Controls.Add(this.labelRoomName);
            this.panelRoomInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRoomInfo.Location = new System.Drawing.Point(0, 0);
            this.panelRoomInfo.Name = "panelRoomInfo";
            this.panelRoomInfo.Size = new System.Drawing.Size(1007, 244);
            this.panelRoomInfo.TabIndex = 0;
            // 
            // buttonLeaveRoom
            // 
            this.buttonLeaveRoom.BackColor = System.Drawing.SystemColors.Control;
            this.buttonLeaveRoom.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLeaveRoom.ForeColor = System.Drawing.Color.Tomato;
            this.buttonLeaveRoom.Location = new System.Drawing.Point(576, 182);
            this.buttonLeaveRoom.Name = "buttonLeaveRoom";
            this.buttonLeaveRoom.Size = new System.Drawing.Size(194, 43);
            this.buttonLeaveRoom.TabIndex = 8;
            this.buttonLeaveRoom.Text = "Rời phòng";
            this.tipRoom.SetToolTip(this.buttonLeaveRoom, "Rời khỏi phòng đấu giá");
            this.buttonLeaveRoom.UseVisualStyleBackColor = false;
            this.buttonLeaveRoom.Click += new System.EventHandler(this.buttonLeaveRoom_Click);
            // 
            // buttonAddItem
            // 
            this.buttonAddItem.BackColor = System.Drawing.SystemColors.Control;
            this.buttonAddItem.Font = new System.Drawing.Font("Times New Roman", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddItem.ForeColor = System.Drawing.Color.LimeGreen;
            this.buttonAddItem.Location = new System.Drawing.Point(242, 182);
            this.buttonAddItem.Name = "buttonAddItem";
            this.buttonAddItem.Size = new System.Drawing.Size(194, 43);
            this.buttonAddItem.TabIndex = 7;
            this.buttonAddItem.Text = "Thêm vật phẩm";
            this.tipRoom.SetToolTip(this.buttonAddItem, "Thêm vật phẩm để đấu giá trong phòng");
            this.buttonAddItem.UseVisualStyleBackColor = false;
            this.buttonAddItem.Click += new System.EventHandler(this.buttonAddItem_Click);
            // 
            // labelUsersCount
            // 
            this.labelUsersCount.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUsersCount.ForeColor = System.Drawing.Color.DarkOrange;
            this.labelUsersCount.Location = new System.Drawing.Point(832, 128);
            this.labelUsersCount.Name = "labelUsersCount";
            this.labelUsersCount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelUsersCount.Size = new System.Drawing.Size(90, 26);
            this.labelUsersCount.TabIndex = 6;
            this.labelUsersCount.Text = "999";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Cornsilk;
            this.label5.Location = new System.Drawing.Point(625, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(171, 25);
            this.label5.TabIndex = 5;
            this.label5.Text = "Số người tham gia";
            // 
            // labelItemsCount
            // 
            this.labelItemsCount.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItemsCount.ForeColor = System.Drawing.Color.Cyan;
            this.labelItemsCount.Location = new System.Drawing.Point(294, 128);
            this.labelItemsCount.Name = "labelItemsCount";
            this.labelItemsCount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelItemsCount.Size = new System.Drawing.Size(90, 26);
            this.labelItemsCount.TabIndex = 4;
            this.labelItemsCount.Text = "99";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Cornsilk;
            this.label3.Location = new System.Drawing.Point(99, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "Số vật phẩm";
            // 
            // labelCreator
            // 
            this.labelCreator.AutoEllipsis = true;
            this.labelCreator.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCreator.ForeColor = System.Drawing.Color.Gold;
            this.labelCreator.Location = new System.Drawing.Point(289, 71);
            this.labelCreator.Name = "labelCreator";
            this.labelCreator.Size = new System.Drawing.Size(627, 26);
            this.labelCreator.TabIndex = 2;
            this.labelCreator.Text = "Vũ Xuân Bắc và những người bạn từ khắp nơi trên thế giới này";
            this.labelCreator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Cornsilk;
            this.label1.Location = new System.Drawing.Point(99, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Người tạo phòng";
            // 
            // labelRoomName
            // 
            this.labelRoomName.AutoEllipsis = true;
            this.labelRoomName.BackColor = System.Drawing.Color.LimeGreen;
            this.labelRoomName.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelRoomName.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRoomName.ForeColor = System.Drawing.Color.Crimson;
            this.labelRoomName.Location = new System.Drawing.Point(0, 0);
            this.labelRoomName.Name = "labelRoomName";
            this.labelRoomName.Size = new System.Drawing.Size(1007, 42);
            this.labelRoomName.TabIndex = 0;
            this.labelRoomName.Text = "Dân chơi phố Huế";
            this.labelRoomName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelSessionInfo
            // 
            this.panelSessionInfo.BackColor = System.Drawing.Color.DodgerBlue;
            this.panelSessionInfo.Controls.Add(this.panel2);
            this.panelSessionInfo.Controls.Add(this.panel1);
            this.panelSessionInfo.Controls.Add(this.labelHighestBuyer);
            this.panelSessionInfo.Controls.Add(this.labelItemName);
            this.panelSessionInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelSessionInfo.Location = new System.Drawing.Point(0, 289);
            this.panelSessionInfo.Name = "panelSessionInfo";
            this.panelSessionInfo.Size = new System.Drawing.Size(1007, 354);
            this.panelSessionInfo.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labelBuyNowPrice);
            this.panel2.Controls.Add(this.buttonBuyNow);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.labelHighestPrice);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 91);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(505, 263);
            this.panel2.TabIndex = 15;
            // 
            // labelBuyNowPrice
            // 
            this.labelBuyNowPrice.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBuyNowPrice.ForeColor = System.Drawing.Color.Gold;
            this.labelBuyNowPrice.Location = new System.Drawing.Point(279, 120);
            this.labelBuyNowPrice.Name = "labelBuyNowPrice";
            this.labelBuyNowPrice.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelBuyNowPrice.Size = new System.Drawing.Size(174, 36);
            this.labelBuyNowPrice.TabIndex = 18;
            this.labelBuyNowPrice.Text = "999";
            // 
            // buttonBuyNow
            // 
            this.buttonBuyNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBuyNow.BackColor = System.Drawing.SystemColors.Control;
            this.buttonBuyNow.Font = new System.Drawing.Font("Times New Roman", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBuyNow.ForeColor = System.Drawing.Color.Black;
            this.buttonBuyNow.Location = new System.Drawing.Point(252, 196);
            this.buttonBuyNow.Name = "buttonBuyNow";
            this.buttonBuyNow.Size = new System.Drawing.Size(193, 43);
            this.buttonBuyNow.TabIndex = 14;
            this.buttonBuyNow.Text = "Mua ngay";
            this.tipRoom.SetToolTip(this.buttonBuyNow, "Thêm vật phẩm để đấu giá trong phòng");
            this.buttonBuyNow.UseVisualStyleBackColor = false;
            this.buttonBuyNow.Click += new System.EventHandler(this.buttonBuyNow_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Cornsilk;
            this.label11.Location = new System.Drawing.Point(32, 125);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(128, 25);
            this.label11.TabIndex = 17;
            this.label11.Text = "Giá bán ngay";
            // 
            // labelHighestPrice
            // 
            this.labelHighestPrice.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHighestPrice.ForeColor = System.Drawing.Color.LawnGreen;
            this.labelHighestPrice.Location = new System.Drawing.Point(279, 60);
            this.labelHighestPrice.Name = "labelHighestPrice";
            this.labelHighestPrice.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelHighestPrice.Size = new System.Drawing.Size(174, 36);
            this.labelHighestPrice.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Cornsilk;
            this.label9.Location = new System.Drawing.Point(32, 65);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(123, 25);
            this.label9.TabIndex = 13;
            this.label9.Text = "Giá cao nhất";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.numPrice);
            this.panel1.Controls.Add(this.labelTime);
            this.panel1.Controls.Add(this.buttonBid);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(505, 91);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(502, 263);
            this.panel1.TabIndex = 14;
            // 
            // numPrice
            // 
            this.numPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numPrice.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numPrice.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numPrice.Location = new System.Drawing.Point(198, 135);
            this.numPrice.Maximum = new decimal(new int[] {
            -1530495976,
            232830,
            0,
            0});
            this.numPrice.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numPrice.Name = "numPrice";
            this.numPrice.Size = new System.Drawing.Size(245, 36);
            this.numPrice.TabIndex = 16;
            this.numPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numPrice.ThousandsSeparator = true;
            this.numPrice.Value = new decimal(new int[] {
            -1530495976,
            232830,
            0,
            0});
            // 
            // labelTime
            // 
            this.labelTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTime.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.ForeColor = System.Drawing.Color.Yellow;
            this.labelTime.Location = new System.Drawing.Point(0, 0);
            this.labelTime.Name = "labelTime";
            this.labelTime.Padding = new System.Windows.Forms.Padding(0, 0, 55, 0);
            this.labelTime.Size = new System.Drawing.Size(502, 69);
            this.labelTime.TabIndex = 15;
            this.labelTime.Text = "59:59";
            this.labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonBid
            // 
            this.buttonBid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBid.BackColor = System.Drawing.SystemColors.Control;
            this.buttonBid.Font = new System.Drawing.Font("Times New Roman", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBid.ForeColor = System.Drawing.Color.Blue;
            this.buttonBid.Location = new System.Drawing.Point(198, 196);
            this.buttonBid.Name = "buttonBid";
            this.buttonBid.Size = new System.Drawing.Size(245, 43);
            this.buttonBid.TabIndex = 13;
            this.buttonBid.Text = "Đấu giá";
            this.tipRoom.SetToolTip(this.buttonBid, "Thêm vật phẩm để đấu giá trong phòng");
            this.buttonBid.UseVisualStyleBackColor = false;
            this.buttonBid.Click += new System.EventHandler(this.buttonBid_Click);
            // 
            // labelHighestBuyer
            // 
            this.labelHighestBuyer.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelHighestBuyer.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHighestBuyer.ForeColor = System.Drawing.Color.Gold;
            this.labelHighestBuyer.Location = new System.Drawing.Point(0, 42);
            this.labelHighestBuyer.Name = "labelHighestBuyer";
            this.labelHighestBuyer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelHighestBuyer.Size = new System.Drawing.Size(1007, 49);
            this.labelHighestBuyer.TabIndex = 16;
            this.labelHighestBuyer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelItemName
            // 
            this.labelItemName.AutoEllipsis = true;
            this.labelItemName.BackColor = System.Drawing.Color.LightSkyBlue;
            this.labelItemName.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelItemName.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItemName.ForeColor = System.Drawing.Color.DarkMagenta;
            this.labelItemName.Location = new System.Drawing.Point(0, 0);
            this.labelItemName.Name = "labelItemName";
            this.labelItemName.Size = new System.Drawing.Size(1007, 42);
            this.labelItemName.TabIndex = 1;
            this.labelItemName.Text = "Giày kiên cường";
            this.labelItemName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelServerNotification
            // 
            this.labelServerNotification.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelServerNotification.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelServerNotification.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelServerNotification.Location = new System.Drawing.Point(0, 244);
            this.labelServerNotification.Name = "labelServerNotification";
            this.labelServerNotification.Size = new System.Drawing.Size(1007, 45);
            this.labelServerNotification.TabIndex = 2;
            this.labelServerNotification.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerRoom
            // 
            this.timerRoom.Interval = 1000;
            this.timerRoom.Tick += new System.EventHandler(this.SessionTimer_Tick);
            // 
            // FormRoom
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1007, 643);
            this.Controls.Add(this.labelServerNotification);
            this.Controls.Add(this.panelSessionInfo);
            this.Controls.Add(this.panelRoomInfo);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FormRoom";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phòng đấu giá";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormRoom_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormRoom_FormClosed);
            this.Load += new System.EventHandler(this.FormRoom_Load);
            this.panelRoomInfo.ResumeLayout(false);
            this.panelRoomInfo.PerformLayout();
            this.panelSessionInfo.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelRoomInfo;
        private System.Windows.Forms.Button buttonAddItem;
        private System.Windows.Forms.Label labelUsersCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelItemsCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelCreator;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelRoomName;
        private System.Windows.Forms.Panel panelSessionInfo;
        private System.Windows.Forms.Label labelItemName;
        private System.Windows.Forms.ToolTip tipRoom;
        private System.Windows.Forms.Button buttonLeaveRoom;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelBuyNowPrice;
        private System.Windows.Forms.Button buttonBuyNow;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label labelHighestBuyer;
        private System.Windows.Forms.Label labelHighestPrice;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Button buttonBid;
        private System.Windows.Forms.NumericUpDown numPrice;
        private System.Windows.Forms.Label labelServerNotification;
        private System.Windows.Forms.Timer timerRoom;
    }
}