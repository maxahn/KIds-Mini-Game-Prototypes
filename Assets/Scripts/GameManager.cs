using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Container[] containers;
    [SerializeField]
    private string jsonFileName;
    // Start is called before the first frame update
    private PromptCollection activePromptCollection;
    private int currentIndex = 0;
    private Sprite[] prizeSprites;

    void Start()
    {
        string json = File.ReadAllText($"{Application.persistentDataPath}/{jsonFileName}.json");
        activePromptCollection = JsonUtility.FromJson<PromptCollection>(json);
        containers = GetComponents<Container>();
        prizeSprites = Resources.LoadAll("Textures", typeof(Texture2D)) as Sprite[];
        PopulateContainers();
    }

    public void PopulateContainers()
    {
        List<Prompt> prompts = activePromptCollection.prompts;
        int prizeIndex = 0;
        int promptIndex = 0;
        for (int i = 0; i < containers.Length; i++)
        {
            int choice = Random.Range(0, 1);
            if (choice == 0 && promptIndex < prompts.Count) // prize
            {
                containers[i].prize.GetComponent<TMP_Text>().text = prompts[promptIndex].text;
                promptIndex += 1;
            } else // prompt
            {
                containers[i].prize.GetComponent<SpriteRenderer>().sprite = prizeSprites[prizeIndex % prizeSprites.Length];
                prizeIndex += 1;
            }

        }
    }
}
