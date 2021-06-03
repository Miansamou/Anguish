using UnityEngine;

public class LocationPlayer : MonoBehaviour
{
    [SerializeField]
    private string locationName;
    [SerializeField]
    private PlayerStatus status;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            status.SetLocation(locationName);
        }
    }
}
