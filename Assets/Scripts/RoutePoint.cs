using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoutePoint : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI routePoint;


    public void SetRouteName(string routeName)
    {
        routePoint.SetText(routeName);  
    }
}
