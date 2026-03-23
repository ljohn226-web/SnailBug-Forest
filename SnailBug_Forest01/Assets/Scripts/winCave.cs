using UnityEngine;
using UnityEngine.SceneManagement;

public class winCave : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadScene("WinScreen");
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene("WinScreen");
    }
}
