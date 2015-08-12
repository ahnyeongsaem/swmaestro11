using UnityEngine;
using System.Collections;

public class pokeralgorithm : MonoBehaviour {

	public static float cardXcha=10f;
	public static float cardY=10f;
	public class cardinfo
	{
		public int type;
		public int number;
		public GameObject gameobject;
		public GameObject cardchangebutton;
	};
	public static GameObject[,] cardlist=new GameObject[4,15];
	public cardinfo[] card=new cardinfo[5];

	//0spade 1diamond 2heart 3club , 1~13index not 0index

	// Use this for initialization
	void Start () {
		cardlist[0,1]=GameObject.Find ("Card_spade_" + "A" + "_");
		for(int i=2;i<=10;i++) cardlist[0,i]=GameObject.Find("Card_spade_"+i+"_"); //spade
		cardlist[0,11]=GameObject.Find ("Card_spade_" + "J" + "_");	cardlist[0,12]=GameObject.Find ("Card_spade_" + "Q" + "_");
		cardlist[0,13]=GameObject.Find ("Card_spade_" + "K" + "_");	cardlist[0,14]=GameObject.Find ("Card_spade_" + "A" + "_");
		cardlist[1,1]=GameObject.Find ("Card_diamond_" + "A" + "_");
		for(int i=2;i<=10;i++) cardlist[1,i]=GameObject.Find("Card_diamond_"+i+"_"); //diamond
		cardlist[1,11]=GameObject.Find ("Card_diamond_" + "J" + "_");	cardlist[1,12]=GameObject.Find ("Card_diamond_" + "Q" + "_");
		cardlist[1,13]=GameObject.Find ("Card_diamond_" + "K" + "_");	cardlist[1,14]=GameObject.Find ("Card_diamond_" + "A" + "_");
		cardlist[2,1]=GameObject.Find ("Card_heart_" + "A" + "_");
		for(int i=2;i<=10;i++) cardlist[2,i]=GameObject.Find("Card_heart_"+i+"_"); //heart
		cardlist[2,11]=GameObject.Find ("Card_heart_" + "J" + "_");	cardlist[2,12]=GameObject.Find ("Card_heart_" + "Q" + "_");
		cardlist[2,13]=GameObject.Find ("Card_heart_" + "K" + "_");	cardlist[2,14]=GameObject.Find ("Card_heart_" + "A" + "_");
		cardlist[3,1]=GameObject.Find ("Card_club_" + "A" + "_");
		for(int i=2;i<=10;i++) cardlist[3,i]=GameObject.Find("Card_club_"+i+"_"); //club
		cardlist[3,11]=GameObject.Find ("Card_club_" + "J" + "_");	cardlist[3,12]=GameObject.Find ("Card_club_" + "Q" + "_");
		cardlist[3,13]=GameObject.Find ("Card_club_" + "K" + "_");	cardlist[3,14]=GameObject.Find ("Card_club_" + "A" + "_");
		for(int i=0;i<4;i++)
		{
			for(int j=1;j<=13;j++)
			{
				cardlist[i,j].SetActive(false);
			}
		}

		for(int i=0;i<5;i++)
		{
			card[i]=new cardinfo();
			card[i].cardchangebutton=GameObject.Find("ChangeCardButton" + i);
			GameObject.Find("ChangeCardButton" + i).SetActive (false);
		}



	}

	void allcarddisable()
	{
		for (int i=0; i<4; i++) {
			for(int j=0;j<=14;j++)
			{
				if(cardlist[i,j]!=null)
				{
					cardlist[i,j].SetActive(false);
				}
			}
		}
	}

	bool onecarddraw(int index)
	{
		int randomtmp1,randomtmp2;
		randomtmp1=Random.Range(0,4);
		randomtmp2=Random.Range(1,14);
		
		if(cardlist[randomtmp1,randomtmp2].activeSelf==false)
		{
			if(card[index].gameobject!=null)
				cardlist[randomtmp1,randomtmp2].transform.position=card[index].gameobject.transform.position;
			card[index].type=randomtmp1;
			card[index].number=randomtmp2;
			card[index].gameobject=cardlist[randomtmp1,randomtmp2];
			cardlist[randomtmp1,randomtmp2].SetActive(true);
			return true;
		}
		return false;
	}

	public void draw()
	{
		for(int i=0;i<5;i++)
		{
			while(!onecarddraw(i));
		}
		for (int i=0; i<5; i++) {
			card [i].gameobject.GetComponent<RectTransform>().anchoredPosition = new Vector3 ((i - 2) * cardXcha, 0 , cardY);
			card[i].cardchangebutton.SetActive(true);
			card[i].cardchangebutton.GetComponent<RectTransform>().anchoredPosition=new Vector3 ((i - 2) * cardXcha,0 , cardY-2f);
		}

	}

	public void cardchange(int index)
	{
		card [index].gameobject.SetActive (false);
		while(!onecarddraw(index));
		card[index].cardchangebutton.SetActive (false);
	}

	int pokercheck()
	{
		for(int i=0;i<5;i++)//sort
		{
			for(int j=0;j<4;j++)
			{
				if(card[i].number>card[i+1].number ||
				   (card[i].number==card[i+1].number && card[i].type>card[i+1].type))
				{
					cardinfo swapcardtmp=card[i];
					card[i]=card[i+1];
					card[i+1]=swapcardtmp;
				}
			}
		}
		//rsf
		//streight flush
		if (card [0].type == card [1].type &&
			card [0].type == card [2].type &&
			card [0].type == card [3].type &&
			card [0].type == card [4].type &&
			card [1].number == card [2].number - 1 &&
			card [2].number == card [3].number - 1 &&
			card [3].number == card [4].number - 1 &&
			(card [0].number == card [1].number - 1 
			|| (card [0].number == 1 && card [4].number == 13))) {
			return 8;
		}
		//fcard
		else if ((card [0].number == card [1].number ||
		          card [3].number == card[4].number) &&
			card [1].number == card [2].number &&
			card [2].number == card [3].number) 
		{
			return 7;
		}
		//fullhouse
		else if( (card[0].number==card[1].number &&
		          card[2].number==card[3].number && card[3].number==card[4].number) ||
		          card[0].number==card[1].number && card[1].number==card[2].number &&
		          card[3].number==card[4].number
		        )
		{
			return 6;
		}
		//flash
		else if(card[0].type==card[1].type &&
		        card[0].type==card[2].type &&
		        card[0].type==card[3].type &&
		        card[0].type==card[4].type)
		{
			return 5;
		}
		//st
		//triple
		//two pair
		//one pair
		return -1;
	}

	// Update is called once per frame
	void Update () {

	}
}
