using UnityEngine;

namespace kmty.gist {
    public class Recorder : MonoBehaviour {

        public int framerate = 60;
        public int maxRecordSeconds = 180;
        public bool recode = false;
        int frameCount;

        void Start() {
            if (recode) StartRecording();
        }

        void StartRecording() {
            System.IO.Directory.CreateDirectory("Capture");
            Time.captureFramerate = framerate;
            frameCount = -1;
        }

        void Update() {
            if (frameCount > 0 && frameCount < framerate * maxRecordSeconds && recode)
                ScreenCapture.CaptureScreenshot($"Capture/frame{frameCount.ToString("0000")}.png");
            frameCount++;
        }
    }
}
