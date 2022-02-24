using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAssets : MonoBehaviour
{
    private static SoundAssets _i;

    public static SoundAssets i
    {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<SoundAssets>("Prefabs/SoundAsset"));
            return _i;
        }
    }

    public SoundAudioClip[] audioClip_array;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundMannager.songs_name sound_name;
        public AudioClip audioClip;
    }
}
