using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Place PlaceData;

    public int iD;

   public Transform point;

    public Transform GetPoint() => point;

    private void Awake()
    {
        point = transform.GetChild(0);
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