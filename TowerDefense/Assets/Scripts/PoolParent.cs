using UnityEngine;

public class PoolParent : MonoBehaviour
{
    [SerializeField] private ObjectPool pool;

    private void Awake()
    {
        pool.CreatePool(transform);
    }
}
