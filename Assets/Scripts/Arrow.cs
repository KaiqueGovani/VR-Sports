using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public Transform tip;

    private Rigidbody _rigidBody;
    private bool _inAir = false;
    private Vector3 _lastPosition = Vector3.zero;
    public bool hasHit = false;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        PullInteraction.PullActionReleased += Release;

        Stop();
    }

    private void OnDestroy()
    {
        PullInteraction.PullActionReleased -= Release;
    }

    private void Release(float value)
    {
        PullInteraction.PullActionReleased -= Release;
        gameObject.transform.parent = null;
        _inAir = true;
        SetPhysics(true);

        Vector3 force = speed * value * transform.forward;
        _rigidBody.AddForce(force, ForceMode.Impulse);

        StartCoroutine(RotateWithVelocity());

        _lastPosition = tip.position;
    }

    private IEnumerator RotateWithVelocity()
    {
        yield return new WaitForFixedUpdate();
        while (_inAir)
        {
            Quaternion newRotation = Quaternion.LookRotation(_rigidBody.velocity, transform.up);
            transform.rotation = newRotation;
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Rigidbody body) && !collision.gameObject.CompareTag("Flecha") && !collision.gameObject.CompareTag("Player"))
        {
            _rigidBody.interpolation = RigidbodyInterpolation.None;
            transform.parent = collision.transform;
            body.AddForce(_rigidBody.velocity, ForceMode.Impulse);
        }
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Flecha"))
        {
            Stop();
        }
    }

    private void Stop()
    {
        _inAir = false;
        SetPhysics(false);
    }

    private void SetPhysics(bool usePhysics)
    {
        _rigidBody.useGravity = usePhysics;
        _rigidBody.isKinematic = !usePhysics;
    }
}
