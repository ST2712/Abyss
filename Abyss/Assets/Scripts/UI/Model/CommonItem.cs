using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class CommonItem : ItemSO, IDestroyableItem
    {
        [field: SerializeField] public AudioClip actionSFX { get; private set; }

        public string ActionName => "";

        // Start is called before the first frame update
        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            Debug.Log(ActionName);
            return false;
            
        }
    }

}

