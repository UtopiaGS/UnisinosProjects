using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class StoneTrigger : MonoBehaviour
{

    Rigidbody _body;

    public List<AudioClip> ClipsExplosion = new List<AudioClip>();

    private Vector3 _currentVelocity;
    private float dot;
    private Vector3 _previousVelocity;

    UnityEvent _OnHit = new UnityEvent();
    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _OnHit.AddListener(CallRandomExplosion);
    }

    void CallRandomExplosion() {
        int i = Random.Range(0, ClipsExplosion.Count - 1);
        Debug.Log("I is " + i);
        SoundPlayer.Instance.PlayClip(ClipsExplosion[i]);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _previousVelocity = _currentVelocity;
        _currentVelocity = _body.velocity;
        dot = Vector3.Dot(_previousVelocity, _currentVelocity);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bricks") && dot>15)
        {
            _OnHit.Invoke();
        }
    }
}
