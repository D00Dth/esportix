using UnityEngine;
using LLMUnity;
using TMPro;
using System.Collections;
using System.Text;
using System.Globalization;


public class EsportChatManager : MonoBehaviour
{
    [SerializeField] private LLMCharacter esportBot;
    [SerializeField] private TMP_InputField userInput;
    private GameObject pendingMessageObj;
    private bool isWaitingForResponse = false;

    [SerializeField] private Transform chatContent;
    [SerializeField] private GameObject messagePrefab;

    public void SendMessageToAI()
    {
        string userMessage = userInput.text;
        if (!string.IsNullOrEmpty(userMessage))
        {
            CreateMessage("<color=#00FF00>[Vous]</color> " + userMessage);
            userInput.text = "";

            pendingMessageObj = CreateMessage("<color=#FFA500>[Expert eSport]</color> .");
            isWaitingForResponse = true;
            StartCoroutine(AnimateDots(pendingMessageObj));

            _ = esportBot.Chat(userMessage, OnAIResponseReceived, ReplyCompleted);
        }
    }

    public void OnAIResponseReceived(string response)
    {
        if (string.IsNullOrWhiteSpace(response)) return;

        isWaitingForResponse = false;
        string sanitizedResponse = RemoveDiacritics(response);

        if (pendingMessageObj == null)
        {
            Debug.LogWarning("pendingMessageObj est null, création d'un nouveau message.");
            pendingMessageObj = CreateMessage("<color=#FFA500>[Expert eSport]</color> ");
        }

        TextMeshProUGUI messageText = pendingMessageObj.GetComponentInChildren<TextMeshProUGUI>();
        messageText.text = "<color=#FFA500>[Expert eSport]</color> " + sanitizedResponse;

    }



    private string RemoveDiacritics(string text)
    {
        if (string.IsNullOrEmpty(text)) return "";

        string normalizedString = text.Normalize(NormalizationForm.FormD);
        StringBuilder stringBuilder = new StringBuilder();

        foreach (char c in normalizedString)
        {
            UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark) stringBuilder.Append(c);
        }
        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }



    public void ReplyCompleted()
    {
        pendingMessageObj = null;
    }

    private GameObject CreateMessage(string text)
    {
        GameObject newMessage = Instantiate(messagePrefab, chatContent);
        TextMeshProUGUI messageText = newMessage.GetComponentInChildren<TextMeshProUGUI>();
        messageText.text = text;
        return newMessage;
    }

    private IEnumerator TypeMessage(GameObject messageObj, string fullText)
    {
        TextMeshProUGUI messageText = messageObj.GetComponentInChildren<TextMeshProUGUI>();
        messageText.text = "";
        print(fullText);
        foreach (char letter in fullText)
        {
            messageText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    private IEnumerator AnimateDots(GameObject messageObj)
    {
        TextMeshProUGUI messageText = messageObj.GetComponentInChildren<TextMeshProUGUI>();
        string baseText = "<color=#FFA500>[Expert eSport]</color> ";
        string[] dots = { ".", "..", "..." };
        int dotIndex = 0;

        while (isWaitingForResponse)
        {
            messageText.text = baseText + dots[dotIndex];
            dotIndex = (dotIndex + 1) % dots.Length;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
