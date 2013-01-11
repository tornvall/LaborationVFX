using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaborationVFX.Components.Input
{
    public class UserAction
    {
        public delegate void ActionEvent(ActionType Type);

        public ActionType Type;

        public event ActionEvent ActionPerformed;

        public UserAction(ActionType type)
        {
            Type = type;
        }

        public void FireEvent()
        {
            ActionPerformed(Type);
        }
    }
}
