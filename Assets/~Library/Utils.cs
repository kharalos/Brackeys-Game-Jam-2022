using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    /// <summary>
    /// Plays audio once in a separate gameobject with DontDestroyOnLoad, then destroys it when complete.
    /// </summary>
    public static void PlayAudio(AudioClip clip, float pitchMin = 1, float pitchMax = 1) {
        var go = new GameObject("audioClip");
        var source = go.AddComponent<AudioSource>();
        GameObject.DontDestroyOnLoad(go);


        source.clip = clip;
        source.pitch = Random.Range(pitchMin, pitchMax);
        source.Play();
        GameObject.Destroy(go, clip.length + 0.5f);
    }
    public static AudioSource PlayAudioAndFade(AudioClip clip, float fadeLength, float pitchMin = 1, float pitchMax = 1f) {
        var go = new GameObject("audioClip");
        var source = go.AddComponent<AudioSource>();
        GameObject.DontDestroyOnLoad(go);


        source.clip = clip;
        source.pitch = Random.Range(pitchMin, pitchMax);
        source.Play();
        CoroutineRunner.RunCoroutine(DecreaseVolume());
        GameObject.Destroy(go, clip.length + 0.5f);

        return source;

        IEnumerator DecreaseVolume() {
            while (source.volume > 0) {
                source.volume -= 1 / fadeLength * Time.unscaledDeltaTime;
                yield return null;
            }
            source.Stop();
        }
    }

    private static System.Random rng = new System.Random();
    public static void Shuffle<T>(this IList<T> list) {
        int n = list.Count;
        while (n > 1) {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    internal static void PlayAudio(object clip) {
        throw new System.NotImplementedException();
    }
}
