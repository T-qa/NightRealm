using UnityEngine;

public class AStarNode
{
    public readonly Vector2Int Cell;
    public int GCost;
    public int HCost;
    public AStarNode Parent;

    public int FCost => GCost + HCost;

    public AStarNode(Vector2Int cell)
    {
        Cell = cell;
    }
}
