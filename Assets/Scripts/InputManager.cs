using System;
using System.Collections;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public bool rightInput;
    public bool leftInput;

    public float screenWidth;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        screenWidth = Screen.width;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.x > screenWidth / 2 && GameManager.Instance.gameActive)
        {
            rightInput = true;
        }
        else
        {
            rightInput = false;
        }
        
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.x < screenWidth / 2 && GameManager.Instance.gameActive)
        {
            leftInput = true;
        }
        else
        {
            leftInput = false;
        }
    }
    
}