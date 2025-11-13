using System;

public interface IPointsContainer
{
    int Points { get; }

    event Action<int> PointsChanged;

    void AddPoints(int point = 1);
    void Reset();
}