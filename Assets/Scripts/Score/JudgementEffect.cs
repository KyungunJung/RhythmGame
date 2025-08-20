using UnityEngine;
using UnityEngine.UI;

public class JudgementEffect : MonoBehaviour
{
    public Text text; // or TextMeshProUGUI
    public float lifetime = 0.6f;
    public Vector2 moveUp = new Vector2(0, 40);

    private Vector2 startPos;
    private Color startColor;
    private float timer = 0f;

    void Start()
    {
        startPos = ((RectTransform)transform).anchoredPosition;
        startColor = text.color;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float t = timer / lifetime;

        // Move Up
        ((RectTransform)transform).anchoredPosition = Vector2.Lerp(startPos, startPos + moveUp, t);

        // Fade Out
        Color c = startColor;
        c.a = Mathf.Lerp(1f, 0f, t);
        text.color = c;

        if (t >= 1f)
            Destroy(gameObject);
    }
}
