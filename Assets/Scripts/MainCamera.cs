using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainCamera : MonoBehaviour {

    public LayerMask layer;
    public GameObject scbar;
    public float flySpeed = 0.5f;
    public float sensitiveX = 1f;
    public float sensitiveY = 1f;
    public float minX = -360;
    public float maxX = 360;
    public float minY = -60;
    public float maxY = 60;
    private float rotX = 0f, rotY = 0f;
    public float height_form_floor = 20f;
    public GameObject[] floor_objects = new GameObject[2];
    public int cur_floor = 0, floor_offset = 3;
    public GameObject textOfFlat;

    private Quaternion cur_rot;
	// Use this for initialization
	void Start () {
        cur_rot = transform.localRotation;
        updateFloor();
	}
	
	// Update is called once per frame
	void Update () {

        var players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
            players[i].GetComponent<MeshRenderer>().enabled = 5 > players[i].transform.position.y - floor_objects[cur_floor].transform.position.y;
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition); // настраиваем луч из камеры на мышку
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer)) // пускаем луч и находим координаты попадания
            {
                
                for (int i = 0; i < scbar.GetComponent<ScrollBarScript>().cur_n; i++)
                {
                    players[i].GetComponent<PlayerAgent>().target = hit.point; // двигаем
                }
            }
        }
            

        if (Input.GetMouseButton(1))  // поворот камеры
        {
            rotX += Input.GetAxis("Mouse X") * sensitiveX;
            rotY += Input.GetAxis("Mouse Y") * sensitiveY;

            rotX = rotX % 360;
            rotY = rotY % 360;

            rotX = Mathf.Clamp(rotX, minX, maxX);
            rotY = Mathf.Clamp(rotY, minY, maxY);

            Quaternion xquat = Quaternion.AngleAxis(rotX, Vector3.up);
            Quaternion yquat = Quaternion.AngleAxis(rotY, Vector3.left);

            transform.localRotation = cur_rot * xquat * yquat;
        }

        // движение

        if (Input.GetAxis("Vertical") != 0)
        {
            Vector3 pos = transform.forward * flySpeed * Input.GetAxis("Vertical");
            transform.position += new Vector3(pos.x, 0, pos.z);
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            Vector3 pos = transform.right * flySpeed * Input.GetAxis("Horizontal");
            transform.position += new Vector3(pos.x, 0, pos.z);
        }
    }

    private void updateFloor()
    {
        textOfFlat.GetComponent<Text>().text = (cur_floor + floor_offset).ToString() + " этаж";
        transform.position = new Vector3(transform.position.x, floor_objects[cur_floor].transform.position.y + height_form_floor, transform.position.z);
        for (int i = 0; i < floor_objects.Length; i++)
            floor_objects[i].SetActive(i <= cur_floor);
    }

    public void goUpFloor()
    {
        if (cur_floor < floor_objects.Length - 1)
            cur_floor++;
        updateFloor();
    }

    public void goDownFloor()
    {
        if (cur_floor > 0)
            cur_floor--;
        updateFloor();
    }
}