
// Unity stuff
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI; // Required for UI components
using System.Collections;

public class APIExample : MonoBehaviour
{
    // Defining the UI gameobjects to be easily assigned in the editor
    public InputField guessInput; // Reference to the InputField where the user types the guess
    public Text responseText; // Reference to the UI Text component for responses

    // Don't understand why is this necessary (for safety I guess)
    [System.Serializable]
    public class Item
    {
        public string guess; // The user's guess
    }

    // This method is called when the user presses the "SEND" button
    public void OnSubmit()
    {
        string userGuess = guessInput.text; // Get the user's guess from the InputField
        Debug.Log("button pressed!");
        Debug.Log("your guess is: " + userGuess);
        StartCoroutine(SendGuess(userGuess)); // Send the guess to the server
    }

    // Handles all communication with the API
    private IEnumerator SendGuess(string guess)
    {
        Item newItem = new Item { guess = guess };
        string jsonData = JsonUtility.ToJson(newItem); // Convert to JSON

        // Must define the endpoint url to be what we have defined in our python script
        using (UnityWebRequest webRequest = new UnityWebRequest("http://localhost:8000/api/check_guess", "POST"))
        {

            //Handling JSON file
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json"); // Set content type

            yield return webRequest.SendWebRequest();

            // Handles errors
            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                responseText.text = webRequest.downloadHandler.text; // Update UI text with response
                Debug.Log(webRequest.downloadHandler.text);
            }
        }
    }
}