using UnityEngine;
using TMPro; // Assure-toi d'utiliser TextMeshPro pour un meilleur rendu

public class ScoreAnimator : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public float animationDuration = 1f;
    public float punchScale = 1.5f;

    private int displayedScore = 0;
    private int targetScore = 0;
    private float scaleSpeed = 8f;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = scoreText.rectTransform.localScale;
        UpdateText();
    }

    void Update()
    {
        if (displayedScore < targetScore)
        {
            float increment = Mathf.Ceil((targetScore - displayedScore) * Time.deltaTime * 10);
            displayedScore += (int)increment;
            if (displayedScore > targetScore) displayedScore = targetScore;

            UpdateText();
            AnimateScale();
        }

        // Retour progressif à l'échelle normale
        scoreText.rectTransform.localScale = Vector3.Lerp(
            scoreText.rectTransform.localScale,
            originalScale,
            Time.deltaTime * scaleSpeed
        );
    }

    void UpdateText()
    {
        scoreText.text = "Score: " + displayedScore.ToString() +" /100";
    }

    void AnimateScale()
    {
        scoreText.rectTransform.localScale = originalScale * punchScale;
    }

    // Appelle cette méthode pour lancer l'animation de score
    public void AddScore(int amount)
    {
        targetScore += amount;
    }
}
