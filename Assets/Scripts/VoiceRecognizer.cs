using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;

public class VoiceRecognizer : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    void Start()
    {
        keywords.Add("eins", () => NumberRecognized(1));
        keywords.Add("zwei", () => NumberRecognized(2));
        keywords.Add("drei", () => NumberRecognized(3));
        keywords.Add("vier", () => NumberRecognized(4));
        keywords.Add("fünf", () => NumberRecognized(5));

        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        keywordRecognizer.OnPhraseRecognized += OnPhraseRecognized;
        keywordRecognizer.Start();

        Debug.Log("Spracherkennung gestartet.");
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Erkannt: " + args.text);

        if (keywords.TryGetValue(args.text, out var action))
        {
            action.Invoke();
        }
    }

    private void NumberRecognized(int number)
    {
        Debug.Log("Der Spieler sagte: " + number);

        // Hier kannst du später deine Spiellogik einbauen.
    }

    private void OnDestroy()
    {
        if (keywordRecognizer != null)
        {
            if (keywordRecognizer.IsRunning)
                keywordRecognizer.Stop();

            keywordRecognizer.Dispose();
        }
    }
}