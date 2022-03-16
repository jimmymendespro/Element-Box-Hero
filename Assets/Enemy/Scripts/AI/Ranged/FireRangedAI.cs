using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRangedAI : RangedAI
{
    protected override void EngagePlayer()
    {
        if(distanceToPlayer > fireRange)
        {
            GetCloserPlayer();
        }
        else
        {
            if(player.tag == "PlayerFireMode")
            {
                CloseRangeCombat();
            }
            else 
            {
                if(distanceToPlayer > meleeRange)
                {
                    RangedAttack();
                }
                else 
                {
                    CloseRangeCombat();
                }
            }
        }
    }

    private void CloseRangeCombat()
    {
        if (distanceToPlayer > navMeshAgent.stoppingDistance)
        {
            ChasePlayer();
        }
        else
        {
            MeleeAttack();
        }
    }
}