using ClientLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ClientUI
{
    public partial class FormAddRoom : Form
    {
        public ItemInfo[] FR_Items { get; private set; }
        public string FR_RoomName { get; private set; }
        public FormAddRoom()
        {
            InitializeComponent();
            listItems.DisplayMember = nameof(ItemInfo.Name);
        }

        private void buttonAddItems_Click(object sender, EventArgs e)
        {
            FormAddItem formAdd = new FormAddItem();
            if (formAdd.ShowDialog() == DialogResult.OK)
            {
                if(formAdd.FR_Items != null)
                    listItems.Items.AddRange(formAdd.FR_Items);
                listItems.SelectedIndex = -1;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            FR_RoomName = textRoomName.Text;
            if (String.IsNullOrEmpty(FR_RoomName))
            {
                MessageBox.Show("Tên phòng không được để trống", "Tên phòng không hợp lệ");
                this.DialogResult = DialogResult.None;
                textRoomName.Focus();
            }
            else
            {
                FR_Items = listItems.Items.OfType<ItemInfo>().ToArray();
                this.Close();
            }
        }

        private void contextItemDeleteCurrent_Click(object sender, EventArgs e)
        {
            listItems.Items.RemoveAt(listItems.SelectedIndex);
            if (listItems.Items.Count == 0)
                contextItemClearAll.Enabled = false;
        }

        private void contextItemClearAll_Click(object sender, EventArgs e)
        {
            listItems.Items.Clear();
            contextItemClearAll.Enabled = false;
        }

        private void listItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            contextItemDeleteCurrent.Enabled = listItems.SelectedIndex > -1;
        }
    }
}
