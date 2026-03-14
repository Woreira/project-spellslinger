using UnityEngine;

[CreateAssetMenu(fileName = "Audio-Collection", menuName = "Collections/Audio Collection")]
public class AudioCollectionSO : CollectionSO<AudioEntry>
{
    public AudioSource AudioSourcePrefab;
}