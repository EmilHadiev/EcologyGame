using System;

public class PointsContainer : IPointsContainer
{
    public int Points { get; private set; }

    public event Action<int> PointsChanged;

    public void AddPoints(int point)
    {
        ++Points;
        PointsChanged?.Invoke(Points);
    }
    public void Reset()
    {
        Points = 0;
        PointsChanged?.Invoke(Points);
    }
}