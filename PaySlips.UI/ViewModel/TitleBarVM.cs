using PaySlips.UI.ViewModel.Base;
using PaySlips.UI.ViewModel.Menu.Base;
using PaySlips.UI.ViewModel.Workspace.Archive;
using PaySlips.UI.ViewModel.Workspace.Main;
using PaySlips.UI.ViewModel.Workspace.Payslips;
using PaySlips.UI.ViewModel.Workspace.Reference;
using PaySlips.UI.ViewModel.Workspace.Templates;
using PaySlips.UI.ViewModel.Workspace.Trash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using PaySlips.UI.Infastructure.Command;
using System.Windows.Media.Imaging;

namespace PaySlips.UI.ViewModel
{
    internal class TitleBarVM : BaseVM
    {
        #region ПОЛЯ

        private readonly MainVM _mvvm;

        #endregion

        #region СВОЙСТВА

        private string _title;
        public string Title { get => _title; set => Set(ref _title, value); }

        public BitmapImage Minimize { get; } = new BitmapImage(
            new Uri(@"pack://application:,,,/Media/Img/Minimize.png"));
        public BitmapImage Expand { get; } = new BitmapImage(
            new Uri(@"pack://application:,,,/Media/Img/Expand.png"));
        public BitmapImage Close { get; } = new BitmapImage(
            new Uri(@"pack://application:,,,/Media/Img/Close.png"));

        #endregion

        #region КОМАНДЫ

        #region Свернуть

        public ICommand MinimizeCommand
        {
            get
            {
                return new RelayCommand(
                    (_) => MinimizeImpl(),
                    (_) => CanTry());
            }
        }
        private void MinimizeImpl()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        #endregion

        #region Развернуть

        public ICommand MaximizeCommand
        {
            get
            {
                return new RelayCommand(
                    (_) => MaximizeImpl(),
                    (_) => CanTry());
            }
        }
        private void MaximizeImpl()
        {
            var window = Application.Current.MainWindow;
            window.WindowState = window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        #endregion

        #region Закрыть

        public ICommand CloseCommand
        {
            get
            {
                return new RelayCommand(
                    (_) => CloseImpl(),
                    (_) => CanTry());
            }
        }
        private void CloseImpl()
        {
            Application.Current.MainWindow.Close();
        }

        #endregion

        private bool CanTry() => true;

        #endregion



        public TitleBarVM(MainVM mvvm)
        {
            Title = "Авторасчет";
            _mvvm = mvvm;
        }
    }
}