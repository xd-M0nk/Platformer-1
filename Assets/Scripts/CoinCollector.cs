using UnityEngine;
using TMPro;

public class CoinCollector : MonoBehaviour
{
    [Header("UI Settings")]
    [Tooltip("TextMeshPro object to display the coin count.")]
    public TextMeshProUGUI coinText;

    private int coinCount = 0;

    private void Start()
    {
        UpdateCoinText();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered the trigger is tagged as "Coin"
        if (other.CompareTag("Coin"))
        {
            // Increment the coin count
            coinCount++;

            // Update the coin count display
            UpdateCoinText();

            // Destroy the coin object
            Destroy(other.gameObject);
        }
    }

    private void UpdateCoinText()
    {
        // Update the TextMeshPro text to display the current coin count
        coinText.text = $"Coin - {coinCount}";
    }
}
