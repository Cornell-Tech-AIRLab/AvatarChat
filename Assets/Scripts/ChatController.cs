using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using TMPro;  // Make sure to include the TextMeshPro namespace

public class ChatController : MonoBehaviour
{
    public TMP_InputField chatInput;  // TMP input field for user input
    public TMP_Text chatHistory;  // TMP text for chat history
    public GameObject chatBox;  // Chatbox panel (chat UI)

    // Simulated avatar response (replace with LLM or other logic)
    private string SimulatedAvatarResponse(string userMessage)
    {
        // Replace this with LLM or actual response logic
        return "Simulated avatar response";
    }

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
            // Display user message in the chat history
            chatHistory.text += $"\nMe: {userMessage}";
            chatInput.text = "";  // Clear the input field

            // Simulate avatar response (replace this with LLM or API call)
            string avatarResponse = SimulatedAvatarResponse(userMessage);
            Invoke("DisplayAvatarResponse", 0.5f);  // Simulate a small delay
        }
    }

    // Function to display the avatar's response in the chat
    private void DisplayAvatarResponse()
    {
        string avatarResponse = SimulatedAvatarResponse(chatInput.text);
        chatHistory.text += $"\nAvatar: {avatarResponse}";
    }

    // Call this method after avatar creation to display the chatbox
    public void ShowChatbox()
    {
        chatBox.SetActive(true);  // Show chatbox panel
    }
}

