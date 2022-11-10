using System.Collections.Generic;
using BehaviorTree; 

public class CompanionBT : Tree
{
    public UnityEngine.Transform playerTransform; 
    public static float speed = 5.0f;
    public static float fovRange = 6f;
    public static float attackRange = 2f;
    public static float damage = 10f; 

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckEnemyInAttackRange(transform), 
                new TaskAttackEnemy(transform)
            }),
            new Sequence(new List<Node>
            {
                new CheckForEnemy(transform),
                new TaskGoToEnemy(transform)
            }),
            new Follow(transform, playerTransform)
        });

        return root; 
    }
}
