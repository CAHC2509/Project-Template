using UnityEngine;

public abstract class FactoryBase : MonoBehaviour
{
    public virtual void Initialize() { }
    public virtual void Conclude() { }
    public abstract IFactoryProduct GetProduct(Vector3 position, Quaternion rotation);
}
