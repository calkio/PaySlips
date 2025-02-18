using PaySlips.UI.Infastructure.Command;
using PaySlips.UI.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PaySlips.UI.ViewModel.Component
{
    internal class ContainerVM : BaseVM
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

        private BaseVM _searchLineVM;
        public BaseVM SearchLineVM { get => _searchLineVM; set => Set(ref _searchLineVM, value); }

        #endregion


        public ContainerVM(string title, IEnumerable<BaseVM> items)
        {
            Title = title;
            Items = items;
        }

        public ContainerVM(string title, BaseVM item)
        {
            Title = title;
            Items = new List<BaseVM>() { item };
        }

        public ContainerVM(string title, IEnumerable<BaseVM> items, BaseVM searchLineVM)
        {
            Title = title;
            Items = items;
            SearchLineVM = searchLineVM;
        }
    }

}
