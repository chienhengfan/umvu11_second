using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvent : MonoBehaviour
{
    private List<GameObject> meteoriteList;
    private int numberMeteorite = 7;
    private int currentNum = 0;
    public GameObject meteorite;

    [SerializeField]
    private float evokeRange = 3f;

    private Transform evokePosition;
    private float evokeHieght = 0f;
    private void Start()
    {
        meteoriteList = new List<GameObject>();

        for(int i = 0; i < numberMeteorite; i++)
        {
            meteoriteList[i] = Instantiate(meteorite);
            meteoriteList[i].gameObject.SetActive(false);
        }
    }
    public void Me01()
    {
        currentNum = currentNum % numberMeteorite;
        evokePosition.position = Random.insideUnitCircle * evokeRange;
        evokePosition.position += new Vector3(0, evokeHieght, 0);
        meteoriteList[currentNum].transform.position = evokePosition.position;
        meteoriteList[currentNum].transform.forward = -evokePosition.up;
        meteoriteList[currentNum].gameObject.SetActive(true);
        currentNum++;
    }

    public void Me02()
    {
        currentNum = currentNum % numberMeteorite;
        evokePosition.position = Random.insideUnitCircle * evokeRange;
        evokePosition.position += new Vector3(0, evokeHieght, 0);
        meteoriteList[currentNum].transform.position = evokePosition.position;
        meteoriteList[currentNum].transform.forward = -evokePosition.up;
        meteoriteList[currentNum].gameObject.SetActive(true);
        currentNum++;
    }

    public void Me03()
    {
        currentNum = currentNum % numberMeteorite;
        evokePosition.position = Random.insideUnitCircle * evokeRange;
        evokePosition.position += new Vector3(0, evokeHieght, 0);
        meteoriteList[currentNum].transform.position = evokePosition.position;
        meteoriteList[currentNum].transform.forward = -evokePosition.up;
        meteoriteList[currentNum].gameObject.SetActive(true);
        currentNum++;
    }

    public void Me04()
    {
        currentNum = currentNum % numberMeteorite;
        evokePosition.position = Random.insideUnitCircle * evokeRange;
        evokePosition.position += new Vector3(0, evokeHieght, 0);
        meteoriteList[currentNum].transform.position = evokePosition.position;
        meteoriteList[currentNum].transform.forward = -evokePosition.up;
        meteoriteList[currentNum].gameObject.SetActive(true);
        currentNum++;
    }
    public void MeLast()
    {
        currentNum = currentNum % numberMeteorite;
        evokePosition.position = Random.insideUnitCircle * evokeRange;
        evokePosition.position += new Vector3(0, evokeHieght, 0);
        meteoriteList[currentNum].transform.position = evokePosition.position;
        meteoriteList[currentNum].transform.forward = -evokePosition.up;
        meteoriteList[currentNum].gameObject.SetActive(true);
        currentNum++;

        currentNum = currentNum % numberMeteorite;
        evokePosition.position = Random.insideUnitCircle * evokeRange;
        evokePosition.position += new Vector3(0, evokeHieght, 0);
        meteoriteList[currentNum].transform.position = evokePosition.position;
        meteoriteList[currentNum].transform.forward = -evokePosition.up;
        meteoriteList[currentNum].gameObject.SetActive(true);
        currentNum++;

        currentNum = currentNum % numberMeteorite;
        evokePosition.position = Random.insideUnitCircle * evokeRange;
        evokePosition.position += new Vector3(0, evokeHieght, 0);
        meteoriteList[currentNum].transform.position = evokePosition.position;
        meteoriteList[currentNum].transform.forward = -evokePosition.up;
        meteoriteList[currentNum].gameObject.SetActive(true);
        currentNum++;
    }
}
