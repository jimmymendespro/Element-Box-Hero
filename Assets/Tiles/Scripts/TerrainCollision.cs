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

    bool isSand = false;

    void Start() {
        if(tag == "Sand") isSand = true;
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
            else
            {
                GetComponent<MeshRenderer>().material = earthMaterial;
                tag = "Earth";
            }
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
    }
}
