using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Stack : MonoBehaviour
{
    public float stackValue;
    [Space]
    public GameObject previousTile;
    public GameObject currentTile;
    public GameObject tilePrefab;

    [Space]
    public Transform playerTransform;

    [Space]
    public GameObject playerParent;

    [Range(0, 50)]
    public float stackFallDistance;
    public List<GameObject> stackObjects = new List<GameObject>();

    public UnityEvent onStackCollect;

    //Vector3 initialPosition;


    [ContextMenu("SimpleMove")]
    void MoveForward()
    {
        //playerParent.transform.DOScale(2, 1);
    }

    private void Start()
    {
        //initialPosition = playerParent.transform.position;
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "stackObject")
        {
            Destroy(other.gameObject);

            if (currentTile == null)
            {
                currentTile = Instantiate(tilePrefab, previousTile.transform.position + new Vector3(0, -0.4f, 0), previousTile.transform.rotation);
                stackObjects.Add(currentTile);
                currentTile.transform.parent = playerParent.transform;
                playerTransform.transform.position += new Vector3(0, 0.2f, 0);
            }
            else
            {
                playerParent.GetComponent<PlayerMovement>().currentPlatform.CheckGates();
                previousTile = currentTile;
                currentTile = Instantiate(tilePrefab, previousTile.transform.position + new Vector3(0, stackFallDistance, 0), previousTile.transform.rotation);

                //Vector3 nScale = new Vector3(currentTile.transform.localScale.x + 0.2f, currentTile.transform.localScale.y + 0.2f, currentTile.transform.localScale.z + 0.2f);
                //currentTile.transform.DOPunchScale(nScale, 0.3f);

                stackObjects.Add(currentTile);
                currentTile.transform.parent = playerParent.transform;
                playerTransform.position += new Vector3(0, stackFallDistance, 0);

                onStackCollect.Invoke();
            }
        }

        stackValue = 0;

        for (int i = 0; i < stackObjects.Count; i++)
        {
            float value = stackObjects[i].GetComponent<StackObjectScript>().value;        
            if (stackObjects[i].GetComponent<Collider>().enabled)
            {
                stackObjects[i].GetComponent<Collider>().enabled = false;
                //stackObjects[i].transform.localPosition = new Vector3(0, stackObjects[i].transform.position.y, 0);
            }
            stackValue += value;
        }

    }

}
