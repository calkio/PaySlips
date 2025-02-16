using PaySlips.UI.Infastructure.Command;
using PaySlips.UI.ViewModel.Base;
using PaySlips.UI.ViewModel.Component;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PaySlips.UI.ViewModel.Workspace.Main
{
    class WorkspaceMainVM : BaseVM
    {
        #region ПОЛЯ

        private readonly MainVM _mvvm;

        #endregion

        #region СВОЙСТВА

        private string _firstLabel = "Hello world1";
        public string FirstLabel { get => _firstLabel; set => Set(ref _firstLabel, value); }

        private BaseVM _container;
        public BaseVM ContainerVM { get => _container; set => Set(ref _container, value); }

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


        public WorkspaceMainVM(MainVM mvvm)
        {
            _mvvm = mvvm;
            ObservableCollection<BaseVM> buttons = new ObservableCollection<BaseVM>();
            for (int i = 0; i < 50; i++)
            {
                buttons.Add(new FileButtonVM(_mvvm));
            }
            ContainerVM = new ContainerVM("Кнопки", buttons);
        }
    }

}
