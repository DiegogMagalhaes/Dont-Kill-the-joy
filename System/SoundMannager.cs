using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundMannager
{
    public enum songs_name{
        Explosion,
        Blade,
        Hit,
    };

    private static float volume = 25f;

    public static List<AudioClip> sound_list = new List<AudioClip>();

    public static void PlaySound(songs_name sound)
    {
        GameObject sound_obj = new GameObject("Sound");
        sound_obj.tag = "Songs";
        sound_obj.transform.SetParent(GameObject.Find("Sounds").GetComponent<Transform>());
        AudioSource audio_source = sound_obj.AddComponent<AudioSource>();

        AudioClip clip = GetAudioClip(sound);
        audio_source.volume = GetVolume();

        audio_source.PlayOneShot(clip);
        Object.Destroy(sound_obj, clip.length);
    }

    public static void PlaySound(songs_name sound, Vector3 position)
    {
        GameObject sound_obj = new GameObject("Sound");
        sound_obj.transform.position = position;
        sound_obj.transform.SetParent(GameObject.Find("Sounds").GetComponent<Transform>());
        AudioSource audio_source = sound_obj.AddComponent<AudioSource>();
        Object.Destroy(sound_obj, 5f);

        audio_source.volume = GetVolume();

        audio_source.PlayOneShot(GetAudioClip(sound));

        Object.Destroy(sound_obj, 10f);
    }

    private static AudioClip GetAudioClip(songs_name sound)
    {
        foreach (SoundAssets.SoundAudioClip soundAudioClip in SoundAssets.i.audioClip_array)
        {
            if(soundAudioClip.sound_name == sound)
            {
                return soundAudioClip.audioClip;
            }
        }

        Debug.LogError("Sound" + sound + "noit found");
        return null;
    }

    public static void SetVolume(float newvolume)
    {
        if (newvolume <= 100 && newvolume >= 0) volume = newvolume;
        else Debug.Log("o volume deve ser um numero de 0 a 100");
    }

    public static float GetVolume()
    {
        return volume / 100;
    }
}
