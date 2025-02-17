using PaySlips.UI.ViewModel.Base;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace PaySlips.UI.ViewModel.Menu.Base
{
    internal class MenuVM : BaseVM
    {
        #region ПОЛЯ

        private readonly MainVM _mvvm;

        #endregion

        #region СВОЙСТВА

        public BitmapImage Source { get; } = new BitmapImage(new Uri(@"pack://application:,,,/Media/Img/STU.png"));

        private ObservableCollection<MenuItem> _menuItems;
        public ObservableCollection<MenuItem> MenuItems { get => _menuItems; set => Set(ref _menuItems, value); }

        private MenuItem _selectedMenuItem;
        public MenuItem SelectedMenuItem 
        { 
            get => _selectedMenuItem;
            set
            { 
                Set(ref _selectedMenuItem, value); 
                _mvvm.WorkspaceVM = value.VM;
            }
        }

        #endregion



        public MenuVM(MainVM mvvm)
        {
            _mvvm = mvvm;
            MenuItems = new ObservableCollection<MenuItem>
            {
                new MenuItem { Title = "Главная", VM = _mvvm.WorkspaceMainVM },
                new MenuItem { Title = "Расчетные\nлисты", VM = _mvvm.PayslipsVM },
                new MenuItem { Title = "Архив", VM = _mvvm.ArchiveVM},
                new MenuItem { Title = "Шаблоны", VM = _mvvm.TemplatesVM },
                new MenuItem { Title = "Справка", VM = _mvvm.ReferenceVM }
            };

        }
    }

}
