/* Written by Marshall Nystrom for the GMTK Game Jam 2023
 * This script manages background music for the scene.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public List<AudioClip> musicLoops = new List<AudioClip>();
    public int loopToPlay;
    public AudioSource audioSource;
    public bool playingMusic;

    // DEBUG TOOL
    public bool nextTrack;
    public bool prevTrack;

    // Set up internal references, settings, and variables, then begin playing music.
    private void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        audioSource.loop = true;

        if (loopToPlay >= musicLoops.Count)
            loopToPlay = 0;

        audioSource.clip = musicLoops[loopToPlay];
        audioSource.Play();
        playingMusic = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug tools, to change tracks from editor and make sure PrevTrack and NextTrack work
        if (prevTrack)
        {
            PreviousTrack();
            prevTrack = false;
        }

        if(nextTrack)
        {
            NextTrack();
            nextTrack = false;
        }
            
    }

    // Publicly callable through MusicManager.instance.NextTrack(); sets the music player to
    // use the next track in the list, looping to the first if at the end of tracks available.
    // Setting a new track usually stops the music player, so added logic to catch if it's
    // currently playing and keep it playing the new track if it was.
    public void NextTrack()
    {
        bool turnOnAtEnd = false;
        if (playingMusic)
        {
            playingMusic = false;
            turnOnAtEnd = true;
            audioSource.Stop();
        }
        loopToPlay++;
        if (loopToPlay >= musicLoops.Count)
            loopToPlay = 0;
        audioSource.clip = musicLoops[loopToPlay];
        if (turnOnAtEnd == true)
            ToggleMusic();
    }

    // Publicly callable through MusicManager.instance.PreviousTrack(); sets the music player to
    // use the previous track in the list, looping to the last if at the first track.
    // Setting a new track usually stops the music player, so added logic to catch if it's
    // currently playing and keep it playing the new track if it was.
    public void PreviousTrack()
    {
        bool turnOnAtEnd = false;
        if (playingMusic)
        {
            playingMusic = false;
            turnOnAtEnd = true;
            audioSource.Stop();
        }
        loopToPlay--;
        if (loopToPlay < 0)
            loopToPlay = musicLoops.Count - 1;
        audioSource.clip = musicLoops[loopToPlay];
        if (turnOnAtEnd == true)
            ToggleMusic();
    }

    // Publicly callable through MusicManager.instance.ToggleMusic(); toggles the music player
    // playing.
    public void ToggleMusic()
    {
        if(playingMusic)
        {
            audioSource.Pause();
            playingMusic = false;
        }
        else
        {
            audioSource.Play();
            playingMusic = true;
        }

    }
}
