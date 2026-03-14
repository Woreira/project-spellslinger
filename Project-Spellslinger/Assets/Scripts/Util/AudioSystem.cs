using UnityEngine;
using System.Collections.Generic;

public static class AudioSystem
{
    private static Dictionary<string, SwapbackList<AudioSource>> _poolById = new Dictionary<string, SwapbackList<AudioSource>>();
    private static Dictionary<string, int> _instanceById = new Dictionary<string, int>();

    public static void InitPool(string id, int capacity = 10)
    {
        if (_poolById.ContainsKey(id)) return;
        Globals.AudioCollection.GetItemById(id, out var entry);
        var list = new SwapbackList<AudioSource>(capacity);
        for (var i = 0; i < capacity; i++)
        {
            var instance = Object.Instantiate(Globals.AudioCollection.AudioSourcePrefab);
            instance.clip = entry.Clip;
            instance.volume = entry.Volume + Random.Range(-entry.VolumeVariance, entry.VolumeVariance);
            instance.pitch = entry.Pitch + Random.Range(-entry.PitchVariance, entry.PitchVariance);
            instance.loop = entry.Loop;
            instance.priority = entry.Priority;
            instance.panStereo = entry.PanStereo;
            instance.spatialBlend = entry.Spatialize ? entry.SpatialBlend : 0;
            instance.minDistance = entry.MinDistance;
            instance.maxDistance = entry.MaxDistance;
            instance.rolloffMode = entry.RolloffMode;
            instance.outputAudioMixerGroup = entry.MixerGroup;            
        }
        _poolById.Add(id, list);
    }

    private static bool TryGetPooledAudioSource(string id, out AudioSource instance)
    {
        instance = null;
        if (_poolById.TryGetValue(id, out var pool))
        {
            if (pool.Count <= 0)    //note(woreira): we should not get a audio source when the pool blows out to avoid voice limit bugs
            {
                Debug.LogWarning($"[AudioSystem]: audio pool with id [{id}] exceeded capacity");
                return false;
            }
            instance = pool.Pop();
            return true;
        }
        return false;
    }

    public static AudioSource Get(string id)
    {
        AudioSource instance;
        if (!TryGetPooledAudioSource(id, out instance))
        {
            InitPool(id);
            TryGetPooledAudioSource(id, out instance);
        }

        if (instance == null) return null;

        _instanceById[id] = instance.GetInstanceID();
        instance.gameObject.SetActive(true);
        return instance;
    }


}