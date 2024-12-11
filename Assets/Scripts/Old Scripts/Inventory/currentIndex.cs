using UnityEngine;

public class currentIndex : MonoBehaviour {

    public SystemUpdate update;

    public void SelectedArms(int getid) => update.princess.CurrentWeapon = getid;
    public void SelectedBandolier(int getid) => update.princess.CurrentBandolier = getid;
    public void SelectedDress(int getid) => update.princess.CurrentDress = getid;
}