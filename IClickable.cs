using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClickable 
{
    bool isClicked { get; set; }
    void onClick();
    void onHover();

    bool Contains(Vector3 position);
}
