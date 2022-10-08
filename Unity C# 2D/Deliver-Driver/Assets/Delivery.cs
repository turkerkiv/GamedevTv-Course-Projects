using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    private bool _hasPackage;
    [SerializeField] float _destroyDelay = 0.5f;
    [SerializeField] Color32 _hasPackageColor = new Color32(255,255,255,255);
    [SerializeField] Color32 _noPackageColor = new Color32(255,255,255,255);
    SpriteRenderer _spriteRenderer;
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Ouch!");
    }

    void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.tag == "Package" && !_hasPackage)
        {
            Debug.Log("Package picked up...");
            
            _hasPackage = true;
            _spriteRenderer.color = _hasPackageColor;
            Destroy(other.gameObject, _destroyDelay);
        }

        if (other.tag == "Customer" && _hasPackage)
        {
            Debug.Log("Package delivered...");
        
            _hasPackage = false;
            _spriteRenderer.color = _noPackageColor;}
    }

}