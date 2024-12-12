using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public string _shipName;
    public HealthPools _shipHealth;
    //public List<WeaponSlot> _weapons;
    public float[] _position;

    //public Inventory _inventory;

    public PlayerData(Player player)
    {
        _shipName = player.GetComponent<Ship>().shipName;
        _shipHealth = EncounterSystem.Instance.Player.health;
        //_weapons = EncounterSystem.Instance.Player.weapons;

        _position = new float[3];
        _position[0] = player.transform.position.x;
        _position[1] = player.transform.position.y;
        _position[2] = player.transform.position.z;

        //todo: save inventory data
    }
}
