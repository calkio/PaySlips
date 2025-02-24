using PaySlips.UI.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PaySlips.UI.ViewModel.Workspace.Main
{
    class ListVM : BaseVM
    {
        #region ПОЛЯ

        #endregion

        #region СВОЙСТВА

        private Orientation _orientation = Orientation.Horizontal;
        public Orientation Orientation { get => _orientation; set => Set(ref _orientation, value); }

        private string _title;
        public string Title { get => _title; set => Set(ref _title, value); }

        private string _text;
        public string Text { get => _text; set => Set(ref _title, value); }

        private IEnumerable<BaseVM> _items;
        public IEnumerable<BaseVM> Items { get => _items; set => Set(ref _items, value); }

        private BaseVM _headerVM;
        public BaseVM HeaderVM { get => _headerVM; set => Set(ref _headerVM, value); }

        #endregion


        public ListVM(string title, IEnumerable<BaseVM> items)
        {
            Title = title;
            Items = items;
        }

        public ListVM(string title, BaseVM item)
        {
            Title = title;
            Items = new List<BaseVM>() { item };
        }


        public ListVM(string title, IEnumerable<BaseVM> items, BaseVM searchLineVM)
        {
            Title = title;
            Items = items;
            HeaderVM = searchLineVM;
        }
    }

}
