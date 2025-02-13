using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PaySlips.UI.Infastructure;
using PaySlips.UI.ViewModel.Base;

namespace PaySlips.UI.ViewModel
{
    internal class ExampleVM2 : BaseVM
    {
        #region ПОЛЯ

        private readonly MainVM _mvvm;

        #endregion

        #region СВОЙСТВА

        private string _firstLabel = "Hello world2";
        public string FirstLabel { get => _firstLabel; set => Set(ref _firstLabel, value); }

        #endregion

        #region КОМАНДЫ

        #endregion


        public ExampleVM2(MainVM mvvm)
        {
            _mvvm = mvvm;
        }
    }
}
