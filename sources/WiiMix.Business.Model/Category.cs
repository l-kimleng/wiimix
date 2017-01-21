using Microsoft.Practices.Prism.Mvvm;
using System.Collections.Generic;

namespace WiiMix.Business.Model
{
    public class Category : BindableBase
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public virtual ICollection<Product> Products { get; set; }

        public Category Clone()
        {
            return new Category
            {
                Id = Id,
                Name = Name
            };
        }
    }
}