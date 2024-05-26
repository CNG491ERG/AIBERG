using UnityEngine;
using UnityEngine.UI;

public class RemainingParkourUI : MonoBehaviour
{
    public Image loadingBar; // The loading bar image
    public RectTransform headImage; // The head image RectTransform
    public float offset = 0; // Offset to adjust the position of the head image

    void Update()
    {
        if (loadingBar != null && headImage != null)
        {
            // Calculate the position of the head image based on the fill amount
            float fillAmount = loadingBar.fillAmount;

            // Get the width of the loading bar's RectTransform
            RectTransform loadingBarRectTransform = loadingBar.GetComponent<RectTransform>();
            float barWidth = loadingBarRectTransform.rect.width;

            // Calculate the new anchored position for the head image
            Vector2 newAnchoredPosition = headImage.anchoredPosition;
            newAnchoredPosition.x = (barWidth * fillAmount) + offset - (barWidth / 2);

            // Update the anchored position of the head image
            headImage.anchoredPosition = newAnchoredPosition;
        }
    }
}
