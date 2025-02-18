using PaySlips.UI.ViewModel.Base;

namespace PaySlips.UI.ViewModel.Workspace.Main
{
    class PayslipsCreatorVM : BaseVM
    {
        #region ПОЛЯ

        private readonly MainVM _mvvm;

        #endregion

        #region СВОЙСТВА

        private BaseVM _downloadCreatesVM;
        public BaseVM DownloadCreatesVM { get => _downloadCreatesVM; set => Set(ref _downloadCreatesVM, value); }

        private BaseVM _creatorVM;
        public BaseVM CreatorVM { get => _creatorVM; set => Set(ref _creatorVM, value); }

        #endregion


        public PayslipsCreatorVM(MainVM mvvm)
        {
            _mvvm = mvvm;
            DownloadCreatesVM = new DownloadCreatesVM(mvvm);
            CreatorVM = new CreatorVM(mvvm);
        }
    }

}
