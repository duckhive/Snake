using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1;
    
    public List<Transform> segments = new List<Transform>();
    
    public int initialSize = 1;
    
    private Vector3 _direction = Vector3.right;

    private void Update()
    {
        if(GameManager.Instance.gameActive && !GameManager.Instance.readyToContinue)
            HandleInput();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.gameActive && !GameManager.Instance.readyToContinue)
        {
            for (int i = segments.Count - 1; i > 0; i--)
            {
                segments[i].position = segments[i - 1].position;
                segments[i].rotation = segments[i - 1].rotation;
            }
        
            transform.position = new Vector3(
                Mathf.Round(transform.position.x) + _direction.x,
                0.5f,
                Mathf.Round(transform.position.z) + _direction.z);
        }
    }

    private void Grow()
    {
        var segment = Instantiate(GameManager.Instance.segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        
        segments.Add(segment);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Food>(out Food food) && GameManager.Instance.gameActive)
        {
            GameManager.Instance.IncreaseScore();
            Grow();
            GameManager.Instance.eatFoodFeedbacks.PlayFeedbacks();
        }

        if (other.TryGetComponent<Obstacle>(out Obstacle wall) && GameManager.Instance.gameActive)
        {
            GameManager.Instance.GameOver();
            GameManager.Instance.dieFeedbacks.PlayFeedbacks();
        }
    }

    private void HandleInput()
    {
        if (_direction == Vector3.forward)
        {
            if (InputManager.Instance.rightInput)
            {
                _direction = Vector3.right;
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            if (InputManager.Instance.leftInput)
            {
                _direction = Vector3.left;
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
        }
        
        else if (_direction == -Vector3.forward)
        {
            if (InputManager.Instance.rightInput)
            {
                _direction = Vector3.left;
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            if (InputManager.Instance.leftInput)
            {
                _direction = Vector3.right;
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
        }

        else if (_direction == Vector3.right)
        {
            if (InputManager.Instance.rightInput)
            {
                _direction = -Vector3.forward;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (InputManager.Instance.leftInput)
            {
                _direction = Vector3.forward;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        
        else if (_direction == Vector3.left)
        {
            if (InputManager.Instance.rightInput)
            {
                _direction = Vector3.forward;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (InputManager.Instance.leftInput)
            {
                _direction = -Vector3.forward;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
}

