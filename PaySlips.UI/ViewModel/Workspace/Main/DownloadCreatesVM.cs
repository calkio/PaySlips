using PaySlips.UI.Infastructure.Command;
using PaySlips.UI.ViewModel.Base;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PaySlips.UI.ViewModel.Workspace.Main
{
    class DownloadCreatesVM : BaseVM
    {
        #region ПОЛЯ

        private readonly MainVM _mvvm;

        #endregion

        #region СВОЙСТВА

        private BitmapImage _sourceButtonCalendar;
        public BitmapImage SourceButtonCalendar{ get => _sourceButtonCalendar; set => Set(ref _sourceButtonCalendar, value); }

        private BitmapImage _sourceButtonCharge;
        public BitmapImage SourceButtonCharge { get => _sourceButtonCharge; set => Set(ref _sourceButtonCharge, value); }

        private BitmapImage _sourceButton;
        public BitmapImage SourceButton { get => _sourceButton; set => Set(ref _sourceButton, value); }

        private BaseVM _creatorVM;
        public BaseVM CreatorVM { get => _creatorVM; set => Set(ref _creatorVM, value); }

        public BitmapImage Source { get; } = new BitmapImage(new Uri(@"pack://application:,,,/Media/Img/TopPanel.png"));

        #endregion

        #region Command

        #region DownloadCalendar

        public ICommand DownloadCalendar
        {
            get
            {
                return new RelayCommand(
                    (_) => DownloadCalendarImpl(),
                    (_) => CanDownloadCalendar());
            }
        }

        private bool CanDownloadCalendar()
        {
            // Логика для смены изображения
            if (true)
            {
                SourceButtonCalendar = new BitmapImage(new Uri(@"pack://application:,,,/Media/Img/OkButton.png"));
                return true;
            }
            else
            {
                SourceButtonCalendar = new BitmapImage(new Uri(@"pack://application:,,,/Media/Img/NotOkButton.png"));
                return false;
            }
        }

        private void DownloadCalendarImpl()
        {
        }

        #endregion

        #region DownloadСharge

        public ICommand DownloadСharge
        {
            get
            {
                return new RelayCommand(
                    (_) => DownloadСhargeImpl(),
                    (_) => CanDownloadСharge());
            }
        }

        private bool CanDownloadСharge()
        {
            // Логика для смены изображения
            if (true)
            {
                SourceButtonCalendar = new BitmapImage(new Uri(@"pack://application:,,,/Media/Img/OkButton.png"));
                return true;
            }
            else
            {
                SourceButtonCalendar = new BitmapImage(new Uri(@"pack://application:,,,/Media/Img/NotOkButton.png"));
                return false;
            }
        }

        private void DownloadСhargeImpl()
        {
        }

        #endregion

        #region DownloadScheduler

        public ICommand DownloadScheduler
        {
            get
            {
                return new RelayCommand(
                    (_) => DownloadSchedulerImpl(),
                    (_) => CanDownloadScheduler());
            }
        }

        private bool CanDownloadScheduler()
        {
            // Логика для смены изображения
            if (true)
            {
                SourceButtonCalendar = new BitmapImage(new Uri(@"pack://application:,,,/Media/Img/OkButton.png"));
                return true;
            }
            else
            {
                SourceButtonCalendar = new BitmapImage(new Uri(@"pack://application:,,,/Media/Img/NotOkButton.png"));
                return false;
            }
        }

        private void DownloadSchedulerImpl()
        {
        }

        #endregion

        #region DownloadHandbook

        public ICommand DownloadHandbook
        {
            get
            {
                return new RelayCommand(
                    (_) => DownloadHandbookImpl(),
                    (_) => CanDownloadHandbook());
            }
        }

        private bool CanDownloadHandbook()
        {
            // Логика для смены изображения
            if (false)
            {
                SourceButtonCalendar = new BitmapImage(new Uri(@"pack://application:,,,/Media/Img/OkButton.png"));
                return true;
            }
            else
            {
                SourceButtonCalendar = new BitmapImage(new Uri(@"pack://application:,,,/Media/Img/NotOkButton.png"));
                return false;
            }
        }

        private void DownloadHandbookImpl()
        {
        }

        #endregion


        #endregion


        public DownloadCreatesVM(MainVM mvvm)
        {
        }
    }

}
