using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MeNoisy_soundboard.App.Base
{
    public interface IWindow
    {
        #region Navigation

        // XXX : Maybe pass the window there ?
        void Navigate(FrameworkElement page);
        void Push(FrameworkElement page);
        void Pop();
        #endregion
    }
}
