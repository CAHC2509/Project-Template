using UnityEngine;

public class ControllerBase : MonoBehaviour, IControllerBase
{
    public virtual void Initialize()
    {
        AddListeners();
    }

    public virtual void Conclude()
    {
        RemoveListeners();
    }

    protected virtual void AddListeners() { }
    protected virtual void RemoveListeners() { }
}
