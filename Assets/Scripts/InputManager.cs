using MoreMountains.Feedbacks;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public bool rightInput;
    public bool leftInput;

    public float screenWidth;

    [SerializeField] MMFeedbacks _inputFeedbacks;

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
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.x > screenWidth / 2 && GameManager.Instance.gameActive
            || (Input.GetButtonDown("Right") && GameManager.Instance.gameActive))
        {
            rightInput = true;
            _inputFeedbacks.PlayFeedbacks();
        }
        else
        {
            rightInput = false;
        }
        
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.x < screenWidth / 2 && GameManager.Instance.gameActive
            || (Input.GetButtonDown("Left") && GameManager.Instance.gameActive))
        {
            leftInput = true;
            _inputFeedbacks.PlayFeedbacks();
        }
        else
        {
            leftInput = false;
        }
    }
    
}