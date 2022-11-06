using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.EventSystems;

public class RoutePanel : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] TextMeshProUGUI routeName;
    [SerializeField] TextMeshProUGUI routeDescription;

    [SerializeField] Transform pointContentRoot;
    [SerializeField] RoutePoint routePointPrefab;
    Route route;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializePanel(Route route)
    {
        this.route = route;
        routeName.SetText(route.RouteName);
        routeDescription.SetText(route.RouteDescription);

        for (int i = 0; i < route.Points.Length; i++)
        {
            Instantiate(routePointPrefab, pointContentRoot).SetRouteName(BuildingsManager.Instance.Buildings.Single(x => x.PlaceData.properties.id == route.Points[i]).PlaceData.properties.title);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RouteBuilder.Instance.DrawPath(route);
    }
}
