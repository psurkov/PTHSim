using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBarScript : MonoBehaviour
{
    private Scrollbar sb;
    public int cur_n;

    // Start is called before the first frame update
    void Start()
    {
        sb = GetComponent<Scrollbar>();
        sb.numberOfSteps = GameObject.FindGameObjectsWithTag("Player").Length;
    }

    // Update is called once per frame
    void Update()
    {
        cur_n = Mathf.RoundToInt(sb.value * sb.numberOfSteps);
    }
}
