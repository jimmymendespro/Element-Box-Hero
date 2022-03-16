using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField] float waitBeforeFall = 0f;
    [SerializeField] float impulseForce = 35;
    [SerializeField] ParticleSystem playerCrushed;

    MeshRenderer[] dropperMesh;

    AudioSource audioSource;

    void Awake() 
    {
        dropperMesh = GetComponentsInChildren<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
        SetMeshActive(false);
    }

    void OnEnable() 
    {
        Invoke("DropperFall", waitBeforeFall);
    }

    void DropperFall()
    {
        SetMeshActive(true);
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().AddForce(Vector3.down * impulseForce, ForceMode.Impulse);
        audioSource.Play();
    }

    void SetMeshActive(bool isActive)
    {
        foreach(MeshRenderer mesh in dropperMesh)
        {
            mesh.enabled = isActive;
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag != "DropperPiece")
        {
            if (other.gameObject.tag == "Player")
            {
                GameObject.FindObjectOfType<PlayerHealth>().TakeDamage(999);
                Instantiate(playerCrushed, other.GetContact(0).point, Quaternion.identity);
            }
            DivideDropperInPieces();
        }
    }

    // Permet de diviser le dropper en pièces et d'ajouter des components à ces pièces.
    void DivideDropperInPieces()
    {
        Transform[] childrenTransformList = GetComponentsInChildren<Transform>();
        for (int i = 1; i < childrenTransformList.Length; i++)
        {
            childrenTransformList[i].parent = transform.parent;
            childrenTransformList[i].gameObject.AddComponent<Rigidbody>();
            childrenTransformList[i].gameObject.AddComponent<DropperPiece>();
            DropperPiecesProjection(childrenTransformList[i]);
        }
    }

    // Provoque une projection des dropper pieces à l'impact
    void DropperPiecesProjection(Transform dropperPiece)
    {
        Rigidbody dropperPieceRigidbody = dropperPiece.gameObject.GetComponent<Rigidbody>();
        dropperPieceRigidbody.mass = 1000f;
        dropperPieceRigidbody.AddExplosionForce(5f, dropperPiece.position, 5f);
    }
}