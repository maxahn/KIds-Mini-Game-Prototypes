using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    public bool isPanelOpen; 
    [SerializeField]
    private GameObject chests, panel;
    [SerializeField]
    private Container[] containers;
    [SerializeField]
    private string jsonFileName;
    // Start is called before the first frame update
    private PromptCollection activePromptCollection;
    [SerializeField]
    private Sprite[] prizeSprites;

    void Start()
    {
        isPanelOpen = true;
        containers = chests.GetComponentsInChildren<Container>();
        prizeSprites = Resources.LoadAll<Sprite>("Toy Images");
    }

    public void StartGame(Prompt[] prompts)
    {
        Debug.Log(prompts.Length);
        PopulateContainers(prompts);
        panel.SetActive(false);
        isPanelOpen = false;
    }


    public void LoadGameFromFile(string fileName)
    {
        // string json = File.ReadAllText($"{Application.persistentDataPath}/{jsonFileName}.json");
        string json = File.ReadAllText($"{Application.persistentDataPath}/{fileName}.json");
        activePromptCollection = JsonUtility.FromJson<PromptCollection>(json);
        PopulateContainers(activePromptCollection.prompts);
    }

    public void PopulateContainers(Prompt[] prompts)
    {
        // List<Prompt> prompts = activePromptCollection.prompts;
        int prizeIndex = 0;
        int promptIndex = 0;
        for (int i = 0; i < containers.Length; i++)
        {
            int choice = Random.Range(0, 2);
            Debug.Log($"random numberl: {choice}"); 
            if (choice == 0 && promptIndex < prompts.Length) // prize
            {
                containers[i].prize.SetActive(false);
                containers[i].promptCard.SetActive(true);
                containers[i].promptCard.GetComponentInChildren<TMP_Text>().text = prompts[promptIndex].text;
                promptIndex += 1;
            } else // prompt
            {
                Sprite sprite = prizeSprites[prizeIndex % prizeSprites.Length];
                Debug.Log($"prizeIndex: {prizeIndex}, promptIndex: {promptIndex}");
                Debug.Log(sprite);
                containers[i].prize.GetComponent<SpriteRenderer>().sprite = sprite;
                prizeIndex += 1;
            }

        }
    }
}
