using UnityEngine;

public class Actor : MonoBehaviour
{
    public float speed;
    private int currNode;
   
    public void Move(UnityEngine.Vector3 [] path)
    {
        if((path[currNode]- transform.position).magnitude < 0.0)
        {
            currNode++;
        }
        var dir = path[currNode] - transform.position;

        if (dir !=new Vector3(0,0,0))
        {
            var rotacion = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, Time.deltaTime);
        }
        var vel = transform.forward * speed * Time.deltaTime;
        transform.position += vel;
    }

}
