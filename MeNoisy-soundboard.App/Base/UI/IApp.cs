using MeNoisySoundboard.App.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeNoisySoundboard.App.Base.UI
{
    public interface IApp
    {
        public GlobalContext GlobalContext { get; }
        public NavigationHandler Navigation { get; }
        public void SaveContext();
    }
}
