using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SWS;
using UnityEngine.Events;

public class splineObjectScript : MonoBehaviour
{
    public bool isEnd;

    [Space]
    public int stacksToRemove;
    public PathManager path;


    [Space]
    int removedTiles = -1;
    GameObject player;

    [Space]
    public bool moveToEnd = true;
    public bool destroyStackObjects = true;
    [Space]
    public UnityEvent OnLevelComplete;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void RemoveTiles()
    {

        if (isEnd)
        {
            stacksToRemove = player.GetComponent<Stack>().stackObjects.Count;
        }
        float length = WaypointManager.GetPathLength(path.GetPathPoints());
        StartCoroutine(RemoveTilesEnum((length / player.GetComponent<PlayerMovement>().splineMove.speed) / (stacksToRemove + 2)));
    }

    public void StartTimer()
    {
        float length = WaypointManager.GetPathLength(path.GetPathPoints());
        StartCoroutine(Timer(length / player.GetComponent<PlayerMovement>().splineMove.speed + 0.2f)); ;
    }



    public IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        player.transform.rotation = new Quaternion();
    }


    IEnumerator RemoveTilesEnum(float waitingTime)
    {
        var stackScript = player.GetComponent<Stack>();

        if (stackScript.stackObjects.Count > 0)
        {
            while (removedTiles < stacksToRemove)
            {
                yield return new WaitForSeconds(waitingTime);

                if (stackScript.stackObjects.Count>0)
                {
                    removedTiles++;

                    stackScript.stackObjects[0].transform.parent = null;

                    if (!isEnd)
                    {
                        Vector3 midPos = new Vector3(
                        stackScript.stackObjects[0].transform.position.x + Random.Range(-5f, 5f),
                        stackScript.stackObjects[0].transform.position.y + Random.Range(1.5f, 4.5f),
                        stackScript.stackObjects[0].transform.position.z);

                        Vector3 endPos = new Vector3(
                            midPos.x + Random.Range(3f, 5f),
                            midPos.y - 10,
                            midPos.z);

                        Vector3[] path = new Vector3[] { stackScript.stackObjects[0].transform.position, midPos, endPos };

                        stackScript.stackObjects[0].transform.DOPath(path, 1, PathType.CatmullRom);
                        stackScript.stackObjects[0].transform.DORotate(new Vector3(180, 180, 180), 1);

                    }

                    if(destroyStackObjects)
                    {
                        Destroy(stackScript.stackObjects[0], 2.5f);
                    }

                    stackScript.stackObjects.Remove(stackScript.stackObjects[0]);

                    for (int i = 0; i < stackScript.stackObjects.Count; i++)
                    {
                        stackScript.stackObjects[i].transform.position -= new Vector3(0, 0.2f, 0);
                    }

                    stackScript.playerTransform.position -= new Vector3(0, 0.2f, 0);

                    if(moveToEnd)
                    {
                        StopAlongSpline();
                    }

                    stackScript.stackValue--;
                }
            }

            //StartCoroutine(ResetRotation());
        }
    }


    void StopAlongSpline()
    {
        var stackScript = player.GetComponent<Stack>();

        if(isEnd)
        {
            if (stackScript.stackObjects.Count < 1)
            {
                player.transform.DOMove(new Vector3(14.75f, 0.5f, 141.800003f), 4).OnComplete(LevelEnd);
                OnLevelComplete.Invoke();
            }
        }

    }

    void LevelEnd()
    {
        GameObject gobject = GameObject.Find("LevelEndObjects");

        for (int i = 0; i < gobject.transform.childCount; i++)
        {
            gobject.transform.GetChild(i).GetComponent<ParticleSystem>().Play();
        }

        
    }

}
