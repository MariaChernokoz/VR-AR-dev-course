using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoonInteraction : MonoBehaviour
{
    private bool isScaled = false;
    private Vector3 originalScale;
    private Vector3 enlargedScale;

    void Start()
    {
        originalScale = transform.localScale;
        enlargedScale = originalScale * 1.5f;
    }

    void Update()
    {
        // Проверка касаний
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began)
            {
                // Создаем луч из камеры
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                
                // Проверяем попадание в объект
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject == gameObject)
                    {
                        ToggleScale();
                    }
                }
            }
        }
    }

    void ToggleScale()
    {
        if (isScaled)
        {
            transform.localScale = originalScale;
        }
        else
        {
            transform.localScale = enlargedScale;
        }
        isScaled = !isScaled;
    }
}