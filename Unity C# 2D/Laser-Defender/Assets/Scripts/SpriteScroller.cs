using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] Vector2 _moveSpeed;

    Vector2 _offset;
    Material _material;

    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        _offset += _moveSpeed * Time.deltaTime;
        _material.mainTextureOffset = _offset;
    }
}
