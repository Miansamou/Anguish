using UnityEngine;

public class MovingScenarioPrologue : MonoBehaviour
{
    [SerializeField]
    private GameObject otherBlock;
    [SerializeField]
    private GameObject frontObj;
    [SerializeField]
    private GameObject backObj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.transform.position.z > gameObject.transform.position.z)
            {
                otherBlock.transform.position = frontObj.transform.position;
            }
            else
            {
                otherBlock.transform.position = backObj.transform.position;
            }
        }
    }
}
