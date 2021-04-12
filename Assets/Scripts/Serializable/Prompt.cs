using UnityEngine;

[System.Serializable]
public class Prompt
{
    public string text;
    public string audioExamplePath;
    public string recordedAudioPath;

    public Prompt(string promptText, string promptAudioExamplePath, string promptRecordedAuthioPath)
    {
        text = promptText;
        audioExamplePath = promptAudioExamplePath;
        recordedAudioPath = promptRecordedAuthioPath;
    }
}
