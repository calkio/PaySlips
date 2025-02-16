using PaySlips.UI.Infastructure.Command;
using PaySlips.UI.ViewModel.Base;
using PaySlips.UI.ViewModel.Component;
using System.Windows.Input;

namespace PaySlips.UI.ViewModel.Menu.Base
{
    internal class MenuVM : BaseVM
    {
        #region ПОЛЯ

        private readonly MainVM _mvvm;

        #endregion

        #region СВОЙСТВА

        private string _firstLabel = "Hello world1";
        public string FirstLabel { get => _firstLabel; set => Set(ref _firstLabel, value); }

        #endregion

        #region КОМАНДЫ

        #region Показать главное окно

        public ICommand ShowMainView
        {
            get
            {
                return new RelayCommand(
                    (_) => ShowMainViewImpl(),
                    (_) => CanShow());
            }
        }
        private void ShowMainViewImpl()
        {
            _mvvm.WorkspaceVM = _mvvm.WorkspaceMainVM;
        }

        #endregion

        #region Показать окно расчетных листов

        public ICommand ShowPayslipsView
        {
            get
            {
                return new RelayCommand(
                    (_) => ShowPayslipsViewImpl(),
                    (_) => CanShow());
            }
        }
        private void ShowPayslipsViewImpl()
        {
            _mvvm.WorkspaceVM = _mvvm.PayslipsVM;
        }

        #endregion

        #region Показать окно архивов

        public ICommand ShowArchiveView
        {
            get
            {
                return new RelayCommand(
                    (_) => ShowArchiveViewImpl(),
                    (_) => CanShow());
            }
        }
        private void ShowArchiveViewImpl()
        {
            _mvvm.WorkspaceVM = _mvvm.ArchiveVM;
        }

        #endregion

        #region Показать окно шаблонов

        public ICommand ShowTemplatesView
        {
            get
            {
                return new RelayCommand(
                    (_) => ShowTemplatesViewImpl(),
                    (_) => CanShow());
            }
        }
        private void ShowTemplatesViewImpl()
        {
            _mvvm.WorkspaceVM = _mvvm.TemplatesVM;
        }

        #endregion

        #region Показать окно справки

        public ICommand ShowReferenceView
        {
            get
            {
                return new RelayCommand(
                    (_) => ShowReferenceViewImpl(),
                    (_) => CanShow());
            }
        }
        private void ShowReferenceViewImpl()
        {
            _mvvm.WorkspaceVM = _mvvm.ReferenceVM;
        }

        #endregion

        private bool CanShow() => true;

        #endregion


        public MenuVM(MainVM mvvm)
        {
            _mvvm = mvvm;
        }
    }

}
