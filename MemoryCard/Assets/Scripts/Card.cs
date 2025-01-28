using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Sprite frontSprite;  // Kartın ön yüzü
    [SerializeField] private Sprite backSprite;   // Kartın arka yüzü
    [SerializeField] private Image cardImage;     // Kartın Image komponenti
    
    private bool isFlipped = false;   // Kart çevrildi mi?
    private bool isMatched = false;   // Kart eşleşti mi?
    public int cardId;                // Kartın kimlik numarası
    
    private void Start()
    {
        // Başlangıçta kartın arka yüzü görünür
        cardImage.sprite = backSprite;
    }

    public void FlipCard()
    {
        if (!isFlipped && !isMatched)
        {
            // Kartı çevir
            isFlipped = true;
            cardImage.sprite = frontSprite;
            
            // GameManager'a bu kartın seçildiğini bildir
            GameManager.Instance.CardSelected(this);
        }
    }

    public void SetMatched()
    {
        isMatched = true;
    }

    public void ResetCard()
    {
        isFlipped = false;
        cardImage.sprite = backSprite;
    }
} 