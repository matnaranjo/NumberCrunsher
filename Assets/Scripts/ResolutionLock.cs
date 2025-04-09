using UnityEngine;

public class ResolutionLock : MonoBehaviour
{
    void Start()
    {
        // Force 1920x1080 resolution in fullscreen windowed mode
        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
    }
}