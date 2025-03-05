using UnityEngine;
using LLMUnity;
using TMPro;

public class EsportChatManager : MonoBehaviour
{
    public LLMCharacter esportBot;
    public TMP_InputField userInput;
    public TextMeshProUGUI chatOutput;
    private string pendingResponse;


    public void SendMessageToAI()
    {
        string userMessage = userInput.text;
        if (!string.IsNullOrEmpty(userMessage))
        {
            chatOutput.text += "\n[Vous] " + userMessage;
            userInput.text = "";

            Debug.Log("Sending message to AI: " + userMessage);  // Add a debug log here

            _ = esportBot.Chat(userMessage, OnAIResponseReceived, ReplyCompleted);
        }
    }

    public void OnAIResponseReceived(string response)
    {
        // Stocke temporairement la réponse (sans l'afficher tout de suite)
        pendingResponse = response;
        Debug.Log("AI response received: " + response);  // Add a debug log here to confirm the response
    }


    public void ReplyCompleted()
    {
        print("here");
        // Affiche la réponse complète quand elle est prête
        if(pendingResponse == null) print("L'ia na rien retourné");
        else print(pendingResponse);
        // chatOutput.text += "\n[EsportBot] " + pendingResponse;
    }

}