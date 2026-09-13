using Unity.VectorGraphics;
using UnityEngine;

public class Checkpoint : MonoBehaviour, IInteractable
{
    public void Interact(Player player)
    {
        SceneController.instance.NextScene();
    }
    
}
