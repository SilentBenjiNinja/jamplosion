using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisappearAfterThreeSecs : MonoBehaviour
{
    public void WinScreen()
    {
        this.gameObject.SetActive(true);
        StartCoroutine(DisappearAfter());
    }

    IEnumerator DisappearAfter()
    {
        yield return new WaitForSeconds(3);
        this.gameObject.SetActive(false);
    }
}
