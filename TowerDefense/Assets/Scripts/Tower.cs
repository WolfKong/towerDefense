using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private TowerData towerData;

    private List<GameObject> targets = new List<GameObject>();
    private GameObject blastPrefab;
    private float interval;
    private float damage;

    private float time = 0;

    private void Start()
    {
        SetData(towerData);
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time > interval)
        {
            time = 0;
            Fire();
        }
    }

    public void SetData(TowerData data)
    {
        interval = data.Interval;
        damage = data.Damage;
        blastPrefab = data.BlastPrefab;
    }

    private void Fire()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i])
            {
                var targetPosition = targets[i].transform.position;
                Debug.LogWarning($"PV-FIRE At {targets[i]}, pos {targetPosition}");
                var blast = Instantiate(blastPrefab, targetPosition, Quaternion.identity, transform);

                var bullet = Instantiate(bulletPrefab, transform);
                bullet.DOMove(targetPosition, 0.5f).OnComplete(() => Destroy(bullet.gameObject));

                break;
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
            targets.Add(collider.gameObject);
    }

    private void OnTriggerExit(Collider collider)
    {
        var intruder = collider.gameObject;
        if (targets.Contains(intruder) && intruder.tag == "Enemy")
            targets.Remove(intruder);
    }
}
