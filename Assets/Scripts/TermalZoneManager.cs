using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Linq;

public class TermalZoneManager : MonoBehaviour
{
    public static TermalZoneManager Instance;

    public SpriteRenderer zoneIndicatorPrefab;

    ZoneData zoneData;

    [SerializeField] ColorLoadndessDependency loadndessDependency;

    [SerializeField] GameObject contentRoot;

    private void Awake()
    {
        Instance = this;

    }

    public void GenerateData()
    {
        StartCoroutine(Generate());
    }

    // Start is called before the first frame update
    IEnumerator Generate()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(Path.Combine(Application.streamingAssetsPath, "ZoneLoadness.json")))
        {
            yield return webRequest.SendWebRequest();

            var jsonString = webRequest.downloadHandler.text;

            zoneData = JsonUtility.FromJson<ZoneData>(jsonString);

            for (int i = 0; i < zoneData.zoneData.Count; i++)
            {
                var building = BuildingsManager.Instance.Buildings.Single(x => x.PlaceData.properties.id == zoneData.zoneData[i].zoneID);
                var indicator = Instantiate(zoneIndicatorPrefab, building.transform.position, Quaternion.identity, contentRoot.transform);
                indicator.transform.eulerAngles = new Vector3(-90f, 0f, 0f);
                MeshRenderer meshRenderer;

                if (building.gameObject.TryGetComponent<MeshRenderer>(out meshRenderer))
                {
                    indicator.transform.localScale = Vector3.one * building.gameObject.GetComponent<MeshRenderer>().bounds.size.magnitude * 0.15f;
                }


                indicator.color = loadndessDependency.loadnessColors[zoneData.zoneData[i].loadness];
            }

            contentRoot.SetActive(false);
        }
    }

    public void SwitchState()
    {
        contentRoot.SetActive(!contentRoot.activeSelf);
    }
}

[System.Serializable]
public class ZoneData
{
    public List<Zone> zoneData;
}

[System.Serializable]
public class Zone
{
    public int zoneID;
    public int loadness;
}

[System.Serializable]
public class ColorLoadndessDependency
{
    public Color[] loadnessColors;
}

