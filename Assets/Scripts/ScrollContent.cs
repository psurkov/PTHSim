using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollContent : MonoBehaviour
{
    public RectTransform prefab;
    public RectTransform content;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
        var objs = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < objs.Length; i++)
        {
            GameObject instance = GameObject.Instantiate(prefab.gameObject, content.transform) as GameObject;
            instance.transform.SetParent(content.transform, false);
            //instance.GetComponent
            //objs[i].
        }
    }
}
