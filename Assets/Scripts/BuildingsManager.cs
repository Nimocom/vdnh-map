using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class BuildingsManager : MonoBehaviour
{
    public static BuildingsManager Instance;

    public List<Building> Buildings;

    private void Awake()
    {
        Instance = this;

        Buildings.AddRange(FindObjectsOfType<Building>());

        //for (int i = 0; i < Buildings.Count; i++)
        //{
        //    Buildings[i].iD = i + 241;
        //}
    }

    private void Start()
    {
        StartCoroutine(LoadPlacedData());
    }

    IEnumerator LoadPlacedData()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(Path.Combine(Application.streamingAssetsPath, "Places.json")))
        {
            yield return webRequest.SendWebRequest();

            var jsonString = webRequest.downloadHandler.text;

            var content = JsonUtility.FromJson<Root>(jsonString);

            for (int i = 0; i < Buildings.Count; i++)
            {
                Buildings[i].PlaceData = content.Places[i];
            }

            MarkersManager.Instance.InitializeMarkers();
            //Route route = new Route();
            //route.Points = new int[] { 435, 409, 306, 343};

            //RouteBuilder.Instance.DrawPath(route);
            TermalZoneManager.Instance.GenerateData();
            PlacesWindow.Instance.LoadPlaces();
            TagManager.Instance.InitializeTags(Buildings);
            StartCoroutine(EventManager.Instance.InitializeEvents());
        }
    }
}