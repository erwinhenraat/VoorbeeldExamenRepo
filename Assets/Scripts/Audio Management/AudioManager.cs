using MarkUlrich.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace UntitledCube.AudioManagement
{
    public class AudioManager : SingletonInstance<AudioManager>
    {
        [SerializeField] private AudioFile[] _audioFiles;
        private readonly Dictionary<string, AudioFile> _audios = new();

        private void Awake()
        {
            foreach (AudioFile audio in _audioFiles)
                _audios.Add(audio.Name, audio);
        }

        public void Play(AudioClip clip, AudioSource source, float volume = 1)
        {
            source.volume = volume;
            source.clip = clip;
            source.Play();
        }

        public void Play(string clip, AudioSource source, float volume = 1)
        {
            AudioClip audioClip = _audios[clip].Clip;
            Play(audioClip, source, volume);
        }

        public void Play(AudioClip[] clips, AudioSource source, float volume = 1)
        {
            int random = Random.Range(0, clips.Length);
            Play(clips[random], source, volume);
        }
    }
}