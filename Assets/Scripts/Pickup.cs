using UnityEngine;


public abstract class Pickup : InteractableObject

{

    public override void Interact(Player player)

    {

        if (player != null)

        {

            OnPickup(player);

        }

        else

        {

            Debug.LogError("Interact called, but Player reference is null!");

        }

    }


    // Abstract method for specific pickup logic

    protected abstract void OnPickup(Player player);

}
