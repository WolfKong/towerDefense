using System.Collections.Generic;
using System.Linq;
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

    private bool resting;
    private float time = 0;

    private void Start()
    {
        resting = true;
        SetData(towerData);
    }

    public void SetData(TowerData data)
    {
        this.data = data;
        interval = data.Interval;
        damage = data.Damage;
        blastPrefab = data.BlastPrefab;
    }

    private void Update()
    {
        if (resting) return;

        time += Time.deltaTime;

        if (time > interval)
        {
            time = 0;
            RemoveDeadTargets();
            TryToFire();
        }
    }

    private void RemoveDeadTargets()
    {
        removeList = targets.FindAll(t => !t);

        foreach (var target in removeList)
            targets.Remove(target);

        removeList.Clear();
    }

    private void TryToFire()
    {
        var target = targets.FirstOrDefault(t => t);

        if (target)
            Fire(target.transform);
        else
            resting = true;
    }

    private void Fire(Transform targetTransform)
    {
        resting = false;

        var targetPosition = targetTransform.position + targetTransform.forward * 0.5f;

        var blast = Instantiate(blastPrefab);
        blast.transform.position = targetPosition;
        blast.TowerData = data;

        var bullet = Instantiate(bulletPrefab, transform);
        bullet.DOMove(targetPosition, 0.3f).OnComplete(() => Destroy(bullet.gameObject));
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            targets.Add(collider.gameObject);

            if (resting)
                Fire(collider.transform);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        var intruder = collider.gameObject;

        if (targets.Contains(intruder) && intruder.tag == "Enemy")
            targets.Remove(intruder);
    }
}
