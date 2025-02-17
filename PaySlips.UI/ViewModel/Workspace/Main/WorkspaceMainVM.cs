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

        private BaseVM _lastPayslips;
        public BaseVM LastPayslips { get => _lastPayslips; set => Set(ref _lastPayslips, value); }

        private BaseVM _unallocatedHours;
        public BaseVM UnallocatedHours { get => _unallocatedHours; set => Set(ref _unallocatedHours, value); }

        private BaseVM _trash;
        public BaseVM Trash { get => _trash; set => Set(ref _trash, value); }

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
            Trash = _mvvm.TrashButtonVM;
            ObservableCollection<BaseVM> buttons = new ObservableCollection<BaseVM>();
            for (int i = 0; i < 150; i++)
            {
                buttons.Add(new FileButtonVM(_mvvm));
            }
            LastPayslips = new ContainerVM("Последние РЛ", buttons);

            ObservableCollection<BaseVM> hours = new ObservableCollection<BaseVM>();
            hours.Add(new FileButtonVM(_mvvm));
            UnallocatedHours = new ContainerVM("Нераспределенные часы", hours);
        }
    }

}
