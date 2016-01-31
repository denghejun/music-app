using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UsingMVVMLightDemo
{

    public class MainWindowViewModel : ViewModelBase
    {
        public ICommand MouseDownCommand
        {
            get
            {
                return new RelayCommand<object>(e =>
                {

                });
            }
        }
    }
}
