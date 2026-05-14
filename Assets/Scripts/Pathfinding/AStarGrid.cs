using UnityEngine;

public class AStarGrid
{
    private readonly Vector2 origin;
    private readonly LayerMask obstacleMask;
    private readonly float cellSize;
    private readonly float obstacleCheckRadius;
    private readonly int maxSearchDistance;

    public AStarGrid(
        Vector2 origin,
        float cellSize,
        float obstacleCheckRadius,
        int maxSearchDistance,
        LayerMask obstacleMask
    )
    {
        this.origin = origin;
        this.cellSize = Mathf.Max(0.01f, cellSize);
        this.obstacleCheckRadius = Mathf.Max(0.01f, obstacleCheckRadius);
        this.maxSearchDistance = Mathf.Max(1, maxSearchDistance);
        this.obstacleMask = obstacleMask;
    }

    public Vector2Int WorldToCell(Vector2 worldPosition)
    {
        var relativePosition = worldPosition - origin;
        return new Vector2Int(
            Mathf.RoundToInt(relativePosition.x / cellSize),
            Mathf.RoundToInt(relativePosition.y / cellSize)
        );
    }

    public Vector2 CellToWorld(Vector2Int cell)
    {
        return origin + new Vector2(cell.x * cellSize, cell.y * cellSize);
    }

    public bool IsInSearchBounds(Vector2Int cell)
    {
        return Mathf.Abs(cell.x) <= maxSearchDistance && Mathf.Abs(cell.y) <= maxSearchDistance;
    }

    public bool IsWalkable(Vector2Int cell)
    {
        if (!IsInSearchBounds(cell))
            return false;

        return !Physics2D.OverlapCircle(CellToWorld(cell), obstacleCheckRadius, obstacleMask);
    }

    public bool HasLineOfSight(Vector2 from, Vector2 to)
    {
        var direction = to - from;
        var distance = direction.magnitude;
        if (distance <= Mathf.Epsilon)
            return true;

        return !Physics2D.CircleCast(
            from,
            obstacleCheckRadius,
            direction.normalized,
            distance,
            obstacleMask
        );
    }
}
