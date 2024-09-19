using UnityEngine;

public class ChatVisibility : MonoBehaviour
{
    public Canvas chatCanvas;

    private void Start()
    {
        chatCanvas.gameObject.SetActive(false);
    }

    public void ShowChatbox()
    {
        chatCanvas.gameObject.SetActive(true);
    }
}
