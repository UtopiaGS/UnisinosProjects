
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RandomAudioClip", order = 1)]
public class RandomAudioClip : ScriptableObject
{
    [SerializeField]
    public List<AudioClip> RandomClips = new List<AudioClip>();
    // Start is called before the first frame update
   
    public AudioClip ReturnChosenClip() {
    
        return RandomClips[Random.Range(0, RandomClips.Count)];
    }
}
