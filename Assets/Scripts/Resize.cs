using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class Resize : MonoBehaviour {

    // Set this to the in-world distance between the left & right edges of your scene.
    public float sceneWidth = 10;

    public Camera cam;

    // Adjust the camera's height so the desired scene width fits in view
    // even if the screen/window size changes dynamically.
    void Update() {
        float unitsPerPixel = sceneWidth / Screen.width;

        float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;

        cam.orthographicSize  = desiredHalfHeight;
    }
}