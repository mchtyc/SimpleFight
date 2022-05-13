using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLittleBlades_Controller : MonoBehaviour
{
    public Transform Blades;
    public BoxCollider _collider;

    Vector3 bladesDefaultPos, bladesActivePos;
    int cycle;

    [HideInInspector] public float damage = 50f;

    // Start is called before the first frame update
    void Start()
    {
        _collider.enabled = false;
        bladesDefaultPos = Blades.localPosition;
        bladesActivePos = new Vector3(0f, 1.1f, 0f);

        StartCoroutine(LittleBladesCycle());
    }

    IEnumerator LittleBladesCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);

            _collider.enabled = true;
            cycle = 10;

            for (int i = 0; i <= cycle; i++)
            {
                Blades.position = new Vector3(Blades.position.x, Mathf.Lerp(Blades.position.y, bladesActivePos.y, (float)(i / cycle)), Blades.position.z);
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(5f);

            _collider.enabled = false;
            cycle = 10;

            for (int i = 0; i <= cycle; i++)
            {
                Blades.position = new Vector3(Blades.position.x, Mathf.Lerp(Blades.position.y, bladesDefaultPos.y, (float)(i / cycle)), Blades.position.z);
                yield return new WaitForEndOfFrame();
            } 
        }
    }
}
