using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class FullScreenFitter : MonoBehaviour
{
    private RectTransform rt;
    private Canvas canvas;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        FitToScreen();
    }

    void FitToScreen()
    {
        float screenRatio = (float)Screen.width / Screen.height;
        float targetRatio = rt.rect.width / rt.rect.height;

        // 1. 앵커는 전체 stretch로
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        // 2. Aspect 유지하면서 꽉 채우기
        float canvasRatio = (float)canvas.pixelRect.width / canvas.pixelRect.height;

        if (screenRatio > targetRatio)
        {
            // 화면이 더 넓음 → X 확장
            float scaleFactor = screenRatio / targetRatio;
            rt.localScale = new Vector3(scaleFactor, 1f, 1f);
        }
        else
        {
            // 화면이 더 길쭉함 → Y 확장
            float scaleFactor = targetRatio / screenRatio;
            rt.localScale = new Vector3(1f, scaleFactor, 1f);
        }
    }

    void Update()
    {
        // 화면 회전 대응
        if (Application.isMobilePlatform)
            FitToScreen();
    }
}
