using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCollision : MonoBehaviour
{
    [SerializeField] Material currentMaterial;
    [SerializeField] Material earthMaterial;
    [SerializeField] Material snowMaterial;
    [SerializeField] Material waterMaterial;
    [SerializeField] Material iceMaterial;
    [SerializeField] Material sandMaterial;
    [SerializeField] Material stoneMaterial;

    bool isSand = false;
    bool isStone = false;

    void Start() {
        if(tag == "Sand") isSand = true;
        else if(tag == "Stone") isStone = true;
    }

    void OnCollisionEnter(Collision other) {
        switch(tag)
        {
            case "Grass":
                CollisionWithGrass(other);
            break;
            case "Earth":
                CollisionWithEarth(other);
            break;
            case "Sand":
                CollisionWithSand(other);
            break;
            case "Snow":
                CollisionWithSnow(other);
            break;
            case "Ice":
                CollisionWithIce(other);
            break;
            case "Lava":
                CollisionWithLava(other);
            break;
            case "Stone":
                CollisionWithStone(other);
            break;
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        TriggeringWater(other);
    }

    void CollisionWithGrass(Collision other)
    {
        if (other.gameObject.tag == "PlayerFireMode")
        {
            GetComponent<MeshRenderer>().material = earthMaterial;
            tag = "Earth";
        }
        else if (other.gameObject.tag == "PlayerIceMode")
        {
            GetComponent<MeshRenderer>().material = snowMaterial;
            tag = "Snow";
        }
    }

    void CollisionWithEarth(Collision other)
    {
        if (other.gameObject.tag == "PlayerIceMode")
        {
            GetComponent<MeshRenderer>().material = snowMaterial;
            tag = "Snow";
        }
    }

    void CollisionWithSand(Collision other)
    {
        if (other.gameObject.tag == "PlayerIceMode")
        {
            GetComponent<MeshRenderer>().material = snowMaterial;
            tag = "Snow";
        }
    }

    void CollisionWithSnow(Collision other)
    {
        if (other.gameObject.tag == "PlayerFireMode")
        {
            if(isSand)
            {
                GetComponent<MeshRenderer>().material = sandMaterial;
                tag = "Sand";
            }
            else if(isStone)
            {
                GetComponent<MeshRenderer>().material = stoneMaterial;
                tag = "Stone";
            }
            else
            {
                GetComponent<MeshRenderer>().material = earthMaterial;
                tag = "Earth";
            }
        }
        else if(other.gameObject.tag == "PlayerIceMode")
        {
            // To do : Reload Faster
        }
        else if(other.gameObject.tag == "Player" || other.gameObject.tag == "PlayerElectrikMode")
        {
            // To do : slow down then speed up
            // DecreasePlayerSpeed(other.gameObject);
        }
    }

    void CollisionWithIce(Collision other)
    {
        if (other.gameObject.tag == "PlayerFireMode")
        {
            GetComponent<MeshRenderer>().material = waterMaterial;
            GetComponent<BoxCollider>().isTrigger = true;
            tag = "Water";
        }
        else if(other.gameObject.tag == "Player" || other.gameObject.tag == "PlayerElectrikMode")
        {
            // To do : Add slipery effect
        }
    }

    void TriggeringWater(Collider other)
    {
        if (other.gameObject.tag == "PlayerIceMode")
        {
            GetComponent<MeshRenderer>().material = iceMaterial;
            GetComponent<BoxCollider>().isTrigger = false;
            tag = "Ice";
        }
    }

    void CollisionWithLava(Collision other)
    {
        if (other.gameObject.tag == "PlayerIceMode")
        {
            GetComponent<MeshRenderer>().material = snowMaterial;
            tag = "Snow";
        }
        else if(other.gameObject.tag == "PlayerWindMode")
        {
            // To do : BurningDamages   
        }
        else if(other.gameObject.tag == "PlayerFireMode")
        {
            // To do : Reload Faster
        }
        else if(other.gameObject.tag == "Player" || other.gameObject.tag == "PlayerElectrikMode")
        {
            // To do : else if -> else && Game Over 
        }
    }

    void CollisionWithStone(Collision other)
    {
        if (other.gameObject.tag == "PlayerIceMode")
        {
            GetComponent<MeshRenderer>().material = snowMaterial;
            tag = "Snow";
        }
    }

    void OnParticleCollision(GameObject other) 
    {
        if(other.tag == "IceDome") 
        {
            switch(tag)
            {
                case "Grass": 
                case "Earth":
                case "Sand":
                case "Lava":
                case "Stone":
                    GetComponent<MeshRenderer>().material = snowMaterial;
                    tag = "Snow";
                break;
            }
        }
    }

    void DecreasePlayerSpeed(GameObject gameObject)
    {
       gameObject.GetComponentInParent<PlayerMovement>().SetPlayerSpeedToHalf();
    }

    void RestaurePlayerSpeed(GameObject gameObject)
    {
        gameObject.GetComponentInParent<PlayerMovement>().SetPlayerSpeedToMax();
    }
    
}