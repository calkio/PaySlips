using PaySlips.UI.ViewModel.Base;
using PaySlips.UI.ViewModel.Component;
using PaySlips.UI.ViewModel.Menu.Base;
using PaySlips.UI.ViewModel.Workspace.Archive;
using PaySlips.UI.ViewModel.Workspace.Main;
using PaySlips.UI.ViewModel.Workspace.Payslips;
using PaySlips.UI.ViewModel.Workspace.Reference;
using PaySlips.UI.ViewModel.Workspace.Templates;
using PaySlips.UI.ViewModel.Workspace.Trash;

namespace PaySlips.UI.ViewModel
{
    internal class MainVM : BaseVM
    {
        #region СВОЙСТВА

        private string _title;
        public string Title { get => _title; set => Set(ref _title, value); }

        #region VM

        private BaseVM _menuVM;
        public BaseVM MenuVM { get => _menuVM; set => Set(ref _menuVM, value); }

        private BaseVM _workspaceVM;
        public BaseVM WorkspaceVM { get => _workspaceVM; set => Set(ref _workspaceVM, value); }

        private BaseVM _titleBarVM;
        public BaseVM TitleBarVM { get => _titleBarVM; set => Set(ref _titleBarVM, value); }


        private BaseVM _archiveVM;
        public BaseVM ArchiveVM { get => _archiveVM; set => Set(ref _archiveVM, value); }

        private BaseVM _workspaceMainVM;
        public BaseVM WorkspaceMainVM { get => _workspaceMainVM; set => Set(ref _workspaceMainVM, value); }

        private BaseVM _payslipsVM;
        public BaseVM PayslipsVM { get => _payslipsVM; set => Set(ref _payslipsVM, value); }

        private BaseVM _referenceVM;
        public BaseVM ReferenceVM { get => _referenceVM; set => Set(ref _referenceVM, value); }

        private BaseVM _templatesVM;
        public BaseVM TemplatesVM { get => _templatesVM; set => Set(ref _templatesVM, value); }

        private BaseVM _trashVM;
        public BaseVM TrashVM { get => _trashVM; set => Set(ref _trashVM, value); }

        private TrashButtonVM _trashButtonVM;
        public TrashButtonVM TrashButtonVM { get => _trashButtonVM; set => Set(ref _trashButtonVM, value); }

        #endregion


        #endregion

        public MainVM()
        {
            TrashButtonVM = new TrashButtonVM(this);
            TitleBarVM = new TitleBarVM(this);
            ArchiveVM = new ArchiveVM(this);
            WorkspaceMainVM = new WorkspaceMainVM(this);
            PayslipsVM = new PayslipsVM(this);
            ReferenceVM = new ReferenceVM(this);
            TemplatesVM = new TemplatesVM(this);
            TrashVM = new TrashVM(this);
            WorkspaceVM = WorkspaceMainVM;
            MenuVM = new MenuVM(this);
        }
    }
}
