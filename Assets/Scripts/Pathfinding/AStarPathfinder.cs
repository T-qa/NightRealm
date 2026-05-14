using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinder
{
    private const int STRAIGHT_COST = 10;
    private const int DIAGONAL_COST = 14;

    private static readonly Vector2Int[] CardinalDirections =
    {
        new Vector2Int(0, 1),
        new Vector2Int(1, 0),
        new Vector2Int(0, -1),
        new Vector2Int(-1, 0),
    };

    private static readonly Vector2Int[] DiagonalDirections =
    {
        new Vector2Int(1, 1),
        new Vector2Int(1, -1),
        new Vector2Int(-1, -1),
        new Vector2Int(-1, 1),
    };

    public List<Vector2> FindPath(
        Vector2 startPosition,
        Vector2 destination,
        AStarGrid grid,
        bool allowDiagonal,
        int maxVisitedNodes
    )
    {
        var startCell = grid.WorldToCell(startPosition);
        var targetCell = FindNearestWalkableCell(grid, grid.WorldToCell(destination));

        if (!grid.IsWalkable(startCell) || !targetCell.HasValue)
            return null;

        if (startCell == targetCell.Value)
            return new List<Vector2> { destination };

        var openSet = new List<AStarNode>();
        var openLookup = new Dictionary<Vector2Int, AStarNode>();
        var closedSet = new HashSet<Vector2Int>();

        var startNode = new AStarNode(startCell)
        {
            GCost = 0,
            HCost = GetDistance(startCell, targetCell.Value, allowDiagonal),
        };

        openSet.Add(startNode);
        openLookup[startCell] = startNode;

        int visitedNodes = 0;
        maxVisitedNodes = Mathf.Max(1, maxVisitedNodes);

        while (openSet.Count > 0 && visitedNodes < maxVisitedNodes)
        {
            visitedNodes++;
            var currentNode = GetLowestCostNode(openSet);

            if (currentNode.Cell == targetCell.Value)
                return BuildWorldPath(currentNode, grid, destination);

            openSet.Remove(currentNode);
            openLookup.Remove(currentNode.Cell);
            closedSet.Add(currentNode.Cell);

            foreach (var neighborCell in GetNeighborCells(currentNode.Cell, allowDiagonal))
            {
                if (closedSet.Contains(neighborCell) || !CanMoveTo(grid, currentNode.Cell, neighborCell))
                    continue;

                int tentativeGCost =
                    currentNode.GCost + GetDistance(currentNode.Cell, neighborCell, allowDiagonal);

                if (!openLookup.TryGetValue(neighborCell, out var neighborNode))
                {
                    neighborNode = new AStarNode(neighborCell);
                    openLookup[neighborCell] = neighborNode;
                    openSet.Add(neighborNode);
                }
                else if (tentativeGCost >= neighborNode.GCost)
                {
                    continue;
                }

                neighborNode.Parent = currentNode;
                neighborNode.GCost = tentativeGCost;
                neighborNode.HCost = GetDistance(neighborCell, targetCell.Value, allowDiagonal);
            }
        }

        return null;
    }

    public List<Vector2> SmoothPath(List<Vector2> path, AStarGrid grid)
    {
        if (path == null || path.Count <= 2)
            return path;

        var result = new List<Vector2>();
        int currentIndex = 0;
        result.Add(path[currentIndex]);

        while (currentIndex < path.Count - 1)
        {
            int nextIndex = path.Count - 1;
            while (nextIndex > currentIndex + 1)
            {
                if (grid.HasLineOfSight(path[currentIndex], path[nextIndex]))
                    break;

                nextIndex--;
            }

            result.Add(path[nextIndex]);
            currentIndex = nextIndex;
        }

        return result;
    }

    private static AStarNode GetLowestCostNode(List<AStarNode> openSet)
    {
        var lowestCostNode = openSet[0];
        for (int i = 1; i < openSet.Count; i++)
        {
            var node = openSet[i];
            if (
                node.FCost < lowestCostNode.FCost
                || node.FCost == lowestCostNode.FCost && node.HCost < lowestCostNode.HCost
            )
            {
                lowestCostNode = node;
            }
        }
        return lowestCostNode;
    }

    private static IEnumerable<Vector2Int> GetNeighborCells(Vector2Int cell, bool allowDiagonal)
    {
        foreach (var direction in CardinalDirections)
        {
            yield return cell + direction;
        }

        if (!allowDiagonal)
            yield break;

        foreach (var direction in DiagonalDirections)
        {
            yield return cell + direction;
        }
    }

    private static bool CanMoveTo(AStarGrid grid, Vector2Int fromCell, Vector2Int toCell)
    {
        if (!grid.IsWalkable(toCell))
            return false;

        var delta = toCell - fromCell;
        if (Mathf.Abs(delta.x) + Mathf.Abs(delta.y) <= 1)
            return true;

        var horizontalCell = new Vector2Int(fromCell.x + delta.x, fromCell.y);
        var verticalCell = new Vector2Int(fromCell.x, fromCell.y + delta.y);
        return grid.IsWalkable(horizontalCell) && grid.IsWalkable(verticalCell);
    }

    private static int GetDistance(Vector2Int fromCell, Vector2Int toCell, bool allowDiagonal)
    {
        int distanceX = Mathf.Abs(fromCell.x - toCell.x);
        int distanceY = Mathf.Abs(fromCell.y - toCell.y);

        if (!allowDiagonal)
            return STRAIGHT_COST * (distanceX + distanceY);

        int diagonalDistance = Mathf.Min(distanceX, distanceY);
        int straightDistance = Mathf.Abs(distanceX - distanceY);
        return DIAGONAL_COST * diagonalDistance + STRAIGHT_COST * straightDistance;
    }

    private static List<Vector2> BuildWorldPath(
        AStarNode targetNode,
        AStarGrid grid,
        Vector2 destination
    )
    {
        var path = new List<Vector2>();
        var currentNode = targetNode;

        while (currentNode != null)
        {
            path.Add(grid.CellToWorld(currentNode.Cell));
            currentNode = currentNode.Parent;
        }

        path.Reverse();

        if (path.Count > 0)
            path[path.Count - 1] = destination;

        return path;
    }

    private static Vector2Int? FindNearestWalkableCell(AStarGrid grid, Vector2Int targetCell)
    {
        if (grid.IsWalkable(targetCell))
            return targetCell;

        const int SEARCH_RADIUS = 4;
        for (int radius = 1; radius <= SEARCH_RADIUS; radius++)
        {
            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    if (Mathf.Abs(x) != radius && Mathf.Abs(y) != radius)
                        continue;

                    var cell = targetCell + new Vector2Int(x, y);
                    if (grid.IsWalkable(cell))
                        return cell;
                }
            }
        }

        return null;
    }
}
