using PaySlips.UI.Infastructure.Command;
using PaySlips.UI.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PaySlips.UI.ViewModel.Component
{
    class FileButtonArchiveVM : BaseVM
    {
        #region ПОЛЯ

        private readonly MainVM _mvvm;
        private readonly string _path = @"C:\Users\maksi\Programming\Ulanov\Test";

        #endregion

        #region СВОЙСТВА

        private string _firstLabel = "24/25_2 Семестр";
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
        }

        public ICommand OpenFolderCommand
        {
            get
            {
                return new RelayCommand(
                    (_) => OpenFolder());
            }
        }



        #endregion

        // Метод для открытия папки
        private void OpenFolder()
        {
            if (Directory.Exists(_path + "\\" + FirstLabel))
            {
                Process.Start("explorer.exe", _path + "\\"+ FirstLabel);
            }

        }

        public FileButtonArchiveVM(MainVM mvvm)
        {
            _mvvm = mvvm;
        }

        public FileButtonArchiveVM(MainVM mvvm, string firstLabel)
        {
            _mvvm = mvvm;
            FirstLabel = firstLabel;
        }
    }
}
