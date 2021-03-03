using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int pool = 0;
    public GameObject obj = null;
    public float rate = 1;

    private int iter = 0;
    private float elapsedTime = 0;
    private bool isEnemy = false;
    private void Awake()
    {
        if (obj.GetComponent<Enemy>() != null)
        {
            isEnemy = true;
        }
        for (int i = 0; i < pool; i++)
        {
            GameObject tmp = Instantiate(obj);
            tmp.transform.parent = transform;
            tmp.transform.localScale = transform.localScale;
            //tmp.transform.position = new Vector3(i*10, -1000, 0);
            tmp.SetActive(false);
        }
    }

    void Update()
    {
        int instance = 0;
        elapsedTime += Time.deltaTime;
        if (isEnemy)
        {
            while (elapsedTime >= 1.0f / rate)
            {
                elapsedTime -= 1.0f / rate;
                GameObject tmp = transform.GetChild(iter).gameObject;
                iter++;
                if (iter >= transform.childCount)
                {
                    iter -= transform.childCount;
                }
                if (tmp.activeSelf == false)
                {
                    tmp.SetActive(true);
                    tmp.transform.position = transform.position + transform.right * instance * 10;
                    instance++;
                }
                else if (tmp.GetComponent<Ragdoll>().RagdollOn == true)
                {
                    tmp.GetComponent<Enemy>().Respawn();
                    tmp.SetActive(true);
                    tmp.transform.position = transform.position + transform.right * instance * 10;
                    instance++;
                }
                // add whatever force
            }
        }
        else
        {
            while (elapsedTime >= 1.0f / rate)
            {
                elapsedTime -= 1.0f / rate;
                GameObject tmp = transform.GetChild(iter).gameObject;
                iter++;
                if (iter >= transform.childCount)
                {
                    iter -= transform.childCount;
                }
                tmp.SetActive(true);
                tmp.transform.position = transform.position + transform.right * instance * 10;
                instance++;
                // add whatever force
            }
        }

    }
}
