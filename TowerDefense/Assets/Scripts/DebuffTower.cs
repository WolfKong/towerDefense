using System.Collections.Generic;
using UnityEngine;

public class DebuffTower : Tower
{
    protected override List<GameObject> ValidTargets()
    {
        return Targets.FindAll(t => t && !t.GetComponent<Enemy>().HasDebuff());
    }
}
