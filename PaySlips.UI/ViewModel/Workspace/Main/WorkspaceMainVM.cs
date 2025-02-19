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

        private BaseVM _payslipsCreatorVM;
        public BaseVM PayslipsCreatorVM { get => _payslipsCreatorVM; set => Set(ref _payslipsCreatorVM, value); }

        private ContainerVM _lastPayslips;
        public ContainerVM LastPayslips { get => _lastPayslips; set => Set(ref _lastPayslips, value); }

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
            PayslipsCreatorVM = new PayslipsCreatorVM(mvvm);
            ObservableCollection<BaseVM> buttons = new ObservableCollection<BaseVM>();
            for (int i = 0; i < 150; i++)
            {
                buttons.Add(new FileButtonVM(i.ToString()));
            }
            LastPayslips = new ContainerVM("Последние РЛ", buttons);
            LastPayslips.Orientation = System.Windows.Controls.Orientation.Vertical;

            UnallocatedHours = new ContainerVM("Нераспределенные часы", new FileButtonVM("часы"));
        }
    }

}
