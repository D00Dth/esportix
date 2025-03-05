using UnityEngine;
using LLMUnity;
using TMPro;

public class EsportChatManager : MonoBehaviour
{
    [SerializeField] private LLMCharacter esportBot;
    [SerializeField] private TMP_InputField userInput;
    [SerializeField] private TextMeshProUGUI chatOutput;
    private string pendingResponse;


    void SendMessageToAI()
    {
        string userMessage = userInput.text;
        if (!string.IsNullOrEmpty(userMessage))
        {
            chatOutput.text += "\n[Vous] " + userMessage;
            userInput.text = "";

            _ = esportBot.Chat(userMessage, OnAIResponseReceived, ReplyCompleted);
        }
    }

    void OnAIResponseReceived(string response)
    {
        // Stocke temporairement la réponse (sans l'afficher tout de suite)
        pendingResponse = response;
    }

    void ReplyCompleted()
    {
        // Affiche la réponse complète quand elle est prête
        chatOutput.text += "\n[EsportBot] " + pendingResponse;
    }

}