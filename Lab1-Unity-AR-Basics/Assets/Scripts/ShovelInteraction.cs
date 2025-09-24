using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelInteraction : MonoBehaviour
{
    private Renderer shovelRenderer;
    private Color[] colors = { Color.red, Color.blue, Color.green, Color.yellow };
    private int currentColorIndex = 0;

    void Start()
    {
        shovelRenderer = GetComponent<Renderer>();
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
                        ChangeColor();
                    }
                }
            }
        }
    }

    void ChangeColor()
    {
        currentColorIndex = (currentColorIndex + 1) % colors.Length;
        shovelRenderer.material.color = colors[currentColorIndex];
    }
}