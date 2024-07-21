using System;
using Runtime.Extensions;
using UnityEngine;

namespace Managers
{
    [Serializable]
    public class GameSound
    {
        public SoundType type;
        public AudioClip clip;
        public float volume = 1f;

        public bool isOneShot;
        public AudioSource externalSource;
    }

    public class SoundManager : SingletonMonoBehaviour<SoundManager>
    {
        [Header("Sound Settings")]
        [SerializeField] private GameSound[] gameSounds;

        [Header("Info - No Touch")]
        [SerializeField] private AudioSource audioSource;

        public void PlaySound(SoundType type)
        {
            var gameSound = Array.Find(gameSounds, sound => sound.type == type);

            if (gameSound == null)
            {
                Debug.LogError($"Sound with type {type} not found!");
                return;
            }

            if (gameSound.isOneShot)
            {
                audioSource.PlayOneShot(gameSound.clip, gameSound.volume);
            }

            else
            {
                gameSound.externalSource.clip = gameSound.clip;
                gameSound.externalSource.volume = gameSound.volume;
                gameSound.externalSource.Play();
            }
        }
    }
}