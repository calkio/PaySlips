﻿using PaySlips.UI.Infastructure.Command;
using PaySlips.UI.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PaySlips.UI.ViewModel.Component
{
    internal class SearchLineVM : BaseVM
    {
        #region ПОЛЯ

        private readonly MainVM _mvvm;

        #endregion

        #region СВОЙСТВА

        private string _searchLabel = "Hello world1";
        public string SearchLabel { get => _searchLabel; set => Set(ref _searchLabel, value); }

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


        public SearchLineVM(MainVM mvvm)
        {
            _mvvm = mvvm;
        }
    }
}
