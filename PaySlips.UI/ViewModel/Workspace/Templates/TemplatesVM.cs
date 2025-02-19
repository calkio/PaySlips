using PaySlips.UI.Infastructure.Command;
using PaySlips.UI.ViewModel.Base;
using PaySlips.UI.ViewModel.Component;
using System.Windows.Input;

namespace PaySlips.UI.ViewModel.Workspace.Templates
{
    internal class TemplatesVM : BaseVM
    {
        #region ПОЛЯ

        private readonly MainVM _mvvm;

        #endregion

        #region СВОЙСТВА

        private string _firstLabel = "Hello world1";
        public string FirstLabel { get => _firstLabel; set => Set(ref _firstLabel, value); }

        private BaseVM _containerProductionCalendarVM;
        public BaseVM ContainerProductionCalendarVM { get => _containerProductionCalendarVM; set => Set(ref _containerProductionCalendarVM, value); }

        private BaseVM _containerLoadVM;
        public BaseVM ContainerLoadVM { get => _containerLoadVM; set => Set(ref _containerLoadVM, value); }

        private BaseVM _containerTeachersScheduleVM;
        public BaseVM ContainerTeachersScheduleVM { get => _containerTeachersScheduleVM; set => Set(ref _containerTeachersScheduleVM, value); }

        private BaseVM _containerTeacherHandbookVM;
        public BaseVM СontainerTeacherHandbookVM { get => _containerTeacherHandbookVM; set => Set(ref _containerTeacherHandbookVM, value); }

        private BaseVM _downloadButtonVM;
        public BaseVM DownloadButtonVM { get => _downloadButtonVM; set => Set(ref _downloadButtonVM, value); }

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


        public TemplatesVM(MainVM mvvm)
        {
            _mvvm = mvvm;
            ContainerProductionCalendarVM = new ContainerVM("Производственный календарь",new FileButtonVM("Название"));
            ContainerLoadVM = new ContainerVM("Производственный календарь", new FileButtonVM("Название"));
            ContainerTeachersScheduleVM = new ContainerVM("Производственный календарь", new FileButtonVM("Название"));
            СontainerTeacherHandbookVM = new ContainerVM("Производственный календарь", new FileButtonVM("Название"));
            DownloadButtonVM = new DownloadButtonVM(mvvm);
        }
    }

}