using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition; // Reference to the player's transform

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPosition.position; // Set the camera's position to the player's position
    }
}
