using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField] private UIInventoryItem itemPrefab;

    [SerializeField] private RectTransform contentPanel;
    
    [SerializeField] private UIInventoryDescription itemDescription;

    [SerializeField] private MouseFollower mouseFollower;

    List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

    //Temp Variables
    public Sprite image;
    public int quantity;
    public string title, description;


    private void Awake(){
        Hide();
        mouseFollower.Toogle(false);
        itemDescription.ResetDescription();
        
    }

    public void InitializeInventoryUI(int inventorysize)
    {
        for (int i = 0; i < inventorysize; i++)
        {
            UIInventoryItem uiItem = 
                Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel, false);
            listOfUIItems.Add(uiItem);
            uiItem.OnItemClicked += HandleItemSelection;
            Debug.Log(uiItem);
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMouseBtnClick += HandleShowItemActions;
        }
    }

    private void HandleShowItemActions(UIInventoryItem obj)
    {

    }

    private void HandleEndDrag(UIInventoryItem obj)
    {
        mouseFollower.Toogle(false);
    }

    private void HandleSwap(UIInventoryItem obj)
    {

    }

    private void HandleBeginDrag(UIInventoryItem obj)
    {
        mouseFollower.Toogle(true);
        mouseFollower.SetData(image, quantity);
    }

    private void HandleItemSelection(UIInventoryItem obj)
    {
        itemDescription.setDescription(image, title, description);
        listOfUIItems[0].Select();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        itemDescription.ResetDescription();

        listOfUIItems[0].SetData( image, quantity);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
