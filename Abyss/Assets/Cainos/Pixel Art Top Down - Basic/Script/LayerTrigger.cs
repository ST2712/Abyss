using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    //when object exit the trigger, put it to the assigned layer and sorting layers
    //used in the stair objects for player to travel between layers
    public class LayerTrigger : MonoBehaviour
    {
        public string layer;
        public string sortingLayer;

        private void OnTriggerExit2D(Collider2D other)
        {
            other.gameObject.layer = LayerMask.NameToLayer(layer);

            other.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayer;
            SpriteRenderer[] srs = other.gameObject.GetComponentsInChildren<SpriteRenderer>();
            foreach ( SpriteRenderer sr in srs)
            {
                if(other.gameObject.name.Equals("Player")){
                    GameObject player = GameObject.Find("Player");
                    player.transform.Find("Weapon").transform.Find("basic_sword").gameObject.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayer;
                }
                sr.sortingLayerName = sortingLayer;
            }
        }

    }
}
