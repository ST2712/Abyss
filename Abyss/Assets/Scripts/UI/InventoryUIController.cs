using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private UIInventoryPage inventoryUI;

    [SerializeField]
    private Image overlay;

    public int inventorySize = 12;

    private void Start(){
        inventoryUI.InitializeInventoryUI(inventorySize);
        overlay.gameObject.SetActive(false);
    }

    public void Update(){
        if(Input.GetKeyDown(KeyCode.I)){
            if(inventoryUI.isActiveAndEnabled == false){
                inventoryUI.Show();
                overlay.gameObject.SetActive(true);
            }else{
            inventoryUI.Hide();   
            overlay.gameObject.SetActive(false);
            }
        }
    }
}