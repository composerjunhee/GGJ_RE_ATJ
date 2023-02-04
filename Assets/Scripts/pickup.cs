using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    public GameObject slotItem;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            inventory inven = collision.GetComponent<inventory>();
            for (int i = 0; i < inven.slots.Count; i++)
            {
                if(inven.slots[i].isEmpty)
                {
                    Instantiate(slotItem, inven.slots[i].slotObj.transform, false);
                    inven.slots[i].isEmpty = false;
                    Debug.Log("gameObject: " + this.gameObject);
                    Destroy(this.gameObject);
                    break;
                }
            }
        }
    }
}
