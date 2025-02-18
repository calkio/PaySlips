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
    class UpPanelVM : BaseVM
    {
        #region ПОЛЯ

        private readonly MainVM _mvvm;

        #endregion

        #region СВОЙСТВА

        private string _firstLabel = "Hello world1";
        public string FirstLabel { get => _firstLabel; set => Set(ref _firstLabel, value); }

        private BaseVM _search;
        public BaseVM Search { get => _search; set => Set(ref _search, value); }

        private BaseVM _directoryVM;
        public BaseVM DirectoryVM { get => _directoryVM; set => Set(ref _directoryVM, value); }

        private BaseVM _titleButtonVM;
        public BaseVM TitleButtonVM { get => _titleButtonVM; set => Set(ref _titleButtonVM, value); }

        private BaseVM _dateButtonVM;
        public BaseVM DateButtonVM { get => _dateButtonVM; set => Set(ref _dateButtonVM, value); }

        #endregion

        #region КОМАНДЫ

        public ICommand ShowSecondUserControl
        {
            get
            {
                return new RelayCommand(
                    (_) => ShowSecondUserControlImpl(),
                    (_) => CanShowSecondUserControl());
            }
        }
        private bool CanShowSecondUserControl() => true;
        private void ShowSecondUserControlImpl()
        {
        }


        #endregion


        public UpPanelVM(MainVM mvvm)
        {
            _mvvm = mvvm;
        }
        public UpPanelVM(MainVM mvvm, BaseVM search, BaseVM directoryVM, BaseVM titleButtonVM, BaseVM dateButtonVM)
        {
            _mvvm = mvvm;
            Search = search;
            DirectoryVM = directoryVM;
            TitleButtonVM = titleButtonVM;
            DateButtonVM = dateButtonVM;
        }
    }
}
