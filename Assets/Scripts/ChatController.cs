using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Make sure to include the TextMeshPro namespace

public class ChatController : MonoBehaviour
{
    public GameObject chatBox;  // The chatbox GameObject (chat UI)
    public GameObject textObject;  // Prefab for displaying chat messages
    public TMP_InputField chatInput;  // TMP input field for user input

    void Start()
    {
        // Hide chatbox initially
        chatBox.SetActive(false);
    }

    // Called when user presses enter to send a chat message
    public void OnChatInputSubmit()
    {
        if (!string.IsNullOrEmpty(chatInput.text))
        {
            string userMessage = chatInput.text;
            
            // Clear the input field after sending the message
            chatInput.text = "";  

            // Display user message in the chatbox
            AddMessageToChat("Me: " + userMessage);

            // Simulate avatar response (replace this with an API or LLM call)
            Invoke("DisplayAvatarResponse", 0.5f);  // Simulate a small delay
        }
    }

    // Function to add a new message to the chatbox
    private void AddMessageToChat(string message)
    {
        // Instantiate the textObject prefab under the Content in the ScrollView
        GameObject newTextObject = Instantiate(textObject, chatBox.transform);
        TMP_Text textComponent = newTextObject.GetComponent<TMP_Text>();

        if (textComponent != null)
        {
            textComponent.text = message;  // Set the text to display the message
        }

    }

    // Function to simulate and display the avatar's response in the chat
    private void DisplayAvatarResponse()
    {
        string avatarResponse = "Simulated avatar response";  // Replace with actual logic
        AddMessageToChat("Avatar: " + avatarResponse);
    }

    // Call this method after avatar creation to display the chatbox
    public void ShowChatbox()
    {
        chatBox.SetActive(true);  // Show chatbox panel
    }
}
