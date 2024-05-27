namespace Lib.Medium;

public class P973
{
    public int[][] KClosest(int[][] points, int k)
    {
        var distS = new List<PointWithDistance>();
        foreach (var x in points)
        {
            distS.Add(new PointWithDistance
            {
                Point = x,
                Distance = x[0] * x[0] + x[1] * x[1]
            });
        }

        distS.Sort((x, y) => x.Distance.CompareTo(y.Distance));

        return distS.Take(k).Select(x=>x.Point).ToArray();
    }

    class PointWithDistance
    {
        public int[] Point { get; set; }
        public int Distance {  get; set; }
    }
}
