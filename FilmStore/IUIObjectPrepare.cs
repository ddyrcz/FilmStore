using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FilmStore
{
    interface IUIObjectPrepareWithParameter<T, W>
        where T : class
        where W : class
    {
        T Prepare(W obj);
    }
}
