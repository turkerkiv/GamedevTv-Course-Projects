using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2Int _coordinates;
    public bool _isWalkable;
    public bool _isExplored;
    public bool _isPath;
    public Node _connectedTo;

    public Node(Vector2Int coordinates, bool isWalkable)
    {
        _coordinates = coordinates;
        _isWalkable = isWalkable;
    }
}
