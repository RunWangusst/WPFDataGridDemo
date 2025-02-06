using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFDataGridDemo
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private bool? isAllSelected;

        public bool? IsAllSelected
        {
            get { return isAllSelected; }
            set
            {
                if (value != isAllSelected)
                {
                    isAllSelected = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsAllSelected)));
                }
            }
        }

        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<string> MySources { get; set; }
        public ICommand SelectAllCommand { get; }
        public ICommand CellValueChangedCommand { get; }

        public MainViewModel()
        {
            // 初始化分类数据
            Categories = new ObservableCollection<Category>
            {
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Clothing" },
                new Category { Id = 3, Name = "Home Appliances" }
            };

            // 初始化产品数据
            Products = new ObservableCollection<Product>
            {
                new Product { Name = "Laptop", Price = 999.99m, CategoryId = 1 },
                new Product { Name = "Shirt", Price = 29.99m, CategoryId = 2 },
                new Product { Name = "Washing Machine", Price = 499.99m, CategoryId = 3 }
            };
            foreach (var item in Products)
            {
                item.PropertyChanged += Item_PropertyChanged;
            }
            MySources = new ObservableCollection<string>()
            {
                "Laptop" ,
                "Shirt" ,
                "Washing Machine"
            };
            SelectAllCommand = new RelayCommand(SelectAll);
            CellValueChangedCommand = new RelayCommand(CellValueChanged);
            IsAllSelected = false;
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateHeaderCheckBoxState();
        }

        private void SelectAll(object obj)
        {
            if (obj is bool isSelected)
            {
                foreach (var item in Products)
                {
                    item.IsSelected = isSelected;
                }
            }
        }
        private void CellValueChanged(object obj)
        {
            UpdateHeaderCheckBoxState();
        }
        public void UpdateHeaderCheckBoxState()
        {
            var allChecked = Products.All(item => item.IsSelected == true);
            var allUnchecked = Products.All(item => item.IsSelected == false);

            IsAllSelected = allChecked ? true : (allUnchecked ? false : (bool?)null);
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }

}
