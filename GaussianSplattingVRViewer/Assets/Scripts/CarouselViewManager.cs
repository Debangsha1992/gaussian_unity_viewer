using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarouselViewManager : MonoBehaviour
{
    public RectTransform scrollViewViewport;
    public float maxScale = 1.2f;
    public float minScale = 1.0f;
    public float CenterOffsetMin = 0;
    public float CenterOffsetMax = 0;
    public float scaleSpeed = 5f;
    public List<RectTransform> items = new List<RectTransform>();

    public bool CanScroll = false;

    private void Start()
    {
    }
    private void Update()
    {
        if(!CanScroll)
        return;


        if (scrollViewViewport == null)
        {
            Debug.LogWarning("ScrollViewViewport is not assigned in ScaleCenterItem.");
            return;
        }

        
        foreach (RectTransform item in items)
        {
            // Get the position of the item relative to the viewport
            Vector3 itemViewportPos = scrollViewViewport.InverseTransformPoint(item.position);

            // Check if the item is in the center of the viewport
            float ScaleStart = CenterOffsetMin + (scrollViewViewport.rect.width / 2f);
            float ScaleStop =  (scrollViewViewport.rect.width / 2f) - CenterOffsetMax;
            bool isCentered = Mathf.Abs(itemViewportPos.x) <= ScaleStart && Mathf.Abs(itemViewportPos.x) > ScaleStop;

            // Scale the item based on its centered status
            float targetScale = isCentered ? maxScale : minScale;

            item.localScale = Vector3.Lerp(item.localScale, Vector3.one * targetScale, Time.deltaTime * scaleSpeed);
        }
    }
}

