using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderForegroundLayers : MonoBehaviour
{
    [SerializeField] List<GameObject> ObjectList;
    bool empty;
    [SerializeField] [Range(0, 100)] int precision;

    // Start is called before the first frame update
    void Start()
    {
        empty = false;

        if ( ObjectList.Count == 0 )
        {
            empty = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!empty)
        {
            foreach (GameObject obj in ObjectList)
            {
                var objArray = GameObject.FindGameObjectsWithTag(obj.tag);

                foreach (GameObject thisObj in objArray)
                {
                    thisObj.GetComponent<SpriteRenderer>().sortingOrder = -(int)(precision*thisObj.transform.position.y);
                }
            }
        }
    }
}
