using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip[] audioFiles;

    void Start(){
        audioSource.loop = true;  
        PlayAudio(0);  
    }

    public void PlayAudio(int track){
        if (track >= 0 && track < audioFiles.Length)
        {
            audioSource.clip = audioFiles[track];  
            audioSource.Play();  
        }
    }

    public void PauseMusic(){
        audioSource.Pause();      }

    public void ResumeMusic(){
        audioSource.UnPause();  
    }
}
    

