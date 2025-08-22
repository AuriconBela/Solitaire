using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.GameFlow;

internal abstract class State
{
    internal abstract bool CanClick(Point point);
}

internal class NormalState : State
{
    internal override bool CanClick(Point point)
    {
        throw new NotImplementedException();
    }
}

internal class SelectedState : State
{
    internal override bool CanClick(Point point)
    {
        throw new NotImplementedException();
    }
}