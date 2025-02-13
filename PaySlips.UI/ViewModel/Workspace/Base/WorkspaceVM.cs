using PaySlips.UI.ViewModel.Base;

namespace PaySlips.UI.ViewModel.Workspace.Base
{
    internal class WorkspaceVM : BaseVM
    {
        #region ПОЛЯ

        private readonly MainVM _mvvm;

        #endregion

        #region СВОЙСТВА

        private string _firstLabel = "Hello world2";
        public string FirstLabel { get => _firstLabel; set => Set(ref _firstLabel, value); }

        #endregion

        #region КОМАНДЫ

        #endregion


        public WorkspaceVM(MainVM mvvm)
        {
            _mvvm = mvvm;
        }
    }
}
