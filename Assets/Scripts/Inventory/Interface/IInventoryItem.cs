using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryComponent
{
    public Sprite GetImage();
    public int GetID();
    public int GetMaxStackSize();
    public int GetAmount();
}
