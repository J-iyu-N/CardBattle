using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string nextSceneName;
    public GameObject guideUI;

    private bool canMoveScene;

    void Start()
    {
        if (guideUI != null)
        {
            guideUI.SetActive(false);
        }
    }

    void Update()
    {
        if (canMoveScene == false) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canMoveScene = true;

            if (guideUI != null)
            {
                guideUI.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canMoveScene = false;

            if (guideUI != null)
            {
                guideUI.SetActive(false);
            }
        }
    }
}