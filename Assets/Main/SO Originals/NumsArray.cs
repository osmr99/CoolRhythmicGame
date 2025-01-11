using UnityEngine;


[CreateAssetMenu(menuName = "Nums Array")]
public class NumsArray : ScriptableObject
{
    public float timeScale;
    public float playDelay;
    public int startPos;
    public int currentBeat;
    public float time;
    public float alternateTime;
    public int lastPlayedIndex;
    public bool resetMarkers = false;
    public float[] nums;
    public float[] alternateNums;
}
