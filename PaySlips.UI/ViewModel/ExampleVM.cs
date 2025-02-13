using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PaySlips.UI.Infastructure;
using PaySlips.UI.Infastructure.Command;
using PaySlips.UI.ViewModel.Base;

namespace PaySlips.UI.ViewModel
{
    internal class ExampleVM : BaseVM
    {
        #region ПОЛЯ

        private readonly MainVM _mvvm;

        #endregion

        #region СВОЙСТВА

        private string _firstLabel = "Hello world1";
        public string FirstLabel { get => _firstLabel; set => Set(ref _firstLabel, value); }

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
            _mvvm.Content = new ExampleVM2(_mvvm);
        }


        #endregion


        public ExampleVM(MainVM mvvm)
        {
            _mvvm = mvvm;
        }
    }
}
