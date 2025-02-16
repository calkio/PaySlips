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

        private ObservableCollection<BaseVM> _container;
        public ObservableCollection<BaseVM> Container { get => _container; set => Set(ref _container, value); }

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
            Container = new ObservableCollection<BaseVM>();
            for (int i = 0; i < 50; i++)
            {
                Container.Add(new FileButtonVM(_mvvm));
            }
        }
    }

}
