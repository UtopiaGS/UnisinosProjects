using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Data", menuName = "ScriptableObjects/PlayRandomSounds", order =1)]
public class PlayRandomSound : ScriptableObject
{
    public List<AudioClip> AttackClips = new List<AudioClip>();
    public List<AudioClip> DamageClips = new List<AudioClip>();

}
