using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TrebuchetComponets : MonoBehaviour
{
    public Transform StoneTarget;
    public Rigidbody Weight;
    public HingeJoint Rope;
    [Range(1,1500)]
    [SerializeField] private float _speed=45;

    private bool _wasFired;
    public bool WasFired => _wasFired;

    public RandomAudioClip SoldierAudioClips;
    // Start is called before the first frame update
    void Start()
    {
        _wasFired = false;
    }

    public void ThrowProjectile(float force) {
        Weight.mass = force;
        _wasFired = true;
        Rope.breakForce = 0;
        Weight.AddForce(0, -(force), 0);
        SoundPlayer.Instance.PlayClip(SoldierAudioClips.ReturnChosenClip());
        SliderPingPong.Instance.StopPingPong();

        Debug.Log("Throwing!");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) && !_wasFired)
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * _speed * Time.deltaTime, 0);
           
        }
        if (Input.GetKey(KeyCode.D) && !_wasFired)
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * _speed * Time.deltaTime, 0);
           
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A)) {
            if (!SoundPlayer.Instance.IsPlaying)
                SoundPlayer.Instance.PlayClipId(9);
        }
    }
}
