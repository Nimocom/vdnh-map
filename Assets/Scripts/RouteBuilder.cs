using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class RouteBuilder : MonoBehaviour
{
    public static RouteBuilder Instance { get; private set; }

    [SerializeField] NavMeshAgent agent;

    Route activeRoute;
    bool isMoving;

    List<Building> routeBuildings;
    int currentPoint;
    NavMeshPath path;

    private void Awake()
    {
        Instance = this;    
        routeBuildings = new List<Building>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //RouteList routeList = new RouteList();
        //routeList.Routes = new List<Route>();
        //Route route = new Route();

        //route.MaxVisitorsAmount = 12;
        //route.TimeInMinutes = 30;
        //route.BudgetStatus = 0;
        //route.MinimalAge = 8;

        //route.TransportStatus = 0;
        //route.AreaStatus = 0;
        //route.HVSSTatus = 0;

        //route.RouteDescription = "Описание";
        //route.RouteName = "Название";


        //route.Points = new int[] { 0, 0, 0 };

        //route.Tags = new List<string>();
        ////route.Tags.Add("");

        //routeList.Routes.Add(route);

        //string jsonStr = JsonUtility.ToJson(routeList);

        //System.IO.File.WriteAllText(@"E:/RoutesExample.json", jsonStr);
    }

    // Update is called once per frame
    void Update()
    {
        //if (!isMoving)
        //    return;
        if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0f)
        {
            if (currentPoint == routeBuildings.Count)
            {
                return;
            }

            agent.SetDestination(routeBuildings[currentPoint++].point.position);
        }


    }

    public void DrawPath(Route route)
    {
        TrailBuilder.Instance.ClearTrail();

        currentPoint = 0;

        activeRoute = route;

        routeBuildings.Clear();

        var buildings = BuildingsManager.Instance.Buildings;

        for (int i = 0; i < activeRoute.Points.Length; i++)
        {
            var building = buildings.SingleOrDefault(x => x.PlaceData.properties.id == activeRoute.Points[i]);

            if (building == null)
                return;

            routeBuildings.Add(building);
        }

        agent.Warp(routeBuildings[currentPoint].point.position);

        currentPoint++;

        agent.SetDestination(routeBuildings[currentPoint].point.position);

        isMoving = true;
    }
}
