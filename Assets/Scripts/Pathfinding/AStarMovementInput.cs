using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AStarMovementInput : BaseMovementInput
{
    [Header("A* pathfinding")]
    [Min(0.05f)]
    [SerializeField]
    protected float cellSize = 0.5f;

    [Min(1)]
    [SerializeField]
    protected int maxSearchDistance = 80;

    [Min(1)]
    [SerializeField]
    protected int maxVisitedNodes = 1500;

    [Min(0.01f)]
    [SerializeField]
    protected float obstacleCheckRadius = 0.2f;

    [Min(0.01f)]
    [SerializeField]
    protected float nextWaypointDistance = 0.2f;

    [Min(0)]
    [SerializeField]
    protected float repathCooldown = 0.15f;

    [SerializeField]
    protected bool allowDiagonal = true;

    [SerializeField]
    protected bool smoothPath = true;

    [SerializeField]
    protected bool drawDebugPath = true;

    private readonly AStarPathfinder pathfinder = new AStarPathfinder();
    private readonly List<Vector2> currentPath = new List<Vector2>();
    private Coroutine movingRoutine;
    private float nextAllowedPathTime;
    private Vector2 lastDestination;
    private bool hasLastDestination;

    public void MoveTo(Vector2 destination)
    {
        if (!isActiveAndEnabled)
            return;

        if (
            hasLastDestination
            && Time.time < nextAllowedPathTime
            && movingRoutine != null
            && Vector2.Distance(lastDestination, destination) <= cellSize
        )
        {
            return;
        }

        lastDestination = destination;
        hasLastDestination = true;
        nextAllowedPathTime = Time.time + repathCooldown;
        var grid = CreateGrid();
        var path = pathfinder.FindPath(
            transform.position,
            destination,
            grid,
            allowDiagonal,
            maxVisitedNodes
        );

        if (path == null || path.Count == 0)
        {
            path = grid.HasLineOfSight(transform.position, destination)
                ? new List<Vector2> { destination }
                : null;
        }
        else if (smoothPath)
        {
            path = pathfinder.SmoothPath(path, grid);
        }

        if (path == null || path.Count == 0)
        {
            StopMove();
            return;
        }

        currentPath.Clear();
        currentPath.AddRange(path);

        if (movingRoutine != null)
        {
            StopCoroutine(movingRoutine);
        }
        movingRoutine = StartCoroutine(FollowPathCoroutine());
    }

    public void StopMove()
    {
        if (movingRoutine != null)
        {
            StopCoroutine(movingRoutine);
            movingRoutine = null;
        }

        currentPath.Clear();
        hasLastDestination = false;
        InputVector = Vector2.zero;
    }

    private AStarGrid CreateGrid()
    {
        return new AStarGrid(
            transform.position,
            cellSize,
            obstacleCheckRadius,
            maxSearchDistance,
            LayerMaskHelper.ObstacleMask
        );
    }

    private IEnumerator FollowPathCoroutine()
    {
        int currentWaypoint = 0;
        float minDistanceSqr = nextWaypointDistance * nextWaypointDistance;

        while (currentWaypoint < currentPath.Count)
        {
            var target = currentPath[currentWaypoint];
            var toTarget = target - (Vector2)transform.position;

            if (toTarget.sqrMagnitude <= minDistanceSqr)
            {
                currentWaypoint++;
                continue;
            }

            InputVector = toTarget.normalized;
            yield return CoroutineHelper.fixedUpdateWait;
        }

        movingRoutine = null;
        InputVector = Vector2.zero;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (!drawDebugPath || currentPath.Count == 0)
            return;

        Gizmos.color = Color.cyan;
        var previousPoint = (Vector2)transform.position;
        foreach (var point in currentPath)
        {
            Gizmos.DrawLine(previousPoint, point);
            Gizmos.DrawWireSphere(point, nextWaypointDistance);
            previousPoint = point;
        }
    }
#endif
}
