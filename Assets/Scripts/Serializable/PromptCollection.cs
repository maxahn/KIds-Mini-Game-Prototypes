using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PromptCollection {
    public string name;
    public Prompt[] prompts;
    public DateTime dateTimeCreated;
    public DateTime lastTimeChanged;

    public PromptCollection(string promptName, Prompt[] promptsList, DateTime promptDateTimeCreated , DateTime promptLastTimeChanged)
    {
        name = promptName;
        prompts = promptsList;
        dateTimeCreated = promptDateTimeCreated;
        lastTimeChanged = promptLastTimeChanged;
    }
}
