using System;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [Serializable]
    private class AmmoSlot
    {
        public AmmoType _ammoType;
        public int _ammoAmount;
    }
    
    [SerializeField] AmmoSlot[] _ammoSlots;

    public int GetCurrentAmmo(AmmoType ammoType)
    {
        AmmoSlot currentAmmoSlot = GetAmmoSlot(ammoType);
        return currentAmmoSlot._ammoAmount;
    }

    public void DecreaseCurrentAmmo(AmmoType ammoType)
    {
        AmmoSlot currentAmmoSlot = GetAmmoSlot(ammoType);
        currentAmmoSlot._ammoAmount--;
    }

    public void IncreaseCurrentAmmo(AmmoType ammoType, int amount)
    {
        AmmoSlot currentAmmoSlot = GetAmmoSlot(ammoType);
        currentAmmoSlot._ammoAmount += amount; 
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach (AmmoSlot slot in _ammoSlots)
        {
            if (slot._ammoType == ammoType)
            {
                return slot;
            }
        }

        return null;
    }
}
