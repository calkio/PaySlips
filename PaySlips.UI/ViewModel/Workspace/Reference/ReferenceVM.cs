﻿using PaySlips.UI.Infastructure.Command;
using PaySlips.UI.ViewModel.Base;
using PaySlips.UI.ViewModel.Component;
using System.Windows.Controls;
using System.Windows.Input;

namespace PaySlips.UI.ViewModel.Workspace.Reference
{
    internal class ReferenceVM : BaseVM
    {
        #region ПОЛЯ

        private readonly MainVM _mvvm;

        #endregion

        #region СВОЙСТВА

        private string _firstLabel = "Hello world1";
        public string FirstLabel { get => _firstLabel; set => Set(ref _firstLabel, value); }

        private BaseVM _refContainerVM;
        public BaseVM RefContainerVM { get => _refContainerVM; set => Set(ref _refContainerVM, value); }

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


        public ReferenceVM(MainVM mvvm)
        {
            _mvvm = mvvm;
            RefContainerVM = new RefContainerVM(_mvvm);
        }
    }
}