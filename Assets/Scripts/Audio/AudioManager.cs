using System;
using System.Collections.Generic;
using System.Linq;
using Qanht.NightRealm.ObjectPooling;
using UnityEngine;
using UnityEngine.Audio;

namespace Qanht.NightRealm.AudioManagement
{
    public static class AudioManager
    {
        public const string MASTER_VOLUME = "MasterVolume";
        public const string MUSIC_VOLUME = "MusicVolume";
        public const string SFX_VOLUME = "SFXVolume";
        public const string UI_VOLUME = "UIVolume";
        private const float MIN_VALUE = 0.0001f;
        private const float MAX_VALUE = 1f;

        public static readonly AudioMixer Mixer;
        private static readonly Dictionary<string, AudioAsset> _audioAssets;
        private static readonly Dictionary<AudioAsset.MixerGroup, AudioMixerGroup> _audioGroups;
        private static readonly Prefab _audioSourcePrefab;
        public static float MasterVolume
        {
            get =>
                Mathf.Clamp(PlayerPrefs.GetFloat(MASTER_VOLUME, MAX_VALUE), MIN_VALUE, MAX_VALUE);
            set
            {
                value = Mathf.Clamp(value, MIN_VALUE, MAX_VALUE);
                Mixer.SetFloat(MASTER_VOLUME, ValueToVolume(value));
                PlayerPrefs.SetFloat(MASTER_VOLUME, value);
            }
        }

        public static float MusicVolume
        {
            get => Mathf.Clamp(PlayerPrefs.GetFloat(MUSIC_VOLUME, MAX_VALUE), MIN_VALUE, MAX_VALUE);
            set
            {
                value = Mathf.Clamp(value, MIN_VALUE, MAX_VALUE);
                Mixer.SetFloat(MUSIC_VOLUME, ValueToVolume(value));
                PlayerPrefs.SetFloat(MUSIC_VOLUME, value);
            }
        }
        public static float SFXVolume
        {
            get => Mathf.Clamp(PlayerPrefs.GetFloat(SFX_VOLUME, MAX_VALUE), MIN_VALUE, MAX_VALUE);
            set
            {
                value = Mathf.Clamp(value, MIN_VALUE, MAX_VALUE);
                Mixer.SetFloat(SFX_VOLUME, ValueToVolume(value));
                PlayerPrefs.SetFloat(SFX_VOLUME, value);
            }
        }

        public static float UIVolume
        {
            get => Mathf.Clamp(PlayerPrefs.GetFloat(UI_VOLUME, MAX_VALUE), MIN_VALUE, MAX_VALUE);
            set
            {
                value = Mathf.Clamp(value, MIN_VALUE, MAX_VALUE);
                Mixer.SetFloat(UI_VOLUME, ValueToVolume(value));
                PlayerPrefs.SetFloat(UI_VOLUME, value);
            }
        }

        static AudioManager()
        {
            // Initialize collections up-front so the type is always usable,
            // even if a resource fails to load. Failing inside a static
            // constructor would raise TypeInitializationException and
            // permanently disable AudioManager for the whole session.
            _audioAssets = new();
            _audioGroups = new();

            Mixer = Resources.Load<AudioMixer>("Audio/AudioMixer");
            if (Mixer == null)
            {
                Debug.LogError(
                    "[AudioManager] Could not load Resources/Audio/AudioMixer. Audio disabled."
                );
                return;
            }

            foreach (var audioAsset in Resources.LoadAll<AudioAsset>("Audio/AudioAssets"))
            {
                if (audioAsset == null || string.IsNullOrEmpty(audioAsset.name))
                    continue;
                _audioAssets[audioAsset.name] = audioAsset;
            }

            var audioSourceObject = Resources.Load<GameObject>("Audio/AudioSourceHelper");
            if (audioSourceObject != null)
                _audioSourcePrefab = audioSourceObject.GetComponent<Prefab>();
            if (_audioSourcePrefab == null)
            {
                Debug.LogError(
                    "[AudioManager] Could not load Resources/Audio/AudioSourceHelper "
                        + "(or it is missing a Prefab component). Sound playback disabled."
                );
            }

            var mixerGroups = Mixer.FindMatchingGroups("");
            RegisterMixerGroup(mixerGroups, AudioAsset.MixerGroup.Master, "Master");
            RegisterMixerGroup(mixerGroups, AudioAsset.MixerGroup.Music, "Music");
            RegisterMixerGroup(mixerGroups, AudioAsset.MixerGroup.SFX, "SFX");
            RegisterMixerGroup(mixerGroups, AudioAsset.MixerGroup.UI, "UI");

            LoadVolume();
        }

        private static void RegisterMixerGroup(
            AudioMixerGroup[] mixerGroups,
            AudioAsset.MixerGroup key,
            string groupName
        )
        {
            var group = mixerGroups.FirstOrDefault(g => g.name == groupName);
            if (group != null)
                _audioGroups[key] = group;
            else
                Debug.LogError(
                    $"[AudioManager] Mixer group \"{groupName}\" not found in AudioMixer."
                );
        }

        public static void LoadVolume()
        {
            MasterVolume = MasterVolume;
            MusicVolume = MusicVolume;
            SFXVolume = SFXVolume;
            UIVolume = UIVolume;
        }

        public static float ValueToVolume(float value)
        {
            return Mathf.Log10(value) * 20;
        }

        public static float VolumeToValue(float volume)
        {
            return Mathf.Pow(10f, volume / 20);
        }

        public static PoolingAudioSource Play(string soundName)
        {
            if (!_audioAssets.TryGetValue(soundName, out AudioAsset audioAsset))
            {
                Debug.LogWarning($"Sound \"{soundName}\" does not exits!");
                return NullSource.Instance;
            }

            if (
                _audioSourcePrefab == null
                || !PoolManager.Get<PoolingAudioSource>(_audioSourcePrefab, out var audioSource)
            )
            {
                Debug.LogError("Fail to create audio source. Please check your resource floder");
                return NullSource.Instance;
            }

            _audioGroups.TryGetValue(audioAsset.Mixer, out var mixerGroup);
            return audioSource.Play(audioAsset.AudioClip, mixerGroup);
        }
    }
}
