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

        public void Play(AudioClip clip, AudioSource source)
        {
            source.clip = clip;
            source.Play();
        }

        public void Play(string clip, AudioSource source)
        {
            AudioClip audioClip = _audios[clip].Clip;
            Play(audioClip, source);
        }

        public void Play(AudioClip[] clips, AudioSource source)
        {
            int random = Random.Range(0, clips.Length);
            Play(clips[random], source);
        }
    }
}