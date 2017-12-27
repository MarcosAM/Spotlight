using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelKeeper : MonoBehaviour {

	public Text p1Text;
	public Text p2Text;
	public Text p3Text;
	public Text p4Text;

	public Image p1Image;
	public Image p2Image;
	public Image p3Image;
	public Image p4Image;

	public void RefreshPanels (int first, int second,int third, int forth, Color color1, Color color2, Color color3, Color color4){
		DontDestroyOnLoad(gameObject);
		p1Text.text = first.ToString();
		p2Text.text = second.ToString();
		p3Text.text = third.ToString();
		p4Text.text = forth.ToString();
		p1Image.color = color1;
		p2Image.color = color2;
		p3Image.color = color3;
		p4Image.color = color4;
	}
}
