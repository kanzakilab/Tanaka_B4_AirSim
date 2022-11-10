using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectDetector : MonoBehaviour
{
    [SerializeField] private GameObject Drone1;
    [SerializeField] private GameObject Drone2;
    private Vector3 pos1;
    private Vector3 pos2;
    private float dis;
    private Color cnct = new Color(0.22f, 0.7f, 0.54f, 1.0f);
    private Color dscn = new Color(1.0f, 0.0f, 0.0f, 1.0f);
    private Text txt;

    void Start()
    {
        txt = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        pos1 = Drone1.transform.position;
        pos2 = Drone2.transform.position;
        dis = Vector3.Distance(pos1, pos2);
        if (dis > 35.0f)
        {
            txt.text = "Disconnected";
            txt.color = dscn;
        }
        else
        {
            txt.text = "Connected";
            txt.color = cnct;
        }
    }
}
