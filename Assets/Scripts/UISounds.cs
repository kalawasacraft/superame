using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISounds : MonoBehaviour
{
    [SerializeField] private bool _isConfirm = true;

    public void PlayNavigationSound()
    {
        SoundsManager.NavigationPlay();
    }

    public void PlayPressedSound()
    {
        if (_isConfirm) {
            SoundsManager.ConfirmPlay();
        } else {
            SoundsManager.BackPlay();
        }
    }
}
