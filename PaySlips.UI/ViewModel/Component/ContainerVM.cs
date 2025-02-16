using PaySlips.UI.Infastructure.Command;
using PaySlips.UI.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PaySlips.UI.ViewModel.Component
{
    internal class ContainerVM : BaseVM
    {
        #region ПОЛЯ

        #endregion

        #region СВОЙСТВА

        private string _title;
        public string Title { get => _title; set => Set(ref _title, value); }

        private string _text;
        public string Text { get => _text; set => Set(ref _title, value); }

        private IEnumerable<BaseVM> _items;
        public IEnumerable<BaseVM> Items { get => _items; set => Set(ref _items, value); }


        #endregion


        public ContainerVM(string title, IEnumerable<BaseVM> items)
        {
            Title = title;
            Items = items;
        }
    }

}
