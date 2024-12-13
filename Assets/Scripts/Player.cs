using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _distanceToCenter;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _pickupRange;
    [SerializeField] private LayerMask _interactableMask;

    private PlayerController _controller;
    private GameObject _nearestObject;

    

    void Start()
    {
        _controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        GetDistanceToCenter();
        CheckNearbyItems();
    }

    public float GetDistanceToCenter()
    {
        return Vector3.Distance(transform.position, Vector3.zero);
    }

    public void Shoot()
    {
        Instantiate(_projectilePrefab, transform.position, transform.rotation);
    }

    public PlayerController GetController()
    {
        return _controller;
    }

    public void Interact()
    {
        if (_nearestObject != null)
        {
            _nearestObject.GetComponent<IInteractable>().Interact(this);
        }
    }

    private void CheckNearbyItems()
    {
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, _pickupRange, _interactableMask);

        foreach (Collider2D collider in colliderArray)
        {
            #region Get Nearest Object

            if (_nearestObject == null)
            {
                _nearestObject = collider.gameObject;
            }
            else
            {
                if (Vector3.Distance(transform.position, collider.transform.position) <
                    Vector3.Distance(transform.position, _nearestObject.transform.position))
                {
                    _nearestObject = collider.gameObject;
                }
            }

            #endregion
        }

        if (colliderArray.Length == 0)
        {
            _nearestObject = null;
            _controller.SetPromptText("");
            return;
        }

        _controller.SetPromptText(_nearestObject.GetComponent<IInteractable>().GetPrompt());
        
    }
}