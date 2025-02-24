using Microsoft.VisualBasic;
using PaySlips.UI.Infastructure.Command;
using PaySlips.UI.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace PaySlips.UI.ViewModel.Component
{
    internal class SearchLineVM : BaseVM
    {
        #region ПОЛЯ

        private readonly MainVM _mvvm;
        private ObservableCollection<FileButtonArchiveVM> _originalButtons; // Оригинальная коллекция
        private ObservableCollection<FileButtonArchiveVM> _filteredButtons; // Отфильтрованная коллекция

        #endregion

        #region СВОЙСТВА

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                Filter(value);
                Set(ref _searchText, value);
            }
        }

        public ObservableCollection<FileButtonArchiveVM> FilteredButtons
        {
            get => _filteredButtons;
            set => Set(ref _filteredButtons, value);
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
        }

        #endregion

        public SearchLineVM(MainVM mvvm, ObservableCollection<FileButtonArchiveVM> buttons)
        {
            _mvvm = mvvm;
            _originalButtons = new ObservableCollection<FileButtonArchiveVM>(buttons); // Сохраняем оригинальную коллекцию
            _filteredButtons = buttons; // Изначально отфильтрованная коллекция равна оригинальной
        }

        public void Filter(string search)
        {
            // Очищаем отфильтрованную коллекцию
            FilteredButtons.Clear();

            // Если строка поиска пустая, добавляем все элементы из оригинальной коллекции
            if (string.IsNullOrEmpty(search))
            {
                foreach (var button in _originalButtons)
                {
                    FilteredButtons.Add(button);
                }
                return;
            }

            // Фильтруем элементы по строке поиска
            foreach (var button in _originalButtons)
            {
                if (button.FirstLabel.Contains(search, StringComparison.OrdinalIgnoreCase)) // Используем Contains для частичного совпадения
                {
                    FilteredButtons.Add(button);
                }
            }
        }
    }
}