using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower _tower;
    [SerializeField] bool _isPlaceable;
    public bool Isplaceable { get { return _isPlaceable; } }

    GridManager _gridManager;
    Pathfinder _pathfinder;
    Vector2Int _coordinates = new Vector2Int();


    private void Awake()
    {
        _pathfinder = FindObjectOfType<Pathfinder>();
        _gridManager = FindObjectOfType<GridManager>();
    }
    private void Start()
    {
        if (_gridManager != null)
        {
            _coordinates = _gridManager.GetCoordinatesFromPosition(transform.position);

            if (!Isplaceable)
            {
                _gridManager.BlockNode(_coordinates);
            }
        }
    }

    private void OnMouseDown()
    {
        if (_isPlaceable && _gridManager.GetNode(_coordinates)._isWalkable && !_pathfinder.WillBlockPath(_coordinates))
        {
            bool isSuccesful = _tower.CreateTower(_tower, transform.position);
            if (isSuccesful)
            {
                _gridManager.BlockNode(_coordinates);
                _pathfinder.NotifyReceivers();
            }
        }
    }
}
