using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Win32;
using PaySlips.UI.Infastructure;
using PaySlips.UI.ViewModel.Base;

namespace PaySlips.UI.ViewModel
{
    internal class MainVM : BaseVM
    {
        #region СВОЙСТВА

        private BaseVM _content;
        public BaseVM Content { get => _content; set => Set(ref _content, value); }

        #endregion

        public MainVM()
        {
            ExampleVM exampleVM = new ExampleVM(this);
            _content = exampleVM;
        }
    }
}
