using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    [Header("Scene Settings")]
    [Tooltip("Name of the scene to load when the player touches this teleporter.")]
    public string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered the trigger has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Ensure the scene name is valid
            if (!string.IsNullOrEmpty(sceneToLoad))
            {
                // Load the specified scene
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.LogWarning("Scene name is not set in the Teleporter script.");
            }
        }
    }
}
