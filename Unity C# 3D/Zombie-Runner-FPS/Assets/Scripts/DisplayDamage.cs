using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDamage : MonoBehaviour
{
    [SerializeField] Canvas _damageReceivingCanvas;
    [SerializeField] float _impactTime = 0.1f;

    private void Start()
    {
        _damageReceivingCanvas.enabled = false;
    }

    public void ShowDamageImpact()
    {
        StartCoroutine(ShowSplatter());
    }

    IEnumerator ShowSplatter()
    {
        _damageReceivingCanvas.enabled = true;

        yield return new WaitForSeconds(_impactTime);

        _damageReceivingCanvas.enabled = false;
    }
}
