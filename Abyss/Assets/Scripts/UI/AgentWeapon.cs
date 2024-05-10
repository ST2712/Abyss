using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    [SerializeField] private EquipableItemSO weapon;
    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private List<ItemParameter> parametersToModify, itemCurrentState;

    public void SetWeapon(EquipableItemSO weaponItemSO, List<ItemParameter> itemState)
    {
        if( weapon != null)
        {
            inventoryData.AddItems(weapon, 1, itemCurrentState);
        }
        this.weapon = weaponItemSO;
        this.itemCurrentState = new List<ItemParameter>(itemState);
        ModifyParameers();
    }

    public void ModifyParameers()
    {
        foreach(var parameter in parametersToModify)
        {
            int index = itemCurrentState.IndexOf(parameter);
            float newValue = itemCurrentState[index].value + parameter.value;
            itemCurrentState[index] = new ItemParameter
            {
                itemParameter = parameter.itemParameter,
                value = newValue
            };
        }
    }
}
