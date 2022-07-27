using System.ComponentModel;

namespace ClientLibrary.Models
{
    //public class ItemInfo
    //{
    //    public string Name { get; set; }
    //    public string Description { get; set; }
    //    public uint InitialPrice { get; set; }
    //    public uint BuyNowPrice { get; set; }
    //}
    public class ItemInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public uint InitialPrice
        {
            get => _initial_price;
            set
            {
                _initial_price = value;
                OnPropertyChanged(nameof(InitialPrice));
            }
        }
        public uint BuyNowPrice
        {
            get => _buy_now_price;
            set
            {
                _buy_now_price = value;
                OnPropertyChanged(nameof(BuyNowPrice));
            }
        }

        public static ItemInfo CreateItem(string name, string description, uint initialPrice, uint buyNowPrice)
        {
            return new ItemInfo(name, description, initialPrice, buyNowPrice);
        }
        public static ItemInfo CreateItem()
        {
            return new ItemInfo("*", null, 1000, 1000);
        }
        private void OnPropertyChanged(string property_name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property_name));
            }
        }
        private ItemInfo(string name, string description, uint initialPrice, uint buyNowPrice)
        {
            _name = name;
            _description = description;
            _initial_price = initialPrice;
            _buy_now_price = buyNowPrice;
        }

        private string _name;
        private string _description;
        private uint _initial_price;
        private uint _buy_now_price;
    }
}
