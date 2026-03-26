using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    public override void Interact(Player player)
    {
        if (kitchenObject == null)
        {
            //Spawn the kitchen object on a counter
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else
        {
            //Give the object to the player
            kitchenObject.SetKitchenObjectParent(player);
        }
    }

    
}
