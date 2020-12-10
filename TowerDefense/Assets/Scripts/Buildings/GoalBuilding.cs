using UnityEngine;

public class GoalBuilding : MonoBehaviour
{
    [SerializeField] private FloatVariable health;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            health.Value--;
            Destroy(collider.gameObject);
        }
    }
}
