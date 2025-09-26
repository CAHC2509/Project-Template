using System;
using UnityEngine;

[Serializable]
public class AudioData
{
    public AudioClip AudioClip;
    public float MinVolume = 1f;
    public float MaxVolume = 1f;
    public float MinPitch = 1f;
    public float MaxPitch = 1f;
}
