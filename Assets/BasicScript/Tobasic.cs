using UnityEngine;
using UnityEngine.SceneManagement;
public class Tobasic : MonoBehaviour
{
    public string sceneName;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}