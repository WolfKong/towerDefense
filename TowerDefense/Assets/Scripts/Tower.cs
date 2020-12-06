using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private TowerData towerData;

    private TowerData data;
    private List<GameObject> targets = new List<GameObject>();
    private List<GameObject> removeList = new List<GameObject>();
    private Blast blastPrefab;
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
        this.data = data;
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
                var targetTransform = targets[i].transform;
                var targetPosition = targetTransform.position + targetTransform.forward * 0.5f;
                var blast = Instantiate(blastPrefab, targetPosition, Quaternion.identity, transform);
                blast.TowerData = data;

                var bullet = Instantiate(bulletPrefab, transform);
                bullet.DOMove(targetPosition, 0.5f).OnComplete(() => Destroy(bullet.gameObject));

                break;
            }
            else
            {
                removeList.Add(targets[i]);
            }
        }

        // Remove Dead targets
        foreach (var target in removeList)
            targets.Remove(target);

        removeList.Clear();
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
