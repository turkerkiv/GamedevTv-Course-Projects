using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Vector2Int _startCoordinates;
    public Vector2Int StartCoordinates { get { return _startCoordinates; } }

    [SerializeField] Vector2Int _destinationCoordinates;
    public Vector2Int DestinationCoordinates { get { return _destinationCoordinates; } }

    Node _startNode;
    Node _destinationNode;
    Node _currentSearchNode;

    Dictionary<Vector2Int, Node> _reached = new Dictionary<Vector2Int, Node>();
    Queue<Node> _frontier = new Queue<Node>();

    Vector2Int[] _directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager _gridManager;
    Dictionary<Vector2Int, Node> _grid = new Dictionary<Vector2Int, Node>();

    private void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();
        _grid = _gridManager.Grid;

        _startNode = _grid[_startCoordinates];
        _destinationNode = _grid[_destinationCoordinates];

    }

    private void Start()
    {
        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(_startCoordinates);
    }

    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        _gridManager.ResetNodes();
        BreadthFirstSearch(coordinates); //creating tree and shortest path
        return BuildPath(); //take the path from ending because there is one connection to end node and reverse it to correct order
    }

    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        //finding neighbors
        foreach (Vector2Int direction in _directions)
        {
            Vector2Int neighborCoordinate = _currentSearchNode._coordinates + direction;

            if (_grid.ContainsKey(neighborCoordinate))
            {
                neighbors.Add(_grid[neighborCoordinate]);
            }
        }

        //making neighbors connect to current and add them to frontier
        foreach (Node neighbor in neighbors)
        {
            if (_reached.ContainsKey(neighbor._coordinates) || !neighbor._isWalkable) { continue; }

            neighbor._connectedTo = _currentSearchNode;
            _reached.Add(neighbor._coordinates, neighbor);
            _frontier.Enqueue(neighbor);
        }
    }

    void BreadthFirstSearch(Vector2Int coordinates)
    {
        _startNode._isWalkable = true;
        _destinationNode._isWalkable = true;

        _frontier.Clear();
        _reached.Clear();

        bool isRunning = true;

        _frontier.Enqueue(_grid[coordinates]);//taking start node
        _reached.Add(coordinates, _grid[coordinates]);

        while (_frontier.Count > 0 && isRunning)
        {
            _currentSearchNode = _frontier.Dequeue(); //removes the node ahead of us and make it current
            _currentSearchNode._isExplored = true;

            ExploreNeighbors();

            if (_currentSearchNode._coordinates == _destinationCoordinates)
            {
                isRunning = false;
            }
        }
    }

    List<Node> BuildPath()
    {
        //starting from the end we are creating path by making every current node equals its connected to
        List<Node> path = new List<Node>();
        Node currentNode = _destinationNode;

        path.Add(currentNode);
        currentNode._isPath = true;

        while (currentNode._connectedTo != null)
        {
            currentNode = currentNode._connectedTo;
            path.Add(currentNode);
            currentNode._isPath = true;
        }
        path.Reverse();

        return path;
    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (_grid.ContainsKey(coordinates))
        {
            bool previousState = _grid[coordinates]._isWalkable;

            _grid[coordinates]._isWalkable = false;
            List<Node> newPath = GetNewPath(); //creating a temporary path
            _grid[coordinates]._isWalkable = previousState;

            //because of we starting from end if we blocked our path then there will be no tile connected to destination
            if (newPath.Count <= 1)
            {
                GetNewPath(); //resetting back the temporary path
                return true;
            }
        }

        return false;
    }

    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }
}