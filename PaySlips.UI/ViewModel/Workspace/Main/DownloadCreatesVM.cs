using PaySlips.UI.ViewModel.Base;
using System.Windows.Media.Imaging;

namespace PaySlips.UI.ViewModel.Workspace.Main
{
    class DownloadCreatesVM : BaseVM
    {
        #region ПОЛЯ

        private readonly MainVM _mvvm;

        #endregion

        #region СВОЙСТВА

        private BaseVM _creatorVM;
        public BaseVM CreatorVM { get => _creatorVM; set => Set(ref _creatorVM, value); }

        public BitmapImage Source { get; } = new BitmapImage(new Uri(@"pack://application:,,,/Media/Img/TopPanel.png"));

        #endregion


        public DownloadCreatesVM(MainVM mvvm)
        {
        }
    }

}
