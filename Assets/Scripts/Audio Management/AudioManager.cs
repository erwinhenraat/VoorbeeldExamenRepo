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

        /// <summary>
        /// Plays the specified audio clip on an AudioSource with an optional volume adjustment.
        /// </summary>
        /// <param name="clip">The AudioClip to play.</param>
        /// <param name="source">The AudioSource to use for playback.</param>
        /// <param name="volume">The volume level (default is 1).</param>
        public void Play(AudioClip clip, AudioSource source, float volume = 1)
        {
            source.volume = volume;
            source.clip = clip;
            source.Play();
        }

        /// <summary>
        /// Plays an audio clip by its name (as defined in the AudioFile array) 
        /// on an AudioSource with an optional volume adjustment.
        /// </summary>
        /// <param name="clip">The name of the AudioClip to play.</param>
        /// <param name="source">The AudioSource to use for playback.</param>
        /// <param name="volume">The volume level (default is 1).</param>
        public void Play(string clip, AudioSource source, float volume = 1)
        {
            AudioClip audioClip = _audios[clip].Clip;
            Play(audioClip, source, volume);
        }

        /// <summary>
        /// Plays a random audio clip from an array of AudioClips on an AudioSource with an optional volume adjustment.
        /// </summary>
        /// <param name="clips">An array of AudioClips.</param>
        /// <param name="source">The AudioSource to use for playback.</param>
        /// <param name="volume">The volume level (default is 1).</param>
        public void Play(AudioClip[] clips, AudioSource source, float volume = 1)
        {
            int random = Random.Range(0, clips.Length);
            Play(clips[random], source, volume);
        }
    }
}