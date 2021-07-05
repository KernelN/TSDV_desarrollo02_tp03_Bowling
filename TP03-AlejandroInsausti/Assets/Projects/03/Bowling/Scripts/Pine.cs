using UnityEngine;
using TMPro;

public class Pine : MonoBehaviour
{
    [SerializeField] bool onFoot;
    [SerializeField] TextMeshProUGUI pointsCounter;
    [SerializeField] TextMeshProUGUI pinsLeft;

    static short pinesLeft;
    static short points;
    static short pointsInThisRound;

    void Start()
    {
        if (pinesLeft < 0)
        {
            pinesLeft = (short)transform.parent.childCount;
        }
    }

    void Update()
    {
        if (!IsOnFoot())
        {
            pinesLeft--;
            pinsLeft.SetText("Pins Left\n" + pinesLeft.ToString("D2"));
            gameObject.SetActive(false);
        }
    }

    bool IsOnFoot()
    {
        return !(Mathf.Abs(transform.localRotation.x) > 65 || Mathf.Abs(transform.localRotation.z) > 65);
    }
}
