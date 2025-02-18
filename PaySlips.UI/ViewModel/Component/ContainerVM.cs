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

        private BaseVM _searchLineVM;
        public BaseVM SearchLineVM { get => _searchLineVM; set => Set(ref _searchLineVM, value); }

        private BaseVM _directoryVM;
        public BaseVM DirectoryVM { get => _directoryVM; set => Set(ref _directoryVM, value); }

        private BaseVM _titleButtonVM;
        public BaseVM TitleButtonVM { get => _titleButtonVM; set => Set(ref _titleButtonVM, value); }

        private BaseVM _dateButtonVM;
        public BaseVM DateButtonVM { get => _dateButtonVM; set => Set(ref _dateButtonVM, value); }

        private BaseVM _fileButtonArchiveVM;
        public BaseVM FileButtonArchiveVM { get => _fileButtonArchiveVM; set => Set(ref _fileButtonArchiveVM, value); }

        #endregion


        public ContainerVM(string title, IEnumerable<BaseVM> items)
        {
            Title = title;
            Items = items;
        }

        public ContainerVM(string title, IEnumerable<BaseVM> items, BaseVM directoryVM, BaseVM searchLineVM, BaseVM titleButtonVM, BaseVM dateButtonVM)
        {
            Title = title;
            Items = items;
            DirectoryVM = directoryVM;
            SearchLineVM = searchLineVM;
            TitleButtonVM = titleButtonVM;
            DateButtonVM = dateButtonVM;
        }
    }

}
