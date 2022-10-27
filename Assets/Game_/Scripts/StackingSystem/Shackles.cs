using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shackles : MonoBehaviour
{
    public List<Transform> ToChainedCharacters;
    [SerializeField] Transform ChainPrefab;
    private void OnEnable()
    {
        ToChainedCharacters.Add(this.transform);
    }
    private void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Criminal" && !ToChainedCharacters.Contains(other.transform))
        {
            ToChainedCharacters.Add(other.transform);
                GameObject tempChain = Instantiate(ChainPrefab.gameObject, other.transform.position/* + (Vector3.up * 1)*/, Quaternion.Euler(0, 0, -90));
                //first node//
                tempChain.transform.GetChild(0).transform.parent = ToChainedCharacters[ToChainedCharacters.Count - 2].transform.GetChild(0).transform;
                tempChain.transform.GetChild(0).transform.localPosition = Vector3.zero;         
                //last node
                tempChain.transform.GetChild(tempChain.transform.childCount - 1).transform.parent = ToChainedCharacters[ToChainedCharacters.Count-1].transform.GetChild(0).transform;
                //tempChain.transform.GetChild(tempChain.transform.childCount - 1).transform.parent = other.transform.GetChild(0).transform;
                tempChain.transform.GetChild(tempChain.transform.childCount - 1).transform.localPosition = Vector3.zero;
            setPos();

        }
    }
    void setPos()
    {
        for (int i = 1; i < ToChainedCharacters.Count-1; i++)
        {
            ToChainedCharacters[i].GetChild(0).GetChild(0).transform.localPosition = Vector3.zero;
            ToChainedCharacters[i].GetChild(0).GetChild(1).transform.localPosition = Vector3.zero;
        }
    }
}
