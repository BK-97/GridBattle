using UnityEngine;
using System.IO;

public class ScreenshotTool : MonoBehaviour
{
    public KeyCode screenshotKey = KeyCode.F12; // Ayarlanabilir tuþ
    public string screenshotFolder = "Screenshots";

    void Update()
    {
        if (Input.GetKeyDown(screenshotKey))
        {
            TakeScreenshot();
        }
    }

    private void TakeScreenshot()
    {
        if (!Directory.Exists(Path.Combine(Application.dataPath, "..", screenshotFolder)))
        {
            Directory.CreateDirectory(Path.Combine(Application.dataPath, "..", screenshotFolder));
        }

        string filename = Path.Combine(Application.dataPath, "..", screenshotFolder, "screenshot_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png");
        ScreenCapture.CaptureScreenshot(filename);
        Debug.Log("Screenshot alýndý: " + filename);
    }
}
