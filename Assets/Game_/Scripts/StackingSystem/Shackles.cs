using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shackles : MonoBehaviour
{
    public static Shackles instance;
    public List<Transform> ToChainedCharacters;
    public List<Transform> connectedChains;
    [SerializeField] Transform ChainPrefab;
    [SerializeField] Transform ChainsConnectedPoint;
    private void OnEnable()
    {
        ToChainedCharacters.Add(this.transform);
    }
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "Criminal" || other.gameObject.tag == "ToArrested" && !ToChainedCharacters.Contains(other.transform))
        {

            ToChainedCharacters.Add(other.transform);
            GameObject tempChain = Instantiate(ChainPrefab.gameObject, other.transform.position/* + (Vector3.up * 1)*/, Quaternion.Euler(0, 0, -90));

            //first node//
            tempChain.transform.GetChild(0).transform.parent = ToChainedCharacters[ToChainedCharacters.Count - 2].transform.GetChild(0).transform;
            tempChain.transform.GetChild(0).transform.localPosition = Vector3.zero;
            ToChainedCharacters[0].GetChild(0).GetChild(0).transform.localPosition = Vector3.zero;

            //last node
            tempChain.transform.GetChild(tempChain.transform.childCount - 1).transform.parent = ToChainedCharacters[ToChainedCharacters.Count - 1].transform.GetChild(0).transform;
            //tempChain.transform.GetChild(tempChain.transform.childCount - 1).transform.parent = other.transform.GetChild(0).transform;
            tempChain.transform.GetChild(tempChain.transform.childCount - 1).transform.localPosition = Vector3.zero;
            setPos();
            connectedChains.Add(tempChain.transform);
        }
    }
    void setPos()
    {
        var _lastCharacterpos = ToChainedCharacters[ToChainedCharacters.Count - 1].GetChild(0).GetChild(0).transform;
        _lastCharacterpos.localPosition = Vector3.zero;

        for (int i = 1; i < ToChainedCharacters.Count - 1; i++)
        {
            ToChainedCharacters[i].GetChild(0).GetChild(0).transform.localPosition = Vector3.zero;
            ToChainedCharacters[i].GetChild(0).GetChild(1).transform.localPosition = Vector3.zero;
        }
    }
    public void removeChains()
    {
        StartCoroutine(removeChain(ChainsConnectedPoint));
    }
    IEnumerator removeChain(Transform _ChainsConnectedPoint)
    {
        _ChainsConnectedPoint.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        foreach (var item in connectedChains)
        {
            item.gameObject.SetActive(false);

        }
    }
}
