using UnityEngine;

public class SimplePooledObject : MonoBehaviour
{
    private SimpleObjectPool pool;
    public SimpleObjectPool Pool { get => pool; set => pool = value; }


    public void Release()
    {
        pool.ReturnToPool(this);
    }
}
