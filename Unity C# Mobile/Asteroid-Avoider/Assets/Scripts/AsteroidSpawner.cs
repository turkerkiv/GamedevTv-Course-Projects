using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] _asteroidPrefabs;
    [SerializeField] float _secondsBetweenAsteroids = 1.5f;
    [SerializeField] Vector2 _forceRange;

    float _timer;
    Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;
        _timer = _secondsBetweenAsteroids;
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer < Mathf.Epsilon)
        {
            SpawnAsteroid();

            _timer = _secondsBetweenAsteroids;
        }
    }

    void SpawnAsteroid()
    {
        int side = Random.Range(0, 4);

        Vector2 viewportSpawnPoint = Vector2.zero;
        Vector2 viewportDirection = Vector2.zero;

        switch (side)
        {
            case 0:
                //left
                viewportSpawnPoint.x = 0; //soldaysa x 0 yani sol.
                viewportSpawnPoint.y = Random.value; //ysi soldan 0la 1 arası.
                viewportDirection = new Vector2(1f, Random.Range(-1f, 1f)); // direction x sağa doğru ysi ya yukarı ya aşağı yani -1le 1.
                break;
            case 1:
                //right
                viewportSpawnPoint.x = 1;
                viewportSpawnPoint.y = Random.value;
                viewportDirection = new Vector2(-1f, Random.Range(-1f, 1f));
                break;
            case 2:
                //bottom
                viewportSpawnPoint.x = Random.value;
                viewportSpawnPoint.y = 0;
                viewportDirection = new Vector2(Random.Range(-1, 1), 1f);
                break;
            case 3:
                //Top
                viewportSpawnPoint.x = Random.value;
                viewportSpawnPoint.y = 1;
                viewportDirection = new Vector2(Random.Range(-1f, 1f), -1f);
                break;
        }

        Vector2 worldSpawnPoint = _mainCamera.ViewportToWorldPoint(viewportSpawnPoint);

        GameObject selectedAsteroid = _asteroidPrefabs[Random.Range(0, _asteroidPrefabs.Length)];

        GameObject asteroidInstance = Instantiate(selectedAsteroid, worldSpawnPoint, Quaternion.Euler(0f, 0f, Random.Range(0f, 359f)));

        Rigidbody asteroidRb = asteroidInstance.GetComponent<Rigidbody>();

        Vector2 worldDirection = _mainCamera.ViewportToWorldPoint(viewportDirection);
        asteroidRb.velocity = worldDirection.normalized * Random.Range(_forceRange.x, _forceRange.y);
    }
}
