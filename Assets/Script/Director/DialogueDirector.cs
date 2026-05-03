using UnityEngine;
using TMPro;

public class DialogueDirector : MonoBehaviour
{
    public GameObject panel;
    public RectTransform panelRect;
    public TextMeshProUGUI text;
    public GameObject tutorial;

    [TextArea]
    public string[] messages;

    public Vector2[] panelPositions;

    private int currentIndex;

    void Start()
    {
        currentIndex = 0;

        if (panel != null)
        {
            panel.SetActive(true);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            NextStep();
        }
    }
    public void NextStep()
    {
        currentIndex += 1;

        if (currentIndex >= messages.Length)
        {
            panel.SetActive(false);
            tutorial.SetActive(true);
            return;
        }
        text.text = messages[currentIndex];
        panelRect.anchoredPosition = panelPositions[currentIndex];
    }
}