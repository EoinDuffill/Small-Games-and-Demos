using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyOpener : MonoBehaviour {

    public bool inRange = false;
    public GameObject key;
    public Inventory inventory;

    void Start()
    {
        // register with the event handler
        inventory.ItemUsed += Inventory_ItemUsed;
    }
    void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        // check if the correct item is in use
        if ((e.item as MonoBehaviour).gameObject == key)
        {
            // check if in range
            if (inRange)
            {
                //gameObject.GetComponent<Door>().Open();
                Debug.Log("OPENING DOOR WITH "+e.item.itemName);
                Animator anim = gameObject.GetComponent<Animator>();
                anim.SetTrigger("Open");
            }
            else
            {
                Debug.Log("OUT OF DOOR RANGE");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        inRange = true;
    }
    private void OnTriggerExit(Collider other)
    {
        inRange = false;
    }
}
