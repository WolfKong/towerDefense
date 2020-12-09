public class BuildingList : DataList<BuildingButton, BuildingData>
{
    protected override int CompareData(BuildingData a, BuildingData b)
    {
        return a.Cost.CompareTo(b.Cost);
    }
}
