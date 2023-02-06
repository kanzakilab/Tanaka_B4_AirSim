using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class SaveTraceData : MonoBehaviour
{
    private StreamWriter sw;
    private int framecount = 0;
    private float posx = 0;
    private float posy = 0;
    private float posz = 0;
    //private string[] s1;
    //private string s2;
    // Start is called before the first frame update
    void Start()
    {
        sw = new StreamWriter(@"C:\Users\jetst\Downloads\SaveDataNB.csv", true, Encoding.GetEncoding("Shift_JIS"));
        string[] s1 = { "Frame", "x", "y", "z" };
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        framecount++;
        posx = this.gameObject.transform.position.x;
        posy = this.gameObject.transform.position.y;
        posz = this.gameObject.transform.position.z;
        string[] s1 = { framecount.ToString(), posx.ToString(), posy.ToString(), posz.ToString() };
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);
    }

    void OnApplicationQuit()
    {
        sw.Close();
    }
}
