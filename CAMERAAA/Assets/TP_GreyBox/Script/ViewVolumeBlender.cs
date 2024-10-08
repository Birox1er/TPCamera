using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewVolumeBlender : MonoBehaviour
{
    List<AViewVolume> ActiveViewVolumes;
    Dictionary<AView, List<AViewVolume>> VolumesPerViews;



    public void AddVolume(AViewVolume viewVol)
    {
        ActiveViewVolumes.Add(viewVol);
        if (!VolumesPerViews.ContainsKey(viewVol.view))
        {
            VolumesPerViews.Add(viewVol.view, new List<AViewVolume>());
            viewVol.view.SetActive(true);
        }
        VolumesPerViews[viewVol.view].Add(viewVol);
    }

    public void RemoveVolume(AViewVolume viewVol)
    {
        ActiveViewVolumes.Remove(viewVol);
        VolumesPerViews[viewVol.view].Remove(viewVol);
        if (VolumesPerViews[viewVol.view].Count == 0)
        {
            VolumesPerViews.Remove(viewVol.view);
            viewVol.view.SetActive(false);
        }
    }

    private static ViewVolumeBlender instance = null;
    public static ViewVolumeBlender Instance => instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

        // Initialisation du Game Manager...
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
