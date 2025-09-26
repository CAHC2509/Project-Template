using UnityEngine;

public class FlatSFXFactory : FactoryBase
{
    [SerializeField] private ObjectPool flatSFXPool;

    public override void Initialize()
    {
        flatSFXPool.Initialize();
    }

    public override void Conclude()
    {
        flatSFXPool.Conclude();
    }

    public override IFactoryProduct GetProduct(Vector3 position, Quaternion rotation)
    {
        PooledObject pooledObject = flatSFXPool.GetPooledObject();
        pooledObject.transform.SetPositionAndRotation(position, rotation);
        IFactoryProduct product = pooledObject.GetComponent<IFactoryProduct>();
        product.Initialize();

        return product;
    }
}
