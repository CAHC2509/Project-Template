using UnityEngine;

public abstract class PlayerComponentBase : MonoBehaviour
{
    protected bool enabledState;

    public bool EnabledState => enabledState;

    public abstract void Initialize();
    public abstract void Conclude();

    public void ChangeEnabledState(bool newState) => enabledState = newState;
}
