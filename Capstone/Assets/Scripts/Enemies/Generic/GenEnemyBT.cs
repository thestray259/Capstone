using System.Collections;
using System.Collections.Generic;

using BehaviorTree; 

public class GenEnemyBT : Tree
{
    public static float speed = 5.0f;
    public static float fovRange = 6f;
    public static float attackRange = 1f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckEnemyInAttackRange(transform),
                new TaskAttackPlayer(transform)
            }),
            new Sequence(new List<Node>
            {
                new CheckForPlayer(transform),
                new TaskGoToPlayer(transform)
            }),
            new GenEnemyIdle()
        });

        return root; 
    }
}
