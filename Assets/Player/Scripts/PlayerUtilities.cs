using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUtilities : MonoBehaviour
{
    
    public bool IsPlayerTag(string tag)
    {
        string[] playerTags = {"Player", "PlayerFireMode", "PlayerIceMode", "PlayerElectrikMode", "PlayerWindMode"};
        foreach(string playerTag in playerTags)
        {
            if(tag == playerTag)
            {
                return true;
            }
        }
        return false;
    }

}