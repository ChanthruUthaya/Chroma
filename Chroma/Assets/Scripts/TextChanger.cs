using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using HTC.UnityPlugin.Vive;


[RequireComponent(typeof(SteamVR_TrackedObject))]

public class TextChanger : MonoBehaviour
{

    public SteamVR_TrackedObject trackedObj1;//left controller
    SteamVR_Controller.Device device1; //left
    public SteamVR_TrackedObject trackedObj2;//right controller
    SteamVR_Controller.Device device2;//right
    public GameObject circledetector;
    public GameObject cameraPos;

    private GameObject todelete;

    Vector3 randpos = new Vector3(0, 0, 0); //need to change to location of circle detector


    List<string> instructions;
    Dictionary<int, string> errormessages;
    public int currentintruction;

    List<GameObject> drawings;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<TextMesh>().characterSize = 0.03f; //need to change based on scale of room
        this.GetComponent<TextMesh>().fontSize = 250;
        currentintruction = 0;
        this.GetComponent<TextMesh>().text = "Welcome to CHROMA demo Tutorial, To continue click the left touchpad"; //0
        instructions.Add("Welcome to CHROMA demo Tutorial, To continue click the left touchpad"); //0
        instructions.Add("Teleport to the to the arrow by touching the right touchpad"); //1
        instructions.Add("To erase point the right controller at the drawing and click the right menu button, click left touchpad to continue");//2
        instructions.Add("Draw a circle by clicking the right trigger and tracing a circle");//3
        instructions.Add("Now lets try joining 2 drawing together, click left touchpad to continue");//4
        instructions.Add("Draw an object intersecting the circle");//5
        instructions.Add("Pick up the drawing by grabbing the drawing with the left trigger");//6
        instructions.Add("To change thickness, click the right menu button and draw");//7
        instructions.Add("Congratulations, you finished the tutorial!"); //8
    }

    private void FixedUpdate()
    {
        device1 = SteamVR_Controller.Input((int)trackedObj1.index);
        device2 = SteamVR_Controller.Input((int)trackedObj2.index);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = cameraPos.transform.position;
        Vector3 position = new Vector3(pos.x - 2.0f, pos.y + 2.0f, pos.z + 2.0f); // need to change based on position of text resp to player
        this.transform.position = position;

        switch (currentintruction) {
            case 0:
                if (device1.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) {
                    currentintruction += 1;
                    GetComponent<TextMesh>().text = instructions[currentintruction];
                }
                break;
            case 1:
                if (Distance(cameraPos.transform.position, randpos) < 0.2) {
                    currentintruction += 1;
                    GetComponent<TextMesh>().text = instructions[currentintruction];
                }
                break;
            case 2:
                if (device1.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    currentintruction += 1;
                    GetComponent<TextMesh>().text = instructions[currentintruction];
                }
                break;
            case 3:
                if (CircleDetector.flag) {
                    currentintruction += 1;
                    GetComponent<TextMesh>().text = instructions[currentintruction]; //add current drawings in scene to array
                }
                break;
            case 4:
                if (device1.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    currentintruction += 1;
                    GetComponent<TextMesh>().text = instructions[currentintruction];
                }
                break;
            case 5:
                bool joined = false;
                List<GameObject> circleDrawings = GetComponent<DrawingListner>().afterdrawings; //get all drawings during circle detection
                foreach(GameObject g in GameObject.FindGameObjectsWithTag("Drawing")) //get all drawings after circle detections
                {
                    if (!circleDrawings.Contains(g)) {
                        circleDrawings.Add(g);
                    }
                }
                foreach (GameObject g in circleDrawings) {
                    if (g.transform.childCount != 0) {
                        joined = true;
                        todelete = g;
                    }
                }
                if (joined) {
                    currentintruction += 1;
                    GetComponent<TextMesh>().text = instructions[currentintruction];
                }
                break;
            case 6:
                //to do check right hand for children with tags drawing
                foreach (Transform child in trackedObj1.transform) {
                    if (child.tag == "Drawing") {
                        currentintruction += 1;
                        GetComponent<TextMesh>().text = instructions[currentintruction];
                        break;
                    }

                }
                break;
            case 7:
                if (ViveInput.GetPress(HandRole.RightHand, ControllerButton.Menu)) {
                    currentintruction += 1;
                    GetComponent<TextMesh>().text = instructions[currentintruction];
                }
                break;
            case 8:
                //to do trigger game
                break;

        }
    }


    float Distance(Vector3 a, Vector3 b)
    {
        float xDist = Mathf.Pow((a.x - b.x), 2);
        float zDist = Mathf.Pow((a.z - b.z), 2);
        float dist = Mathf.Sqrt(xDist + zDist);
        return dist;
    }
}
