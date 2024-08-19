using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using YuzuValen.Utils;
public class AudioHandler : MonoBehaviourSingleton<AudioHandler>
{
    public SerializedDictionary<string, AudioClip> audioClips = new();

    public void PlayAudio(string audioName)
    {
        if (audioClips.TryGetValue(audioName, out AudioClip audioClip))
        {
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
        }
        else
        {
            Debug.LogError($"Audio clip with name {audioName} not found.");
        }
    }
}
