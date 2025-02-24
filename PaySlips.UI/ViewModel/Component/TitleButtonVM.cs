using PaySlips.UI.Infastructure.Command;
using PaySlips.UI.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PaySlips.UI.ViewModel.Component
{
    //class TitleButtonVM : BaseVM
    //{
    //    #region ПОЛЯ

    //    private readonly MainVM _mvvm;

    //    #endregion

    //    #region СВОЙСТВА

    //    private string _firstLabel = "Hello world1";
    //    public string FirstLabel { get => _firstLabel; set => Set(ref _firstLabel, value); }

    //    #endregion

    //    #region КОМАНДЫ

    //    public ICommand ShowSecondUserControl
    //    {
    //        get
    //        {
    //            return new RelayCommand(
    //                (_) => ShowSecondUserControlImpl(),
    //                (_) => CanShowSecondUserControl());
    //        }
    //    }
    //    private bool CanShowSecondUserControl() => true;
    //    private void ShowSecondUserControlImpl()
    //    {
    //    }


    //    #endregion


    //    public TitleButtonVM(MainVM mvvm)
    //    {
    //        _mvvm = mvvm;
    //    }

    //}
    internal class TitleButtonVM : BaseVM
    {
        #region ПОЛЯ

        private readonly MainVM _mvvm;
        private ObservableCollection<FileButtonArchiveVM> _buttons;

        #endregion

        #region СВОЙСТВА

        private string _firstLabel = "Hello world1";
        public string FirstLabel
        {
            get => _firstLabel;
            set => Set(ref _firstLabel, value);
        }

        public ObservableCollection<FileButtonArchiveVM> Buttons
        {
            get => _buttons;
            set => Set(ref _buttons, value);
        }

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
            // Логика для показа второго пользовательского контроллера.
        }

        public ICommand SortButtonsCommand
        {
            get
            {
                return new RelayCommand(
                    (_) => SortButtons(),
                    (_) => CanSortButtons());
            }
        }

        private bool CanSortButtons() => Buttons != null && Buttons.Count > 0;

        private void SortButtons()
        {
            // Сортируем коллекцию кнопок по Title в алфавитном порядке
            var sortedButtons = Buttons.OrderBy(button => button.FirstLabel).ToList();

            // Очищаем и заполняем коллекцию отсортированными элементами
            Buttons.Clear();
            foreach (var button in sortedButtons)
            {
                Buttons.Add(button);
            }
        }

        #endregion

        public TitleButtonVM(MainVM mvvm)
        {
            _mvvm = mvvm;
        }
        public TitleButtonVM(MainVM mvvm, ObservableCollection<FileButtonArchiveVM> buttons)
        {
            _mvvm = mvvm;
            Buttons = buttons; // Инициализация коллекции
        }
    }
}
