using Inventory.UI;
using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private UIInventoryPage inventoryUI;

        [SerializeField] private InventorySO inventoryData;

        [SerializeField] private Image overlay;

        public List<InventoryItem> initialItems = new List<InventoryItem>();

        [SerializeField] private AudioSource audioSource;

        [SerializeField] private AudioClip dropClip;
        private GameObject player;

        private void Start()
        {
            PrepareUI();
            overlay.gameObject.SetActive(false);
            PrepareInventoryData();
            player = GameObject.Find("Player");
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty)
                    continue;
                inventoryData.AddItem(item);

            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }

        private void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            /*IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                inventoryUI.ShowItemAction(itemIndex);
                inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
            }
            else if (itemAction.ActionName != "")
            {
                inventoryUI.ShowItemAction(itemIndex);
            }*/

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryUI.ShowItemAction(itemIndex);
                IItemAction itemAction = inventoryItem.item as IItemAction;
                if (itemAction != null)
                {
                    inventoryUI.ShowItemAction(itemIndex);
                    inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
                }
                inventoryUI.AddAction("Tirar", () => DropItem(itemIndex, inventoryItem.quantity));
            }


        }

        private void DropItem(int itemIndex, int quantity)
        {
            inventoryData.RemoveItem(itemIndex, quantity);
            inventoryUI.ResetSelection();

            if (audioSource != null && dropClip != null)
            {
                audioSource.PlayOneShot(dropClip);
                Debug.Log("" + audioSource.name + dropClip.name);
            }
            else
            {
                Debug.LogError("AudioSource or dropClip is null!");
            }
        }
        public void PerformAction(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;

            if (destroyableItem != null)
            {
                inventoryData.RemoveItem(itemIndex, 1);
            }

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(gameObject, inventoryItem.itemState);
                audioSource.PlayOneShot(itemAction.actionSFX);
                if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                    inventoryUI.ResetSelection();
            }
        }

        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }
        private void HandleSwapItems(int itemIndex1, int itemIndex2)
        {
            inventoryData.SwapItems(itemIndex1, itemIndex2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }

            ItemSO item = inventoryItem.item;
            string description = PrepareDescription(inventoryItem);
            inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.Name, description);
        }

        private string PrepareDescription(InventoryItem inventoryItem)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < inventoryItem.itemState.Count; i++)
            {
                sb.Append($"{inventoryItem.itemState[i].itemParameter.ParameterName}" +
                        $": {inventoryItem.itemState[i].value} / {inventoryItem.item.DefaultParametersList[i].value}");
                sb.AppendLine();

            }
            sb.AppendLine();
            sb.Append(inventoryItem.item.Description);

            return sb.ToString();

        }


        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                ReAsignPlayerScripts();
                if (inventoryUI.isActiveAndEnabled == false)
                {
                    Time.timeScale = 0f;
                    inventoryUI.Show();
                    overlay.gameObject.SetActive(true);
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                    }
                }
                else
                {
                    Time.timeScale = 1f;
                    inventoryUI.Hide();
                    overlay.gameObject.SetActive(false);
                }
            }
        }

        private void ReAsignPlayerScripts()
        {
            if (player == null)
            {
                player = GameObject.Find("Player");
                player.GetComponent<TopDownMovement>().enabled = !player.GetComponent<TopDownMovement>().enabled;
                player.GetComponent<CombatScript>().enabled = !player.GetComponent<CombatScript>().enabled;
                player.GetComponent<Animator>().SetFloat("xLast", 0);
                player.GetComponent<Animator>().SetFloat("yLast", -1);
            }
            else
            {
                player.GetComponent<TopDownMovement>().enabled = !player.GetComponent<TopDownMovement>().enabled;
                player.GetComponent<CombatScript>().enabled = !player.GetComponent<CombatScript>().enabled;
                player.GetComponent<Animator>().SetFloat("xLast", 0);
                player.GetComponent<Animator>().SetFloat("yLast", -1);
            }
        }
    }
}