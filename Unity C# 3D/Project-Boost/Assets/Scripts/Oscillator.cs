using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 _startingPosition;
    [SerializeField] Vector3 _movementVector;
    float _movementFactor;
    [SerializeField] float _period = 2f;

    void Start()
    {
        _startingPosition = transform.position;
    }

    void Update()
    {
        if (_period <= Mathf.Epsilon)
            return;

        float cycles = Time.time / _period; //period arttıkça radius artmış olacak. burada bir daire çizimini alıyoruz.

        const float tau = Mathf.PI * 2; //6.28 olayı tau
        float rawSinWave = Mathf.Sin(cycles * tau); //burada o circle oluştukça sinwave çiziyoruz. 1 cycleda 6.28 radiantlı daire oldu. yani doğru açılarla. 1den sonrasında da sinwavein yukarı aşağı hareketi gibi loop yani.

        _movementFactor = (rawSinWave + 1f) / 2; //-1 ve 1 arası olsaydı startingpos mid point olurdu onun yerine 0 ile 2 arası yapıp 2 ye böldük ki 0 1 olsun. yani startingpos start oluyor

        Vector3 offset = _movementVector * _movementFactor;
        transform.position = _startingPosition + offset;
    }
}
