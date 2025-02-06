using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDataGridDemo
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Product:INotifyPropertyChanged
    {
        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set {
                if (value != IsSelected)
                {
                    isSelected = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected)));
                }
            }
        }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
