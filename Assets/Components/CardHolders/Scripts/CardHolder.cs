﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardHolder : MonoBehaviour {

    public GameObject cardObject;
    private Card card;

    public GameObject queryManagerObject;
    private QueryManager queryManager;

    public GameObject coolDownObject;
    private CoolDown coolDown;

    public GameObject cardHolder;
    private Animator cardHolderAnim;

    private bool inputReady = false;
    private bool releaseReady = false;
    
    void Start()
    {
        cardHolderAnim = GetComponent<Animator>();
        card = cardObject.GetComponent<Card>();
        queryManager = queryManagerObject.GetComponent<QueryManager>();
        coolDown = coolDownObject.GetComponent<CoolDown>();
    }

    public void NewCardHolder()
    {
        cardHolder.SetActive(true);
        cardHolderAnim.SetTrigger("newCardHolder");
        NewCard();
        inputReady = true;  // should wait for end of animation
    }

    public void RemoveCardHolder()
    {  
        LaunchCards();
        cardHolderAnim.SetTrigger("removeCardHolder");
        inputReady = false;
    }

    public void CancelAttack()
    {
        card.Cancel();
    }

    public void NewCard()
    {
        card.Init();
    }

    public void UpdateCard(string text)
    {
        // todo add code to check the current card
        releaseReady = queryManager.IsValid(text) && !coolDown.ContainsTag(text);
        card.UpdateText(text, releaseReady);
    }

    public void LaunchCards()
    {
        card.Launch();
    }

    public void ReleaseCards()
    {
        card.Release();
    }

    public bool IsInputReady()
    {
        return inputReady;
    }

    public bool IsReleaseReady() 
    { 
        return releaseReady; 
    }

    public bool IsResetReady()
    {
        return card.IsResetReady();
    }
}