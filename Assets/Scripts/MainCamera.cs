using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    public LayerMask layer;
    public float flySpeed = 0.5f;
    public float sensitiveX = 1f;
    public float sensitiveY = 1f;
    public float minX = -360;
    public float maxX = 360;
    public float minY = -60;
    public float maxY = 60;
    private float rotX = 0f, rotY = 0f;

    private Quaternion cur_rot;
	// Use this for initialization
	void Start () {
        cur_rot = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition); // настраиваем луч из камеры на мышку
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer)) // пускаем луч и находим координаты попадания
            {
                var objs = GameObject.FindGameObjectsWithTag("Player");
                for (int i = 0; i < objs.Length; i++)
                {
                    objs[i].GetComponent<PlayerAgent>().target = hit.point; // ставим всем (пока) чубзиками идти туда
                }
            }
        }

        // поворот камеры
		
        rotX += Input.GetAxis("Mouse X") * sensitiveX;
        rotY += Input.GetAxis("Mouse Y") * sensitiveY;

        rotX = rotX % 360;
        rotY = rotY % 360;

        rotX = Mathf.Clamp(rotX, minX, maxX);
        rotY = Mathf.Clamp(rotY, minY, maxY);

        Quaternion xquat = Quaternion.AngleAxis(rotX, Vector3.up);
        Quaternion yquat = Quaternion.AngleAxis(rotY, Vector3.left);

        transform.localRotation = cur_rot * xquat * yquat;

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
}