using UnityEngine;

public class ChatVisibility : MonoBehaviour
{
    public Canvas chatCanvas;  // Reference to the Canvas

    private void Start()
    {
        // Initially, ensure the chatbox is hidden
        chatCanvas.gameObject.SetActive(false);
    }

    public void ShowChatbox()
    {
        chatCanvas.gameObject.SetActive(true);
    }
}
