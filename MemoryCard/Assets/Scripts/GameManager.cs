using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform cardContainer;
    [SerializeField] private int gridSizeX = 4;
    [SerializeField] private int gridSizeY = 4;
    
    private Card firstCard;
    private Card secondCard;
    private bool canSelect = true;
    
    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        InitializeGame();
    }
    
    private void InitializeGame()
    {
        CreateCards();
    }
    
    private void CreateCards()
    {
        int pairCount = (gridSizeX * gridSizeY) / 2;
        List<int> cardNumbers = new List<int>();
        
        // Her karttan iki tane olacak şekilde liste oluştur
        for (int i = 0; i < pairCount; i++)
        {
            cardNumbers.Add(i);
            cardNumbers.Add(i);
        }
        
        // Kartları karıştır
        for (int i = 0; i < cardNumbers.Count; i++)
        {
            int temp = cardNumbers[i];
            int randomIndex = Random.Range(i, cardNumbers.Count);
            cardNumbers[i] = cardNumbers[randomIndex];
            cardNumbers[randomIndex] = temp;
        }
        
        // Kartları oluştur
        for (int i = 0; i < cardNumbers.Count; i++)
        {
            GameObject cardObj = Instantiate(cardPrefab, cardContainer);
            Card card = cardObj.GetComponent<Card>();
            card.cardId = cardNumbers[i];
        }
    }
    
    public void CardSelected(Card card)
    {
        if (!canSelect) return;
        
        if (firstCard == null)
        {
            firstCard = card;
        }
        else
        {
            secondCard = card;
            canSelect = false;
            StartCoroutine(CheckMatch());
        }
    }
    
    private IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(1f);
        
        if (firstCard.cardId == secondCard.cardId)
        {
            firstCard.SetMatched();
            secondCard.SetMatched();
        }
        else
        {
            firstCard.ResetCard();
            secondCard.ResetCard();
        }
        
        firstCard = null;
        secondCard = null;
        canSelect = true;
    }
} 