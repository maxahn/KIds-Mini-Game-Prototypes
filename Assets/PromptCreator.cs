using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;


public class PromptCreator : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField currentCollectionNameInput, currentPromptInput;
    [SerializeField]
    private Button addPromptButton;
    [SerializeField]
    private GameObject PromptList;
    [SerializeField]
    private GameObject PromptListItemPrefab;

    private List<Prompt> prompts;

    private void Start()
    {
        prompts = new List<Prompt>();
    }

    public void AddToPromptList()
    {
        Prompt prompt = new Prompt(currentPromptInput.text, "", "");
        prompts.Add(prompt);
        GameObject newListItem = Instantiate(PromptListItemPrefab);
        newListItem.transform.SetParent(PromptList.transform);
        newListItem.transform.Find("Prompt Text").GetComponent<TMP_Text>().text = prompt.text;
        currentPromptInput.text = "";
    }

    public void SavePromptCollection()
    {
        PromptCollection promptCollection = new PromptCollection(currentCollectionNameInput.text, prompts, DateTime.Now, DateTime.Now);
        string fileName = currentCollectionNameInput.text;
        string json = JsonUtility.ToJson(promptCollection);
        StreamWriter writer = File.CreateText($"{Application.persistentDataPath}/{fileName}.json");
        writer.Write(json);
        writer.Close();
        Debug.Log("prompt saved: " + json);
    }

    public void RefreshPromptList()
    {
    }

    public void LoadPromptList()
    {
    }
}
