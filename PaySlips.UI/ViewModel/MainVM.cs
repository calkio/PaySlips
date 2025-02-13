using PaySlips.UI.ViewModel.Base;
using PaySlips.UI.ViewModel.Menu.Base;
using PaySlips.UI.ViewModel.Workspace.Base;

namespace PaySlips.UI.ViewModel
{
    internal class MainVM : BaseVM
    {
        #region СВОЙСТВА

        private BaseVM _menuVM;
        public BaseVM MenuVM { get => _menuVM; set => Set(ref _menuVM, value); }


        private BaseVM _workspaceVM;
        public BaseVM WorkspaceVM { get => _workspaceVM; set => Set(ref _workspaceVM, value); }

        #endregion

        public MainVM()
        {
            MenuVM = new MenuVM(this);
            WorkspaceVM = new WorkspaceVM(this);
        }
    }
}
