using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _distanceToCenter;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _pickupRange;

    private PlayerController _controller;
    private BaseItem _nearestItem;

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

    public void Interact()
    {
        _nearestItem.OnPickup();
    }

    private void CheckNearbyItems()
    {
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, _pickupRange);
        foreach (Collider2D collider in colliderArray)
        {
            if (collider.TryGetComponent<BaseItem>(out BaseItem item))
            {
                if (_nearestItem == null)
                {
                    _nearestItem = item;
                }
                else
                {
                    if (Vector3.Distance(transform.position, item.transform.position) <
                        Vector3.Distance(transform.position, _nearestItem.transform.position))
                    {
                        _nearestItem = item;
                    }
                }
            }
        }

        if (_nearestItem != null)
        {
            _controller.SetPromptText($"Press E to pick up {_nearestItem.itemData.itemName}");
        }
        else
        {
            _controller.SetPromptText("");
        }
    }

    public void LoadPlayer(PlayerData playerData)
    {
        EncounterSystem.Instance.Player.health = playerData._shipHealth;
        //EncounterSystem.Instance.Player.weapons = playerData._weapons;

        Vector3 position;
        position.x = playerData._position[0];
        position.y = playerData._position[1];
        position.z = playerData._position[2];
        transform.position = position;
    }
}