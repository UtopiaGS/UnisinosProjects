using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class StoneTrigger : MonoBehaviour
{

    Rigidbody _body;

    public RandomAudioClip _clips;

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
        SoundPlayer.Instance.PlayClipWithoutLoop(_clips.ReturnChosenClip());
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
        if (other.gameObject.CompareTag("Bricks") && dot>20)
        {           
            _OnHit.Invoke();
        }
    }
}
