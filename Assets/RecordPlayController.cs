using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordPlayController : MonoBehaviour
{
    [SerializeField]
    private Button playButton, recordButton;
    [SerializeField]
    private Sprite playSprite, stopSprite;

    private Image playButtonImg, recordButtonImg;

    private Color recordColor;
    private Color normalColor;

    private bool isMicConnected;
    private int minFrequency;
    private int maxFrequency;
    private AudioSource audioSource;
    

    void Start()
    {
        recordColor = new Color(219, 152, 152, 255);
        normalColor = new Color(255, 255, 255, 255);
        if (Microphone.devices.Length <= 0)
        {
            Debug.LogWarning("No microphones connected.");
            isMicConnected = false;
            return;
        }
        isMicConnected = true;
        Microphone.GetDeviceCaps(null, out minFrequency, out maxFrequency);
        maxFrequency = (minFrequency == 0 && maxFrequency == 0) ? 44100 : maxFrequency;
        audioSource = this.GetComponent<AudioSource>();
        playButton.onClick.AddListener(PlayAudioClip);
        recordButton.onClick.AddListener(RecordAudioClip);

        playButtonImg = playButton.gameObject.GetComponent<Image>();
        recordButtonImg = recordButton.gameObject.GetComponent<Image>();
    }

    public void RecordAudioClip()
    {
        if (isMicConnected)
        {
            ColorBlock colors = recordButton.colors;
            if (!Microphone.IsRecording(null))
            {
                audioSource.clip = Microphone.Start(null, true, 10, maxFrequency);
                playButton.interactable = false;
                colors.normalColor = recordColor;
            } else
            {
                Microphone.End(null);
                playButton.interactable = true;
                colors.normalColor = normalColor;
            }
        }
    }

    public void PlayAudioClip()
    {
        audioSource.Play();
    }
}
