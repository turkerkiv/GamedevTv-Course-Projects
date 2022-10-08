using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color _exploredColor = Color.yellow;
    [SerializeField] Color _pathColor = new Color(1f, 0.5f, 0f);
    [SerializeField] Color _defaultColor = Color.white;
    [SerializeField] Color _blockedColor = Color.gray;

    TextMeshPro _label;
    Vector2Int _coordinates = new Vector2Int();
    GridManager _gridManager;

    void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();

        _label = GetComponent<TextMeshPro>();
        _label.enabled = true;

        DisplayCoordinates();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }

        SetLabelColor();
        ToggleLabels();
    }

    private void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _label.enabled = !_label.IsActive();
        }
    }

    void DisplayCoordinates()
    {
        if (_gridManager == null) { return; }

        _coordinates.x = Mathf.RoundToInt(transform.parent.position.x / _gridManager.UnityGridSize);
        _coordinates.y = Mathf.RoundToInt(transform.parent.position.z / _gridManager.UnityGridSize);

        _label.text = _coordinates.x + "," + _coordinates.y;
    }

    void SetLabelColor()
    {
        if (_gridManager == null) { return; }

        Node node = _gridManager.GetNode(_coordinates);

        if (node == null) { return; }

        if (!node._isWalkable)
        {
            _label.color = _blockedColor;
        }
        else if (node._isPath)
        {
            _label.color = _pathColor;
        }
        else if (node._isExplored)
        {
            _label.color = _exploredColor;
        }
        else
        {
            _label.color = _defaultColor;
        }
    }

    void UpdateObjectName()
    {
        transform.parent.name = _coordinates.ToString();
    }
}
