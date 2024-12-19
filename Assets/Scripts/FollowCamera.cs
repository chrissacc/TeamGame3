using UnityEngine;

public enum FollowState
{
    Following = 0,
    MovingBack = 1,
    Still = 2
}
public class FollowCamera : MonoBehaviour
{
    public GameObject Player;
    public GameObject BasePosition;
    public GameObject ClosePosition;
    public float speed;
    public FollowState state;
    void Start()
    {
        Player = transform.parent.gameObject;
        state = FollowState.Still;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, BasePosition.transform.position) > 3f && state == FollowState.Still) 
        {
            state = FollowState.Following;
        }
        if (state == FollowState.Following)
        {
            Vector3 newPos = transform.position;
            newPos = Vector3.MoveTowards(newPos, ClosePosition.transform.position, speed * Time.deltaTime);
            transform.position = newPos;
            if (Vector3.Distance(newPos, ClosePosition.transform.position) <= .5f)
            {
                state = FollowState.MovingBack;
            }
        }
        if (state == FollowState.MovingBack)
        {
            Vector3 newPos = transform.position;
            newPos = Vector3.MoveTowards(newPos, BasePosition.transform.position, speed * Time.deltaTime);
            transform.position = newPos;
            if (Vector3.Distance(newPos, BasePosition.transform.position) <= .1f)
            {
                transform.position = BasePosition.transform.position;
                state = FollowState.Still;
            }
        }
    }
}
