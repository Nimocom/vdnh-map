using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class RoutesWindow : MonoBehaviour
{
    [SerializeField] RouteList routeList;

    [SerializeField] RoutePanel routePanelPrefab;

    [SerializeField] Transform contentRoot;

    List<Route> routes = new List<Route>();     

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadRoutesData());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadRoutesData()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(Path.Combine(Application.streamingAssetsPath, "Routes.json")))
        {
            yield return webRequest.SendWebRequest();

            var jsonString = webRequest.downloadHandler.text;

            routeList = JsonUtility.FromJson<RouteList>(jsonString);

            for (int i = 0; i < routeList.Routes.Count; i++)
            {
             
                routes.Add(routeList.Routes[i]);
            }
        }
    }

    public void Filter()
    {
        for (int i = 0; i < contentRoot.childCount; i++)
        {
            Destroy(contentRoot.GetChild(i).gameObject);
        }

        FilterSettings settings = FilterWindow.Instance.ActiveSettings;

        for (int i = 0; i < routes.Count; i++)
        {
            Route route = routes[i];

            if (route.TimeInMinutes > settings.TimeInMinutes ||
                route.IsOnTransport != settings.IsOnTransport ||
                route.IsForOVZ != settings.IsForOVZ ||
                route.MinCost > settings.Cost ||
                route.MaxPeople > settings.People ||
                route.MinAge > settings.Age)
                return;


            Instantiate(routePanelPrefab, contentRoot).InitializePanel(route);
        }
    }
}

