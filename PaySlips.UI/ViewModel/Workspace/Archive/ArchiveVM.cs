using PaySlips.UI.Infastructure.Command;
using PaySlips.UI.ViewModel.Base;
using PaySlips.UI.ViewModel.Component;
using System.Collections.ObjectModel;
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

        private BaseVM _deleteButtonVM;
        public BaseVM DeleteButtonVM { get => _deleteButtonVM; set => Set(ref _deleteButtonVM, value); }

        private BaseVM _deleteAllButtonVM;
        public BaseVM DeleteAllButtonVM { get => _deleteAllButtonVM; set => Set(ref _deleteAllButtonVM, value); }

        private BaseVM _downloadButtonVM;
        public BaseVM DownloadButtonVM { get => _downloadButtonVM; set => Set(ref _downloadButtonVM, value); }
        
        private BaseVM _recoverButtonVM;
        public BaseVM RecoverButtonVM { get => _recoverButtonVM; set => Set(ref _recoverButtonVM, value); }

        private BaseVM _containerVM;
        public BaseVM ContainerVM { get => _containerVM; set => Set(ref _containerVM, value); }

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


        public ArchiveVM(MainVM mvvm)
        {
            ObservableCollection<BaseVM> buttons = new ObservableCollection<BaseVM>();
            for (int i = 0; i < 150; i++)
            {
                buttons.Add(new FileButtonArchiveVM(_mvvm));
            }
            _mvvm = mvvm;
            DirectoryVM = new DirectoryVM(mvvm);
            Search = new SearchLineVM(mvvm);
            TitleButtonVM = new TitleButtonVM(mvvm);
            DateButtonVM = new DateButtonVM(mvvm);
            FileButtonArchiveVM = new FileButtonArchiveVM(mvvm);
            DeleteButtonVM = new DeleteButtonVM(mvvm);
            DeleteAllButtonVM = new DeleteAllButtonVM(mvvm);
            DownloadButtonVM = new DownloadButtonVM(mvvm);
            RecoverButtonVM = new RecoverButtonVM(mvvm);

            ContainerVM = new ContainerVM("Последние РЛ", buttons, Search );



        }


    }

}
