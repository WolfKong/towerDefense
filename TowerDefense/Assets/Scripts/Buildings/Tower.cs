using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform bulletPrefab;

    protected List<GameObject> Targets = new List<GameObject>();

    private TowerData data;
    private List<GameObject> removeList = new List<GameObject>();
    private Blast blastPrefab;
    private float interval;
    private float damage;
    private int simultaneousTargets;

    private bool resting;
    private float time = 0;

    private void Start()
    {
        resting = true;
    }

    public void SetData(TowerData data)
    {
        this.data = data;
        interval = data.Interval;
        damage = data.Damage;
        simultaneousTargets = data.SimultaneousTargets;
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
        removeList = Targets.FindAll(t => !t);

        foreach (var target in removeList)
            Targets.Remove(target);

        removeList.Clear();
    }

    protected virtual List<GameObject> ValidTargets()
    {
        return Targets.FindAll(t => t);
    }

    private void TryToFire()
    {
        var validTargets = ValidTargets();
        var validCount = validTargets.Count;

        if (validCount == 0)
        {
            resting = true;
        }
        else
        {
            for (var i = 0; i < simultaneousTargets && i < validCount; i++)
                Fire(validTargets[i].transform);
        }
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
            Targets.Add(collider.gameObject);

            if (resting)
                Fire(collider.transform);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        var intruder = collider.gameObject;

        if (Targets.Contains(intruder) && intruder.tag == "Enemy")
            Targets.Remove(intruder);
    }
}
