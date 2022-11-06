using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterWindow : MonoBehaviour
{
    public static FilterWindow Instance;    

    bool isExpanded;

    Animation animationComponent;

    public FilterSettings ActiveSettings;

    private void Awake()
    {
        Instance = this;

        animationComponent = GetComponent<Animation>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToAdvancedSettings()
    {
        isExpanded = !isExpanded;
        animationComponent.Play(!isExpanded ? "FilterHide" : "FilterShow");
    }

    public void SetMaxPeople(Slider slider)
    {
        ActiveSettings.People = (int)slider.value;
    }

    public void SetAge(Slider slider)
    {
        ActiveSettings.Age = (int)slider.value;
    }

    public void SetCost(Slider slider)
    {
    ActiveSettings.Cost = (int)slider.value;    
    }

    public void SetAvailableTime(Slider slider)
    {
        ActiveSettings.TimeInMinutes = (int)slider.value;
    }
    public void SetTransportPref(Slider slider)
    {
        ActiveSettings.IsOnTransport = (int)slider.value;
    }
    public void SetOVZPref(Slider slider)
    {
        ActiveSettings.IsForOVZ = (int)slider.value;
    }

    public void SetTag(string tag)
    {
        ActiveSettings.Tags.Add(tag);
    }

    public void ResetTags()
    {
        ActiveSettings.Tags.Clear();
    }
}

public class FilterSettings
{
    public int People;
    public int Cost;
    public int TimeInMinutes;
    public int Age;
    public int IsOnTransport;
    public int IsForOVZ;
    public int IsOutside;

    public List<string> Tags;
}
