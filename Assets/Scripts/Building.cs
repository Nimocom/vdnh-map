using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Place PlaceData;

    public int iD;

    public Transform point;
    Vector3 startScale;

    public Transform GetPoint() => point;

    private void Awake()
    {
        point = transform.GetChild(0);
        startScale = transform.localScale;

    }

    private void OnMouseDown()
    {
        InfoPanel.Instance.ShowPanel(this);
    }

    private void OnMouseEnter()
    {
        StopAllCoroutines();
        StartCoroutine(LerpScale(1.1f));
    }

    private void OnMouseExit()
    {
        StopAllCoroutines();
        StartCoroutine(LerpScale(1f));
    }

    IEnumerator LerpScale(float size)
    {
        var targetScale = startScale * size;
        while (transform.localScale != targetScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, 12f * Time.deltaTime);
            yield return null;
        }

        transform.localScale = targetScale;
    }

}

//[System.Serializable]
//public class BuildingData
//{
//    public enum BuildingType
//    {
//        Activities,
//        Foot,
//        Walking,
//        Services,
//        Transport
//    }

//    public BuildingType Type;

//    public string Name;
//    public string Description;
//}