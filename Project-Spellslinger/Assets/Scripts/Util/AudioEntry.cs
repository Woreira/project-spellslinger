using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class AudioEntry : Entry
{
    public AudioClip Clip;
    [Range(0f,1f)] public float Volume = 1f;
    [Range(0f,1f)] public float VolumeVariance = 0f;
    public float Pitch = 1f;
    public float PitchVariance = 0f;
    public bool Loop = false;
    public bool Spatialize = false;
    [Range(0f,1f)] public float SpatialBlend = 0f;
    public float MinDistance = 1f;
    public float MaxDistance = 500f;
    public AudioRolloffMode RolloffMode = AudioRolloffMode.Logarithmic;
    public bool PlayOnAwake = false;
    public bool BypassEffects = false;
    public bool BypassListenerEffects = false;
    public bool BypassReverbZones = false;
    [Range(0,256)] public int Priority = 128;
    [Range(-1,1)] public float PanStereo = 0f;
    public AudioMixerGroup MixerGroup;
}