using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float _shakeDuration = 1f;
    [SerializeField] float _shakeMagnitude = 0.5f;

    Vector3 _initialPosition;

    void Start()
    {
        _initialPosition = transform.position;
    }

    public void PlayShaking()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float elapsedTime = 0f;
        while (elapsedTime < _shakeDuration)
        {
            //burada olduğumuz yerle toplayarak z ekseninin 0 olmasını engelliyoruz. Ayrıca hangi pozisyonda olursak olalım yakınlara gelmiş oluyor yeni poz
            //öbür türlü bir anda orta kısma ışınlanırdı ve geri gelirdi.
            transform.position = _initialPosition + (Vector3)(Random.insideUnitCircle * _shakeMagnitude);
            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        transform.position = _initialPosition;
    }
}
