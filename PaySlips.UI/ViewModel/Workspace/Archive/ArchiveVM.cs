using PaySlips.UI.Infastructure.Command;
using PaySlips.UI.ViewModel.Base;
using PaySlips.UI.ViewModel.Component;
using System.Windows.Input;

namespace PaySlips.UI.ViewModel.Workspace.Archive
{
    class ArchiveVM : BaseVM
    {
        #region ПОЛЯ

        private readonly MainVM _mvvm;

        #endregion

        #region СВОЙСТВА

        private BaseVM _search;
        public BaseVM Search { get => _search; set => Set(ref _search, value); }

        private BaseVM _directoryVM;
        public BaseVM DirectoryVM { get => _directoryVM; set => Set(ref _directoryVM, value); }

        private BaseVM _titleButtonVM;
        public BaseVM TitleButtonVM { get => _titleButtonVM; set => Set(ref _titleButtonVM, value); }

        private BaseVM _dateButtonVM;
        public BaseVM DateButtonVM { get => _dateButtonVM; set => Set(ref _dateButtonVM, value); }

        private BaseVM _fileButtonArchiveVM;
        public BaseVM FileButtonArchiveVM { get => _fileButtonArchiveVM; set => Set(ref _fileButtonArchiveVM, value); }




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


        public ArchiveVM(MainVM mvvm)
        {
            _mvvm = mvvm;
            DirectoryVM = new DirectoryVM(mvvm);
            Search = new SearchLineVM(mvvm);
            TitleButtonVM = new TitleButtonVM(mvvm);
            DateButtonVM = new DateButtonVM(mvvm);
            FileButtonArchiveVM = new FileButtonArchiveVM(mvvm);



        }


    }

}
