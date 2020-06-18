using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    private AudioSource _source;

    public List<AudioClip> Clips = new List<AudioClip>();

    public static SoundPlayer Instance;

    public bool IsPlaying => _source.isPlaying;

    public AudioClip ClipPlaying => _source.clip;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    public void PlayClipId(int i) {
        if(Clips[i]!=null)
            _source.PlayOneShot(Clips[i]);
    }
    public void PlayClip(AudioClip i)
    {
            _source.PlayOneShot(i);
    }

    public void PlayClipWithoutLoop(AudioClip i)
    {
        if(_source.clip != i)
            _source.PlayOneShot(i);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
