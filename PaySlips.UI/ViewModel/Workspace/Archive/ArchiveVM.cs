﻿using PaySlips.UI.Infastructure.Command;
using PaySlips.UI.ViewModel.Base;
using PaySlips.UI.ViewModel.Component;
using System.Windows.Input;

namespace PaySlips.UI.ViewModel.Workspace.Archive
{
    class ArchiveVM : BaseVM
    {
        #region ПОЛЯ

        private readonly MainVM _mvvm;

        #endregion

        #region СВОЙСТВА

        private BaseVM _search;
        public BaseVM Search { get => _search; set => Set(ref _search, value); }

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


        public ArchiveVM(MainVM mvvm)
        {
            _mvvm = mvvm;
            Search = new SearchLineVM(mvvm);


        }


    }

}
