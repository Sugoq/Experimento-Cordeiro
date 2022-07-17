using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagsController : MonoBehaviour
{
    [SerializeField]
    private List<string> tags = new List<string>();

    public bool HasTag(string tag)
    {
        return tags.Contains(tag);
    }
}
