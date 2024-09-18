using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class ChatController : MonoBehaviour
{
    public GameObject chatPanel;  
    public GameObject textObject; 
    public TMP_InputField chatInput;  
    List<Message> messageList = new List<Message>();

    private string ollamaUrl = "http://localhost:11434/api/ask";  // Adjust if necessary

    void Update()
    {
        if (chatInput.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SendMessageToChat("Me: " + chatInput.text);
                StartCoroutine(GetAvatarResponse(chatInput.text));  // Call the Ollama API here
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

    // Coroutine to send a message to Ollama and get a response
    IEnumerator GetAvatarResponse(string userMessage)
    {
        // Setup the request payload
        var requestData = new { model = "llama2", prompt = userMessage };
        string jsonData = JsonUtility.ToJson(requestData);

        using (UnityWebRequest request = new UnityWebRequest(ollamaUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // Send the request and wait for a response
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.error);
                SendMessageToChat("Avatar: Sorry, something went wrong.");
            }
            else
            {
                // Parse the response
                string jsonResponse = request.downloadHandler.text;
                OllamaResponse response = JsonUtility.FromJson<OllamaResponse>(jsonResponse);

                // Display the avatar's response in chat
                SendMessageToChat("Avatar: " + response.response);
            }
        }
    }
}

// Class to hold Ollama's response
[System.Serializable]
public class OllamaResponse
{
    public string response;
}

[System.Serializable]
public class Message
{
    public string text;
    public TMP_Text textObject;
}
