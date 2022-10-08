using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float _thrustForce = 150f;
    [SerializeField] float _rotateSpeed = 150f;
    [SerializeField] AudioClip _thrustSound;
    [SerializeField] ParticleSystem _thrustParticles;
    [SerializeField] ParticleSystem _rightThrustParticles;
    [SerializeField] ParticleSystem _leftThrustParticles;

    Rigidbody _rb;
    AudioSource _audioSource;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ThrustRocket();
        RotateRocket();
    }

    void RotateRocket()
    {
        if (Input.GetKey(KeyCode.A))
        {
            StartRotating(Vector3.forward, _leftThrustParticles);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            StartRotating(Vector3.back, _rightThrustParticles);
        }
        else
        {
            StopRotating();
        }
    }

    void ThrustRocket()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void StartThrusting()
    {
        _rb.AddRelativeForce(Vector3.up * _thrustForce * Time.deltaTime);

        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(_thrustSound);
        }
        if (!_thrustParticles.isPlaying)
        {
            _thrustParticles.Play();
        }
    }

    void StopThrusting()
    {
        _audioSource.Stop();
        _thrustParticles.Stop();
    }
    void StartRotating(Vector3 direction, ParticleSystem particleSystem)
    {
        _rb.freezeRotation = true;
        transform.Rotate(direction * Time.deltaTime * _rotateSpeed);
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;

        if (particleSystem.isPlaying)
            return;
        particleSystem.Play();
    }

    void StopRotating()
    {
        _leftThrustParticles.Stop();
        _rightThrustParticles.Stop();
    }
}
