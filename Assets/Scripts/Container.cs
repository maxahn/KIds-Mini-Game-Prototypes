using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public GameObject prize;
    [SerializeField]
    private Animator animator;
    private bool isOpened;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        isOpened = false;
    }

    private void OnMouseDown()
    {
        if (!isOpened)
        {
            animator.Play("Open Chest");
            this.isOpened = true; 
        }
    }
}
