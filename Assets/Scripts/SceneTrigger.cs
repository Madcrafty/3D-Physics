using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    public int sceneIndex;
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(sceneIndex);
        Cursor.lockState = CursorLockMode.None;
    }
    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene(sceneIndex);
        Cursor.lockState = CursorLockMode.None;
    }
}
