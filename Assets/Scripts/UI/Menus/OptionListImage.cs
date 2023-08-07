using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionListImage : OptionList
{
    public List<Sprite> images;
    public Image imagebox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void UpdateOption(int index)
    {
        base.UpdateOption(index);
        imagebox.sprite = images[index];
    }

}
