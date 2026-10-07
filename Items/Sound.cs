using NAudio.Wave;
using System.IO;

namespace WPFTutorial.Items
{
    public class Sound
    {
        public WaveOutEvent? audioPlayer;
        private WaveFileReader audioFileReader;
        public SoundEnums name;
        public bool repeats = true;


        public Sound(SoundEnums _name, bool _repeats = true)
        {
            name = _name;
            repeats = _repeats;

            // Bundled audio makes launches independent of the working directory.
            var audio = typeof(Sound).Assembly.GetManifestResourceStream($"Audio.{name}.wav")
                ?? throw new FileNotFoundException($"Missing bundled audio: {name}.wav");
            audioFileReader = new WaveFileReader(audio);
        }

        // Play the audio
        public void Play()
        {
            Stop();
            audioPlayer = new WaveOutEvent();
            audioPlayer.PlaybackStopped += Loop;

            audioPlayer.Init(audioFileReader);

            audioFileReader.Position = 0;
            audioPlayer.Play();
        }

        // Stop playing the audio
        public void Stop()
        {
            // Completely delete audioplayer
            if (audioPlayer != null)
            {
                audioPlayer.PlaybackStopped -= Loop;
                audioPlayer.Stop();
                audioPlayer.Dispose();
                audioPlayer = null;
            }
        }

        // Called event to restart audio if it ends
        private void Loop(object? sender, StoppedEventArgs e)
        {
            if (repeats)
            {
                audioFileReader.Position = 0;
                audioPlayer?.Play(); // Play audio if not null
            }
        }
    }

    // When you add a sound file to 'audio', make sure it is a wav and spelled exactly like you put it here
    public enum SoundEnums
    {
        none, // Basically used as a NULL

        // Backgrounds
        coldWind,
        creepyWind,
        thunder1,
        ominous1,
        rainEffect,

        // Sound Effects
        doorSlam1,
        doorSqueak1,
        doorSqueak2,
        doorSqueakLong1,
        rainThunder1,
        stairsWood1,

        // Unused Yet
        lab1,
        lab2,
        stairsWood2,
        stairsWood3,
        stairsWood4,

    }

}
