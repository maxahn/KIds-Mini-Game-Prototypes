using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public GameObject prize, promptCard;
    public bool isPrize { get; set; }
    public string prizePrompt { get; set; }
    private Animator animator;
    private GameManager gameManager;
    private bool isOpened;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        isOpened = false;
        gameManager = GameManager.Instance;
    }

    private void OnMouseDown()
    {
        if (!isOpened && !GameManager.Instance.isPanelOpen)
        {
            animator.Play("Open Chest");
            this.isOpened = true; 
        }
    }

    public void ActivatePrizePrompt()
    {
        if (!isPrize)
        {
            gameManager.ActivatePrizePrompt(prizePrompt);
        }
    }
}
