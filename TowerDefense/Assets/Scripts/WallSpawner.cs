using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [SerializeField] private Transform wallPrefab;


    private void Start()
    {
        MouseInput.Clicked += Spawn;
    }

    private void OnDestroy()
    {
        MouseInput.Clicked -= Spawn;
    }

    private void Spawn(Vector3 position)
    {
        var wall = Instantiate(wallPrefab, transform);
        var wallTransform = wall.transform;
        wallTransform.position = new Vector3(position.x, position.y + wallTransform.localScale.y * 0.5f, position.z);
    }
}
