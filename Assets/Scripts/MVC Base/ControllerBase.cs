using UnityEngine;

public class ControllerBase : MonoBehaviour, IControllerBase
{
    public virtual void Conclude()
    {
        AddListeners();
    }

    public virtual void Initialize()
    {
        RemoveListeners();
    }

    protected virtual void AddListeners() { }
    protected virtual void RemoveListeners() { }
}
