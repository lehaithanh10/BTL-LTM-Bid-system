using ClientLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientUI
{
    public partial class FormAddItem : Form
    {
        /// <summary>
        /// Used for binding. This will poured on FR_Items
        /// </summary>
        private BindingList<ItemInfo> Items { get; set; }
        /// <summary>
        /// Used for binding
        /// </summary>
        private BindingSource listItemsBindingSource = new BindingSource();
        /// <summary>
        /// Added Items. null if none
        /// </summary>
        public ItemInfo[] FR_Items { get; private set; }
        public FormAddItem()
        {
            InitializeComponent();
            Items = new BindingList<ItemInfo>();

            Items.Add(ItemInfo.CreateItem());
            // TODO: Test
            Items.Add(ItemInfo.CreateItem("Giày kiên cường", "Kháng hiệu ứng 35%", 25000, 120000));

            listItemsBindingSource.DataSource = Items;
            listItems.DataSource = listItemsBindingSource;
            listItems.DisplayMember = nameof(ItemInfo.Name);

            textName.DataBindings.Add(new Binding(nameof(TextBox.Text), listItems.DataSource, nameof(ItemInfo.Name)));
            textDescription.DataBindings.Add(new Binding(nameof(TextBox.Text), listItems.DataSource, nameof(ItemInfo.Description)/*, false, DataSourceUpdateMode.OnPropertyChanged*/));
            numInitialPrice.DataBindings.Add(new Binding(nameof(NumericUpDown.Value), listItems.DataSource, nameof(ItemInfo.InitialPrice)/*, false, DataSourceUpdateMode.OnPropertyChanged*/));
            numBuyNowPrice.DataBindings.Add(new Binding(nameof(NumericUpDown.Value), listItems.DataSource, nameof(ItemInfo.BuyNowPrice)/*, false, DataSourceUpdateMode.OnPropertyChanged*/));
        }

        /// <summary>
        /// Callback method when User choose another Item on List.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listItems.SelectedIndex == 0)
            {
                contextItemDeleteCurrent.Enabled = false;
                buttonConfirm.Visible = true;
            }
            else
            {
                contextItemDeleteCurrent.Enabled = true;
                buttonConfirm.Visible = false;
            }
        }

        /// <summary>
        /// Callback method when user click on Add Item Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonConfirm_Click(object sender, EventArgs e)
        {

            if((listItems.SelectedItem as ItemInfo).Name.Equals("*"))
            {
                MessageBox.Show("Không thể dùng '*' cho Tên vật phẩm.", "Không thể thêm vật phẩm");
                return;
            }
            Items.Insert(0, ItemInfo.CreateItem());
            listItems.SelectedIndex = 0;
        }
        /// <summary>
        /// Callback method when user right click on <see cref="listItems"/> and choose Clear Current
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextItemDeleteCurrent_Click(object sender, EventArgs e)
        {
            Items.RemoveAt(listItems.SelectedIndex);
            if(Items.Count < 2)
                contextItemClearAll.Enabled = false;
        }
        /// <summary>
        /// Callback method when user right click on <see cref="listItems"/> and choose Clear All
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextItemClearAll_Click(object sender, EventArgs e)
        {
            while (Items.Count > 1)
                Items.RemoveAt(1);
            contextItemClearAll.Enabled = false;
        }

        /// <summary>
        /// Callback method when user click on submit button. Close current form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            var items = Items.ToArray();
            if(items.Length > 1)
            {
                FR_Items = items.Skip(1).ToArray();
            }
            else
            {
                FR_Items = null;
            }
            this.Close();
        }

        /// <summary>
        /// Callback method when the value of <see cref="numInitialPrice"/> or <see cref="numBuyNowPrice"/> changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PriceValueChanged(object sender, EventArgs e)
        {
            (sender as NumericUpDown).Value = Math.Round((sender as NumericUpDown).Value / 1000) * 1000;
        }
    }
}
