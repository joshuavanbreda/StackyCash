using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScalingObjects : MonoBehaviour
{
    public List< ScaleObjects> objects =  new List<ScaleObjects>();
    public float scaleFactor;
    public ParticleSystem particle;



    private void Start()
    {
        int childrenCount = transform.childCount;

        for (int i = 0; i < childrenCount; i++)
        {
            ScaleObjects scaleObjects = new ScaleObjects();
            objects.Add(scaleObjects);
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            objects[i].objct = transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].targetScale = objects[i].objct.transform.localScale;
            objects[i].objct.transform.localScale = new Vector3();
        }

    }

    [ContextMenu("Test")]
    void Test()
    {
        ScaleObjects();
    }


    public void ScaleObjects()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].objct.transform.DOScale(objects[i].targetScale, i * scaleFactor ).SetEase(Ease.InOutBounce);
        }
    }


}

[System.Serializable]
public class ScaleObjects
{
    public GameObject objct;
    public Vector3 targetScale;
}