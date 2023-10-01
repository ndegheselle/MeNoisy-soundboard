using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeNoisySoundboard.App.Base.UI
{
    public interface IApp
    {
        public NavigationHandler Navigation { get; set; }
        public void SaveContext();
    }
}
