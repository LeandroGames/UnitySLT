using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class regRow : MonoBehaviour {


	public Text id;
	public Text line;
	public Text bet;
	public Text prev;
	public Text curr;
	public Text paid;
	public Text sort;
	public Text date;
	public Text prize;


	public void reg (
		string _id,
		string _line,
		string _bet,
		string _prev,
		string _cur,
		string _paid,
		string _sort,
		string _date,
		string _prize ) 
	{
		id.text = _id;
		line.text = _line;
		bet.text = _bet;
		prev.text = _prev;
		curr.text = _cur;
		paid.text = _paid;
		sort.text = _sort;
		date.text = _date;
		prize.text = _prize;
	}
	

}
