using PaySlips.UI.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySlips.UI.ViewModel.Menu
{
    class MenuItem : BaseVM
    {
        public string Title { get; set; }

        private BaseVM _vm;
        public BaseVM VM { get => _vm; set => Set(ref _vm, value); }

    }
}
