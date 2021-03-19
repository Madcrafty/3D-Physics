using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    public int sceneIndex;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            SceneManager.LoadScene(sceneIndex);
            Cursor.lockState = CursorLockMode.None;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "Player")
        {
            SceneManager.LoadScene(sceneIndex);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
