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

        // 1. ��Ŀ�� ��ü stretch��
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        // 2. Aspect �����ϸ鼭 �� ä���
        float canvasRatio = (float)canvas.pixelRect.width / canvas.pixelRect.height;

        if (screenRatio > targetRatio)
        {
            // ȭ���� �� ���� �� X Ȯ��
            float scaleFactor = screenRatio / targetRatio;
            rt.localScale = new Vector3(scaleFactor, 1f, 1f);
        }
        else
        {
            // ȭ���� �� ������ �� Y Ȯ��
            float scaleFactor = targetRatio / screenRatio;
            rt.localScale = new Vector3(1f, scaleFactor, 1f);
        }
    }

    void Update()
    {
        // ȭ�� ȸ�� ����
        if (Application.isMobilePlatform)
            FitToScreen();
    }
}
