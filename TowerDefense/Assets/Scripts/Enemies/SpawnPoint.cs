using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject dangerFX;

    public void SetDanger(bool danger)
    {
        dangerFX.SetActive(danger);
    }
}
