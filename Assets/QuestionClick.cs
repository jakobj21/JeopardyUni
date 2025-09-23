using UnityEngine;
using UnityEngine.UI;

public class JeopardyButton : MonoBehaviour
{
    public int questionIndex;
    public JeopardyManager manager;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => manager.ShowQuestion(questionIndex));
    }
}
