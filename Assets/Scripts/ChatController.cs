using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;

public class ChatController : MonoBehaviour
{
    public GameObject chatPanel;  
    public GameObject textObject; 
    public TMP_InputField chatInput;

    private const string url = "http://localhost:11434/api/chat";
    List<Message> messageList = new List<Message>();

    void Update()
    {
        if (chatInput.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                string userMessage = chatInput.text;
                SendMessageToChat("Me: " + userMessage);
                chatInput.text = "";
                SendMessageToApi(userMessage); // Call API here
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

    // Method to send user input to the Llama API
    private void SendMessageToApi(string userContent)
    {
        StartCoroutine(PostRequest(userContent));
    }

    private IEnumerator PostRequest(string userContent)
    {
        // Create the request data
        var requestData = new ApiRequest
        {
            model = "llama3.2",
            messages = new ApiMessage[]
            {
                new ApiMessage { role = "system", content = "you are a salty pirate" },
                new ApiMessage { role = "user", content = userContent }
            },
            stream = false
        };

        // Convert the request data to JSON
        string json = JsonUtility.ToJson(requestData);

        using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            // Send the request and wait for a response
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {www.error}");
            }
            else
            {
                // Parse the response
                ApiResponse response = JsonUtility.FromJson<ApiResponse>(www.downloadHandler.text);
                string assistantMessage = response.message.content;

                // Display the assistant's response in the chat
                SendMessageToChat("Bot: " + assistantMessage);
            }
        }
    }
}

[System.Serializable]
public class Message
{
    public string text;
    public TMP_Text textObject;
}

[System.Serializable]
public class ApiMessage
{
    public string role;
    public string content;
}

[System.Serializable]
public class ApiRequest
{
    public string model;
    public ApiMessage[] messages;
    public bool stream;
}

// Class to parse the response
[System.Serializable]
public class ApiResponse
{
    public string model;
    public string created_at;
    public ApiMessage message; // Use ApiMessage here for the assistant's message
    public string done_reason;
    public bool done;
    public long total_duration;
    public long load_duration;
    public int prompt_eval_count;
    public long prompt_eval_duration;
    public int eval_count;
    public long eval_duration;
}
