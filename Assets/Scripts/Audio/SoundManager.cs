using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using System;

namespace  BW
{
    public class SoundManager : MonoSingleton<SoundManager>
    {
        public enum AudioType { Music, SFX }

        [Header("===Audio Mixer===")]
        [SerializeField] private AudioMixer audioMixer;

        [Header("===Audio Source===")]
        [SerializeField] private Transform musicAudioTransform;
        [SerializeField] private Transform sfxAudioTransform;
        
        [Header("===Audio Management===")]
        [SerializeField] private int audioID = 0;
        private static Dictionary<int, AudioSource> MusicAudioDic = new Dictionary<int, AudioSource>();
        private static Dictionary<int, AudioSource> SFXAudioDic = new Dictionary<int, AudioSource>();

        [Header("===App Pause===")]
        private bool applicationPause = false;

        [Header("===!!! Test Audio Clip !!!===")]
        public AudioClip[] misicClip_Test;
        public AudioClip[] sfxClip_Test;
        public Transform testTarnsform;

        #region Music
        public int PlayMusic(AudioClip clip, Transform transform = null)
        {
            return playMusic(AudioType.Music, clip, 1f, transform);
        }

        public int PlayMusic(AudioClip clip, float volume, Transform transform = null)
        {
            return playMusic(AudioType.Music, clip, volume, transform);
        }

        /// <summary>
        /// Base Music
        /// </summary>
        private int playMusic(AudioType audioType, AudioClip audioClip, float volume, Transform transform)
        {
            StopAllAudio(audioType, 1f);

            int audioID = AddAudio(audioType, audioClip, volume, transform);

            StartCoroutine(FadeOutCoroutine(GetAudio(audioType, audioID), 1f));

            return audioID;
        }
        #endregion

        #region SFX
        public int PlaySFX(AudioClip clip, Transform transform = null)
        {
            return PlaySFX(AudioType.SFX, clip, 1f, transform);
        }

        public int PlaySFX(AudioClip clip, float volume, Transform transform = null)
        {
            return PlaySFX(AudioType.SFX, clip, volume, transform);
        }

        /// <summary>
        /// Base SFX
        /// </summary>
        private int PlaySFX(AudioType audioType, AudioClip audioClip, float volume, Transform transform)
        {
            int audioID = AddAudio(audioType, audioClip, volume, transform);

            StartCoroutine(FadeOutCoroutine(GetAudio(audioType, audioID), 0f));

            return audioID;
        }
        #endregion

        #region Add AudioSource
        private int AddAudio(AudioType audioType, AudioClip audioClip, float volume, Transform transform)
        {
            Dictionary<int, AudioSource> audioDic = GetAudioTypeDictionary(audioType);

            AudioSource audio = AddAudioSource(audioType, volume, transform);
            audio.clip = audioClip;
            audioDic.Add(audioID, audio);

            return audioID++;
        }

        private AudioSource AddAudioSource(AudioType audioType, float volume, Transform transform)
        {
            Transform audioTransform = null;
            AudioSource audio = null;
            
            // Set AudioSource Transform
            switch (audioType) {
                case AudioType.Music:
                    audioTransform = musicAudioTransform;
                    break;
                case AudioType.SFX:
                    audioTransform = sfxAudioTransform;
                    break;
            }
            if (transform != null) audioTransform = transform;
            audio = audioTransform.AddComponent<AudioSource>();

            // Sound Setting
            switch (audioType) {
                case AudioType.Music:
                    MusicSetting(audio, volume);
                    break;
                case AudioType.SFX:
                    SFXSetting(audio, volume);
                    break;
            }
            return audio;
        }

        private void MusicSetting(AudioSource audio, float volume)
        {
            audio.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Music")[0];
            audio.volume = volume;
            audio.loop = true;
        }

        private void SFXSetting(AudioSource audio, float volume)
        {
            audio.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
            audio.volume = volume;
        }
        #endregion

        #region Get AudioSource
        private AudioSource GetAudio(AudioType audioType, int audioID)
        {
            Dictionary<int, AudioSource> audioDic = GetAudioTypeDictionary(audioType);

            if (audioDic.ContainsKey(audioID)) {
                return audioDic[audioID];
            }
            else {
                return null;
            }
        }
        #endregion

     #region Get AudioDictionary
        private Dictionary<int, AudioSource> GetAudioTypeDictionary(AudioType audioType)
        {
            Dictionary<int, AudioSource> audioDic = new Dictionary<int, AudioSource>();
            switch (audioType) {
                case AudioType.Music:
                    audioDic = MusicAudioDic;
                    break;
                case AudioType.SFX:
                    audioDic = SFXAudioDic;
                    break;
            }
            return audioDic;
        }
        #endregion

        #region Auto Remove Audio
        private void LateUpdate()
        {
            UpdateAllAudio(MusicAudioDic);
            UpdateAllAudio(SFXAudioDic);
        }

        private void UpdateAllAudio(Dictionary<int, AudioSource> audioDic)
        {
            List<int> keys = new List<int>(audioDic.Keys);
            foreach (int key in keys) {
                AudioSource audio = audioDic[key];

                if (!audio.isPlaying && !applicationPause) {
                    Destroy(audio);
                    audioDic.Remove(key);
                }
            }
        }
        #endregion

        #region Stop All Audio
        private void StopAllAudio(AudioType audioType, float fadeOutTime)
        {
            Dictionary<int, AudioSource> audioDic = GetAudioTypeDictionary(audioType);
            List<int> keys = new List<int>(audioDic.Keys);
            
            StopAllCoroutines();
            foreach (int key in keys) {
                AudioSource audio = audioDic[key];
                if (fadeOutTime > 0) {
                    StartCoroutine(FadeInCoroutine(audio, fadeOutTime));
                }
                else {
                    audio.Pause();
                }
            }
        }
        #endregion

        #region Volume Fade
        IEnumerator FadeInCoroutine(AudioSource audio, float fadeOutTime, Action callBack = null) // Volume Off
        {
            float time = 0f;
            float startVolume = audio.volume;
            
            if (fadeOutTime > 0) {
                while (time <= fadeOutTime) {
                    audio.volume = Mathf.Lerp(startVolume, 0f, time / fadeOutTime);
                    
                    time += Time.deltaTime;
                    yield return null;
                }
            }
            audio.volume = 0f;
            audio.Pause();
            callBack?.Invoke();
            yield break;
        }

        IEnumerator FadeOutCoroutine(AudioSource audio, float fadeOutTime, Action callBack = null)  // Volume On
        {
            float time = 0f;
            float startVolume = audio.volume;

            audio.Play();

            if (fadeOutTime > 0) {
                while (time <= fadeOutTime) {
                    audio.volume = Mathf.Lerp(0f, startVolume, time / fadeOutTime);

                    time += Time.deltaTime;
                    yield return null;
                }
            }
            audio.volume = startVolume;
            callBack?.Invoke();
            yield break;
        }
        #endregion

        #region Volume Slider ( Slider.MinValue => 0.0001 )
        public void MasterVolume(float level) => audioMixer.SetFloat("materVolume", Mathf.Log10(level) * 20f);
        public void MusicVolume(float level) => audioMixer.SetFloat("musicVolume", Mathf.Log10(level) * 20f);
        public void SFXVolume(float level) => audioMixer.SetFloat("sfxVolume", Mathf.Log10(level) * 20f);
        #endregion

        private void OnApplicationPause(bool pause)
        {
            if (pause) {
                applicationPause = true;
            }
            else {
                if (applicationPause) {
                    applicationPause = false;
                }
            }
        }
    }
}