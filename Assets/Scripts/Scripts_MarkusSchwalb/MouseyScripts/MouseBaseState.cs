using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MouseBaseState 
{
    public abstract void EnterMouseState(MouseStateManager Mouse);

    public abstract void UpdateMouseState(MouseStateManager Mouse);

    public abstract void ExitMouseState(MouseStateManager Mouse);

}
