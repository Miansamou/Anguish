using UnityEngine;

public class MovingScenario : MonoBehaviour
{
    #region variables

    [SerializeField] 
    private Vector3 target = new Vector3(0, 0, -70);
    [SerializeField] 
    private float speed = 2;

    #endregion

    #region eternal movement

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);

        if (this.transform.position.z <= -69.95979)
        {
            transform.position = new Vector3(this.transform.position.x, 0, 270.11783f);
        }
    }

    #endregion
}
