public class TowerList : DataList<BuildingButton, TowerData>
{
    protected override int CompareData(TowerData a, TowerData b)
    {
        return a.Damage.CompareTo(b.Damage);
    }
}
