using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private UIInventoryPage inventoryUI;

    public int inventorySize = 12;

    private void Start(){
        inventoryUI.InitializeInventoryUI(inventorySize);
    }

    public void Update(){
        if(Input.GetKeyDown(KeyCode.I)){
            if(inventoryUI.isActiveAndEnabled == false){
                inventoryUI.Show();
            }else{
            inventoryUI.Hide();   
            }
        }
    }
}