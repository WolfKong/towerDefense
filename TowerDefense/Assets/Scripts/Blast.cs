using System.Collections;
using UnityEngine;

public class Blast : MonoBehaviour
{
    [SerializeField] private new Collider collider;
    [SerializeField] private bool areaDamage;

    public TowerData TowerData { get; set; }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            var enemy = collider.gameObject.GetComponent<Enemy>();
            enemy.ApplyBlast(TowerData);

            if (!areaDamage)
                this.collider.enabled = false;
        }
    }
}
