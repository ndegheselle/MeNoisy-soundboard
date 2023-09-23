using MeNoisySoundboard.App.Logic.Sounds.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeNoisySoundboard.App.Logic
{
    public class GlobalContext
    {
        public SoundsContext SoundsContext { get; set; } = new SoundsContext();
    }
}
