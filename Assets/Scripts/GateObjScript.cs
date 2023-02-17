using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GateObjScript : MonoBehaviour
{
    public enum GateType { POSITIVE, NEGATIVE};

    public bool isOpen;
    public TextMeshProUGUI text;

    [Space]
    public GameObject triggerObject;
    public float requiredValue;
    public GameObject player;

    [Space]
    public GameObject[] gates;

    [Space]
    public ParticleSystem confettiFX;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //text.text = requiredValue + " $";
        triggerObject.SetActive(false);
    }

    public void CheckPlayer()
    {

        if (player.GetComponent<Stack>().stackValue >= requiredValue)
        {
            if(!isOpen)
            {
                GetComponent<Collider>().enabled = false;
                triggerObject.SetActive(true);

                for (int i = 0; i < gates.Length; i++)
                {
                    gates[i].transform.DOLocalRotate(new Vector3(0, 180, 0), 0.5f).SetEase(Ease.InOutBounce);
                }

                //text.text = "OPEN";
                confettiFX.Play();
                isOpen = true;
            }

        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
