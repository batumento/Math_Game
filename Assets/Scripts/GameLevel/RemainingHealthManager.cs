using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainingHealthManager : MonoBehaviour
{
    [SerializeField] private GameObject[] remainingHealth;

    public void RemainingHealthChecked(int remainingLife)
    {
        switch (remainingLife)
        {
            case 3:
                remainingHealth[0].SetActive(true);
                remainingHealth[1].SetActive(true);
                remainingHealth[2].SetActive(true);
                break;
            case 2:
                remainingHealth[0].SetActive(false);
                remainingHealth[1].SetActive(true);
                remainingHealth[2].SetActive(true);
                break;
            case 1:
                remainingHealth[0].SetActive(false);
                remainingHealth[1].SetActive(false);
                remainingHealth[2].SetActive(true);
                break;
            case 0:
                remainingHealth[0].SetActive(false);
                remainingHealth[1].SetActive(false);
                remainingHealth[2].SetActive(false);
                break;
        }

    }
}
