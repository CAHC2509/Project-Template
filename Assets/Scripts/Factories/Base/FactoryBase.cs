using UnityEngine;

public abstract class FactoryBase : MonoBehaviour
{
    public abstract void Initialize();
    public abstract void Conclude();
    public abstract IFactoryProduct GetProduct(Vector3 position, Quaternion rotation);
}
