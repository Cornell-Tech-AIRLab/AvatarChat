using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LLMUnity;  // Import LLM for Unity

public class ChatController : MonoBehaviour
{
    public GameObject chatPanel;  
    public GameObject textObject; 
    public TMP_InputField chatInput;  
    public LLMCharacter llmCharacter;  // Reference to LLMCharacter for AI replies
    //public Canvas chatCanvas;  // Reference to the Canvas


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
                
                // Use LLMCharacter to handle chatbot response instead of Ollama
                _ = llmCharacter.Chat(userMessage, HandleReply, ReplyCompleted);
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

        // Scroll to bottom after adding a new message
        Canvas.ForceUpdateCanvases();
    }

    // Function to handle the AI's reply
    void HandleReply(string reply)
    {
        SendMessageToChat("Avatar: " + reply);
    }

    // Optional: Called when the AI has completed its response
    void ReplyCompleted()
    {
        Debug.Log("The AI has finished responding.");
    }
}

// Class to represent chat messages
[System.Serializable]
public class Message
{
    public string text;
    public TMP_Text textObject;
}
