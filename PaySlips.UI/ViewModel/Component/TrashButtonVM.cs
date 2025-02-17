using PaySlips.UI.Infastructure.Command;
using PaySlips.UI.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PaySlips.UI.ViewModel.Component
{
    class TrashButtonVM : BaseVM
    {
        #region ПОЛЯ

        private readonly MainVM _mvvm;

        #endregion

        #region СВОЙСТВА

        public BitmapImage Source { get; } = new BitmapImage(new Uri(@"pack://application:,,,/Media/Img/Trash.png"));
        public BitmapImage SourceClick { get; } = new BitmapImage(new Uri(@"pack://application:,,,/Media/Img/TrashClick.png"));
        public BitmapImage SourceFocus { get; } = new BitmapImage(new Uri(@"pack://application:,,,/Media/Img/TrashFocus.png"));

        #endregion

        #region КОМАНДЫ

        public ICommand ShowTrash
        {
            get
            {
                return new RelayCommand(
                    (_) => ShowTrashImpl(),
                    (_) => CanShowTrash());
            }
        }
        private bool CanShowTrash()
        {
            if (_mvvm.WorkspaceVM == _mvvm.TrashVM) return false;
            else return true;
        }
        private void ShowTrashImpl()
        {
            _mvvm.WorkspaceVM = _mvvm.TrashVM;
        }


        #endregion


        public TrashButtonVM(MainVM mvvm)
        {
            _mvvm = mvvm;
        }
    }

}
