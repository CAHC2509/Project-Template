using UnityEngine;

public abstract class ControllerBase : MonoBehaviour
{
    public abstract void Initialize();
    public abstract void Conclude();

    protected virtual void AddListeners() { }
    protected virtual void RemoveListeners() { }
}
