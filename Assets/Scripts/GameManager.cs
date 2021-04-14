using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    public bool isPanelOpen; 
    [SerializeField]
    private GameObject chests, backgroundPanel, gameGeneratorPanel, prizePromptPanel;
    [SerializeField]
    private Container[] containers;
    [SerializeField]
    private string jsonFileName;
    // Start is called before the first frame update
    private PromptCollection activePromptCollection;
    [SerializeField]
    private Sprite[] prizeSprites;
    private Animator animator;
    private GameManager gameManager;


    void Start()
    {
        isPanelOpen = true;
        containers = chests.GetComponentsInChildren<Container>();
        prizeSprites = Resources.LoadAll<Sprite>("Toy Images");
        animator = GetComponent<Animator>();
        gameManager = GameManager.Instance;
    }

    public void StartGame(Prompt[] prompts)
    {
        Debug.Log(prompts.Length);
        PopulateContainers(prompts);
        backgroundPanel.SetActive(false);
        gameGeneratorPanel.SetActive(false);
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
            Container container = containers[i];
            if (choice == 0 && promptIndex < prompts.Length) // prompt
            {
                string text = prompts[promptIndex].text;
                container.prize.SetActive(false);
                container.promptCard.SetActive(true);
                container.promptCard.GetComponentInChildren<TMP_Text>().text = text; 
                container.isPrize = false;
                container.prizePrompt = text;
                promptIndex += 1;
            } else // prize
            {
                Sprite sprite = prizeSprites[prizeIndex % prizeSprites.Length];
                container.prize.GetComponent<SpriteRenderer>().sprite = sprite;
                container.isPrize = true;
                prizeIndex += 1;
            }

        }
    }

    public void ActivatePrizePrompt(string prompt)
    {
        prizePromptPanel.GetComponentInChildren<TMP_Text>().text = prompt;
        prizePromptPanel.SetActive(true);
        isPanelOpen = true;
    }

    public void DeactivatePrizePrompt(string prompt)
    {
        prizePromptPanel.SetActive(false);
        isPanelOpen = false;
    }
}
