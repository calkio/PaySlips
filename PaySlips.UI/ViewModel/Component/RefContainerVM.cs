using PaySlips.UI.Infastructure.Command;
using PaySlips.UI.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PaySlips.UI.ViewModel.Component
{
    internal class RefContainerVM : BaseVM
    {
        #region ПОЛЯ

        private readonly MainVM _mvvm;

        #endregion

        #region СВОЙСТВА

        private string _labelInfo = "О приложении";
        public string LabelInfo { get => _labelInfo; set => Set(ref _labelInfo, value); }

        private string _programInfo = "\tРазработка программного обеспечения «Авторасчет кафедры ИТТ» выполняется студентами группы БПИ-411 факультета Бизнес-информатики Сибирского государственного университета путей сообщения (СГУПС).\r\n\tМы являемся командой энтузиастов, специализирующихся на разработке программного обеспечения, анализе данных и автоматизации процессов. В рамках данного проекта мы ставим перед собой цель создать удобный и функциональный инструмент для упрощения расчетов и повышения эффективности работы кафедры.\r\n\tНаша команда:\r\n\t* Разработчики:\r\n\tМы активно используем современные технологии программирования (C#, SQL, JSON), а также занимаемся проектированием пользовательских интерфейсов и баз данных.\r\n\t* Ответственность за проект:\r\n\tМы разрабатываем весь цикл системы — от анализа требований и проектирования архитектуры до тестирования и внедрения программного продукта.\r\n";
        public string ProgramInfo { get => _programInfo; set => Set(ref _programInfo, value); }

        

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


        public RefContainerVM(MainVM mvvm)
        {
            _mvvm = mvvm;
        }
    }
}
