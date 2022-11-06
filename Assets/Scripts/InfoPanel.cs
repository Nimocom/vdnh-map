using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoPanel : MonoBehaviour
{
    public static InfoPanel Instance;
    [SerializeField] TextMeshProUGUI type;
    [SerializeField] TextMeshProUGUI description;

    Marker currentMarker;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentMarker)
            return;

        transform.position = currentMarker.transform.position;
    }

    public void ShowPanel(Marker marker)
    {
        Building building = marker.building;
        currentMarker = marker;

        type.SetText(building.PlaceData.properties.type);
        description.SetText(building.PlaceData.properties.title);
        gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        gameObject.SetActive(false);
    }
}
