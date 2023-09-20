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
        void Navigate<TPage>(object context, object? parameters = null, TPage? page = null) where TPage : BasePage, new();
        void Push<TPage>(object context, object? parameters = null, TPage? page = null) where TPage : BasePage, new();
        void Pop();
        #endregion
    }
}
