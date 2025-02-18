using PaySlips.UI.ViewModel.Base;
using System.Windows.Media.Imaging;

namespace PaySlips.UI.ViewModel.Workspace.Main
{
    class CreatorVM : BaseVM
    {
        #region ПОЛЯ

        private readonly MainVM _mvvm;

        #endregion

        #region СВОЙСТВА

        private BaseVM _downloadCreatesVM;
        public BaseVM DownloadCreatesVM { get => _downloadCreatesVM; set => Set(ref _downloadCreatesVM, value); }

        public BitmapImage Source { get; } = new BitmapImage(new Uri(@"pack://application:,,,/Media/Img/BotPanel.png"));

        #endregion


        public CreatorVM(MainVM mvvm)
        {
        }
    }

}
