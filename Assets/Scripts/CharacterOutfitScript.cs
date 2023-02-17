using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterOutfitScript : MonoBehaviour
{
    public Animator animator;
    [Space]
    public Outfit currentOutfit;


    [Space]
    public bool isPositive;

    [Space]
    public List<Outfit> outfits = new List<Outfit>();

    private void Start()
    {
        currentOutfit = GetOutfit("01");
        currentOutfit.ShowOutfit();
    }

    private Outfit GetOutfit(string outfitName)
    {
        Outfit outfit = new Outfit();

        for (int i = 0; i < outfits.Count; i++)
        {
            if(outfits[i].outfitName == outfitName)
            {
                outfit = outfits[i];
                return outfit;
            }
        }

        return outfit;
    }

    float rotationValue = 1000;
    bool isRotating;
    private void Update()
    {
        if(isRotating)
        GetComponent<Stack>().playerTransform.Rotate(0, -rotationValue * Time.deltaTime, 0, Space.World);
    }


    public void StartRotation()
    {
        isRotating = true;
        StartCoroutine(StopRotation());
    }


    IEnumerator StopRotation()
    {
        yield return new WaitForSeconds(1);
        isRotating = false;
        GetComponent<Stack>().playerTransform.DOLocalRotate(new Vector3(), 0.5f);

        SetAnimationTrigger("isSpinning", false);

        if(isPositive)
        {
            SetAnimationTrigger("isHappy", true);
            SetAnimationTrigger("iaSad", false);
        }else
        {
            SetAnimationTrigger("isHappy", false);
            SetAnimationTrigger("iaSad", true);
        }

    }


    public void ChangeCharacterOutfit(string outfitId)
    {
        for (int i = 0; i < outfits.Count; i++)
        {
            outfits[i].HideOutfit();
        }

        GetComponent<Stack>().playerTransform.localPosition = new Vector3(0, GetComponent<Stack>().playerTransform.localPosition.y, 0);

        SetAnimationTrigger("isSpinning", true);
        StartRotation();

        currentOutfit = GetOutfit(outfitId);
        currentOutfit.ShowOutfit();

    }


    public void SetAnimationTrigger(string name, bool value)
    {
        animator.SetBool(name, value);
    }
}

[System.Serializable]
public class Outfit
{
    public string outfitName;
    public GameObject top;
    public GameObject bottom;

    public void HideOutfit()
    {
        top.SetActive(false);
        bottom.SetActive(false);
    }

    public void ShowOutfit()
    {
        top.SetActive(true);
        bottom.SetActive(true);
    }

}