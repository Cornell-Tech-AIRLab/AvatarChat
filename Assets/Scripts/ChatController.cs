using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatController : MonoBehaviour
{
    public GameObject chatPanel;  
    public GameObject textObject; 
    public TMP_InputField chatInput;


    List<Message> messageList = new List<Message>();

    void Update()
    {
        if (chatInput.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SendMessageToChat("Me: " + chatInput.text);
                string userMessage = chatInput.text;
                chatInput.text = "";
                  
            }
        }
        else
        {
            if (!chatInput.isFocused && Input.GetKeyDown(KeyCode.Return))
            {
                chatInput.ActivateInputField();
            }
        }
    }

    // Method to add the player's message to the chat
    public void SendMessageToChat(string text)
    {
        Message newMessage = new Message();
        newMessage.text = text;

        GameObject newText = Instantiate(textObject, chatPanel.transform);
        newMessage.textObject = newText.GetComponent<TMP_Text>();

        newMessage.textObject.text = newMessage.text;

        messageList.Add(newMessage);

        Canvas.ForceUpdateCanvases();
    }

    // Function to handle the AI's reply
    // void HandleReply(string reply)
    // {
    //     Debug.Log("LLM reply works");
    //     SendMessageToChat("Avatar: " + reply);
    // }
}

[System.Serializable]
public class Message
{
    public string text;
    public TMP_Text textObject;
}
