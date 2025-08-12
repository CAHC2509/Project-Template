using UnityEngine;

public abstract class ControllerBase : MonoBehaviour
{
    public virtual void Initialize() => AddListeners();
    public virtual void Conclude() => RemoveListeners();

    protected virtual void AddListeners() { }
    protected virtual void RemoveListeners() { }
}
