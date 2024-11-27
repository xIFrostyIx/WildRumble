using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=yDNZpkyosrA Tutorial followed by Alex Wolfe
public class Link
{
    public GameObject link;
    public Connector connector;
    public string targetName;
}

public class Detector : MonoBehaviour
{
    public GameObject linkPrefab;
    private List<Link> linksList = new List<Link>();

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Debug.Log("Projectile Detected Enemy");
            if (linkPrefab != null)
            {
                Link newLink = new Link() { link = Instantiate(linkPrefab) as GameObject };
                newLink.connector = newLink.link.GetComponent<Connector>();
                newLink.targetName = other.name;
                linksList.Add(newLink);
                if (newLink.connector != null) 
                {
                    newLink.connector.MakeConnection(transform.position, other.transform.position);
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log("Projectile Update Link");
        if (linksList.Count > 0)
        {
            for (int i = 0; i < linksList.Count; i++) 
            {
                if(other.name == linksList[i].targetName)
                    linksList[i].connector.MakeConnection(transform.position, other.transform.position);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("Projectile Exit");
        if (linksList.Count > 0)
        {
            for (int i = 0; i < linksList.Count; i++)
            {
                if (other.name == linksList[i].targetName)
                    Destroy(linksList[i].link);
            }
        }
    }
}
