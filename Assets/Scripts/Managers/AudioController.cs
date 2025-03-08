using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] // SerializeField attribute allows private variables to be visible in the inspector
    AudioSource audioSource;  // Reference to the AudioSource component to control audio playback
    [SerializeField]
    AudioClip[] audioFiles;  // Array of AudioClip to store multiple audio tracks

    // Start is called before the first frame update
    void Start(){
        audioSource.loop = true;  // Enable looping of the audio to automatically repeat when it finishes
        PlayAudio(0);  // Start playing the first audio track in the array at the beginning
    }

    // Play the audio track specified by the given index
    public void PlayAudio(int track){
        if (track >= 0 && track < audioFiles.Length)  // Ensure the track index is within valid range
        {
            audioSource.clip = audioFiles[track];  // Set the audio clip to the selected track
            audioSource.Play();  // Start playing the selected audio track
        }
    }

    // Pause the currently playing audio
    public void PauseMusic(){
        audioSource.Pause();      // Pause the audio playback at its current position
    }

    // Resume playback of the currently paused audio
    public void ResumeMusic(){
        audioSource.UnPause();  // Resume the paused audio track from where it left off
    }
}
