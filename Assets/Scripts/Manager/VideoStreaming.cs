using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoStreaming : MonoBehaviour
{
    public void PlayVideo(string videoName)
    {
        VideoPlayer vp = GetComponent<VideoPlayer>();

        if (vp == null)
        {
            return;
        }

        string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoName + ".mp4");
        print(videoPath);
        vp.url = videoPath;
        vp.Play();
    }
}
