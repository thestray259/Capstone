using System.Collections.Generic;
using BehaviorTree; 

public class CompanionBT : Tree
{
    public UnityEngine.Transform playerTransform; 
    public static float speed = 5.0f; 

    protected override Node SetupTree()
    {
        Node root = new Follow(transform, playerTransform);
        return root; 
    }
}
