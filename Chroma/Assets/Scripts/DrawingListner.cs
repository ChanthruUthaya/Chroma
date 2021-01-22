using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingListner : MonoBehaviour
{
    List<GameObject> beforedrawings;
    public List<GameObject> afterdrawings; 
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Drawing");
        beforedrawings.AddRange(obj);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<TextChanger>().currentintruction == 3)
        {
            GameObject[] obj = GameObject.FindGameObjectsWithTag("Drawing");
            foreach (GameObject o in obj)
            {
                if (!beforedrawings.Contains(o))
                {
                    afterdrawings.Add(o);
                }
            }
        }
    }
}
