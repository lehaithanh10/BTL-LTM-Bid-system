namespace ClientUI
{
    partial class FormMain
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelHeader = new System.Windows.Forms.Label();
            this.layoutRooms = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCreateRoom = new System.Windows.Forms.Button();
            this.tipFormMain = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.layoutRooms.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelHeader);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(860, 44);
            this.panel1.TabIndex = 0;
            // 
            // labelHeader
            // 
            this.labelHeader.BackColor = System.Drawing.Color.Yellow;
            this.labelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelHeader.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeader.ForeColor = System.Drawing.Color.Red;
            this.labelHeader.Location = new System.Drawing.Point(0, 0);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(860, 44);
            this.labelHeader.TabIndex = 0;
            this.labelHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // layoutRooms
            // 
            this.layoutRooms.AutoScroll = true;
            this.layoutRooms.Controls.Add(this.btnCreateRoom);
            this.layoutRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutRooms.Location = new System.Drawing.Point(0, 44);
            this.layoutRooms.Name = "layoutRooms";
            this.layoutRooms.Size = new System.Drawing.Size(860, 539);
            this.layoutRooms.TabIndex = 3;
            // 
            // btnCreateRoom
            // 
            this.btnCreateRoom.AutoEllipsis = true;
            this.btnCreateRoom.BackColor = System.Drawing.Color.LawnGreen;
            this.btnCreateRoom.BackgroundImage = global::ClientUI.Properties.Resources.icon_add;
            this.btnCreateRoom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCreateRoom.Font = new System.Drawing.Font("Times New Roman", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateRoom.Location = new System.Drawing.Point(30, 30);
            this.btnCreateRoom.Margin = new System.Windows.Forms.Padding(30);
            this.btnCreateRoom.Name = "btnCreateRoom";
            this.btnCreateRoom.Size = new System.Drawing.Size(215, 108);
            this.btnCreateRoom.TabIndex = 1;
            this.tipFormMain.SetToolTip(this.btnCreateRoom, "Tạo một phòng đấu giá mới");
            this.btnCreateRoom.UseVisualStyleBackColor = false;
            this.btnCreateRoom.Click += new System.EventHandler(this.btnCreateRoom_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(860, 583);
            this.Controls.Add(this.layoutRooms);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(320, 200);
            this.Name = "FormMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trang chủ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.panel1.ResumeLayout(false);
            this.layoutRooms.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.FlowLayoutPanel layoutRooms;
        private System.Windows.Forms.Button btnCreateRoom;
        private System.Windows.Forms.ToolTip tipFormMain;
    }
}

