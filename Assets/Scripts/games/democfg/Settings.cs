/*
 *  Leandro M. da Costa - 03/2017
 *  
 *  Slots Unity Projec 
 *  
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class Settings : MonoBehaviour {

	public Sprite Icon_0;
	public Sprite Icon_1;
	public Sprite Icon_2;
	public Sprite Icon_3;
	public Sprite Icon_4;
	public Sprite Icon_5;
	public Sprite Icon_6;
	public Sprite Icon_7;
	public Sprite Icon_8;
	public Sprite Icon_9;

	public GameObject visualline1v;
	public GameObject visualline2v;
	public GameObject visualline3v;
	public GameObject visualline4v;
	public GameObject visualline5v;
	public GameObject visualline6v;
	public GameObject visualline7v;
	public GameObject visualline8v;
	public GameObject visualline9v;
	public GameObject visualline10v;
	public GameObject visualline11v;
	public GameObject visualline12v;
	public GameObject visualline13v;
	public GameObject visualline14v;
	public GameObject visualline15v;
	public GameObject visualline16v;
	public GameObject visualline17v;
	public GameObject visualline18v;
	public GameObject visualline19v;
	public GameObject visualline20v;

	Vector4 _Azul = new Vector4 (0, 0, 139, 255);
	Vector4 _Roxo = new Vector4 (148,0,211, 255);
	Vector4 _Rosa = new Vector4 (255,105,180, 255);
	Vector4 _Vermelho = new Vector4 (139,0,0, 255);
	Vector4 _Ciano = new Vector4 (0,139,139, 255);
	Vector4 _Laranja = new Vector4 (255,140,0, 255);
	Vector4 _Verde = new Vector4 (0,128,0, 255);
	Vector4 _Amarelo = new Vector4 (1,215,0, 255);
	Vector4 _Marrom = new Vector4 (160,82,45, 255);
	Vector4 _Cinza = new Vector4 (169,169,169, 255);

	Vector4 _Azul2 = new Vector4 (0,191,255, 255);
	Vector4 _Roxo2 = new Vector4 (138,43,226, 255);
	Vector4 _Rosa2 = new Vector4 (240,128,128, 255);
	Vector4 _Vermelho2 = new Vector4 (255,0,0, 255);
	Vector4 _Ciano2 = new Vector4 (72,209,204, 255);
	Vector4 _Laranja2 = new Vector4 (255,69,0, 255);
	Vector4 _Verde2 = new Vector4 (50,205,50, 255);
	Vector4 _Amarelo2 = new Vector4 (255,215,0, 255);
	Vector4 _Marrom2 = new Vector4 (160,82,45, 255);
	Vector4 _Cinza2 = new Vector4 (160,160,160, 255);

	private float lineSize = 5f;


	void Start () {

		//Valor de Creditos
		Globals.CreditValue = 0.10f;

		Globals.upIcons (0,Icon_0);
		Globals.upIcons (1,Icon_1);
		Globals.upIcons (2,Icon_2);
		Globals.upIcons (3,Icon_3);
		Globals.upIcons (4,Icon_4);
		Globals.upIcons (5,Icon_5);
		Globals.upIcons (6,Icon_6);
		Globals.upIcons (7,Icon_7);
		Globals.upIcons (8,Icon_8);
		Globals.upIcons (9,Icon_9);

		//Animações
		AnimConfig ancfg = new AnimConfig ();
		Globals.addAnim (0, ancfg.anim_0);
		Globals.addAnim (1, ancfg.anim_1);
		Globals.addAnim (2, ancfg.anim_2);
		Globals.addAnim (3, ancfg.anim_3);
		Globals.addAnim (4, ancfg.anim_4);
		Globals.addAnim (5, ancfg.anim_5);
		Globals.addAnim (6, ancfg.anim_6);
		Globals.addAnim (7, ancfg.anim_7);
		Globals.addAnim (8, ancfg.anim_8);
		Globals.addAnim (9, ancfg.anim_9);

		//Premios
		PrizesConfig pzcfg = new PrizesConfig ();
		Globals.Prizes.Add (0,pzcfg._prize0);
		Globals.Prizes.Add (1,pzcfg._prize1);
		Globals.Prizes.Add (2,pzcfg._prize2);
		Globals.Prizes.Add (3,pzcfg._prize3);
		Globals.Prizes.Add (4,pzcfg._prize4);
		Globals.Prizes.Add (5,pzcfg._prize5);
		Globals.Prizes.Add (6,pzcfg._prize6);
		Globals.Prizes.Add (7,pzcfg._prize7);
		Globals.Prizes.Add (8,pzcfg._prize8);
		Globals.Prizes.Add (9,pzcfg._prize9);

		//Linhas
		slotLinesConfig slcfg = new slotLinesConfig ();
		Globals.Lines.Add (1, new Line ( slcfg.l1v,  1, "R", visualline1v, Globals.xCor(_Azul),  lineSize));//DIREITA
		Globals.Lines.Add (2, new Line ( slcfg.l2v,  2, "L", visualline2v, Globals.xCor(_Azul2),  lineSize));
		Globals.Lines.Add (3, new Line ( slcfg.l3v,  3, "L", visualline3v, Globals.xCor(_Roxo2),  lineSize));
		Globals.Lines.Add (4, new Line ( slcfg.l4v,  4, "R", visualline4v, Globals.xCor(_Roxo), lineSize));//DIREITA
		Globals.Lines.Add (5, new Line ( slcfg.l5v,  5, "R", visualline5v, Globals.xCor(_Rosa),  lineSize));//DIREITA
		Globals.Lines.Add (6, new Line ( slcfg.l6v,  6, "L", visualline6v, Globals.xCor(_Rosa2),  lineSize));
		Globals.Lines.Add (7, new Line ( slcfg.l7v,  7, "L", visualline7v, Globals.xCor(_Vermelho2), 	lineSize));
		Globals.Lines.Add (8, new Line ( slcfg.l8v,  8, "L", visualline8v, Globals.xCor(_Ciano2),lineSize));
		Globals.Lines.Add (9, new Line ( slcfg.l9v,  9, "R", visualline9v, Globals.xCor(_Vermelho), lineSize));//DIREITA
		Globals.Lines.Add(10, new Line ( slcfg.l10v, 10,"R", visualline10v, Globals.xCor(_Ciano), lineSize));//DIREITA
		Globals.Lines.Add(11, new Line ( slcfg.l11v, 11,"R", visualline11v, Globals.xCor(_Laranja), lineSize));//DIREITA
		Globals.Lines.Add(12, new Line ( slcfg.l12v, 12,"L", visualline12v, Globals.xCor(_Laranja2), lineSize));
		Globals.Lines.Add(13, new Line ( slcfg.l13v, 13,"L", visualline13v, Globals.xCor(_Verde2), lineSize));
		Globals.Lines.Add(14, new Line ( slcfg.l14v, 14,"L", visualline14v, Globals.xCor(_Amarelo2),lineSize));
		Globals.Lines.Add(15, new Line ( slcfg.l15v, 15,"L", visualline15v, Globals.xCor(_Marrom2), lineSize));
		Globals.Lines.Add(16, new Line ( slcfg.l16v, 16,"R", visualline16v, Globals.xCor(_Verde),lineSize));//DIREITA
		Globals.Lines.Add(17, new Line ( slcfg.l17v, 17,"R", visualline17v, Globals.xCor(_Amarelo),  lineSize));//DIREITA
		Globals.Lines.Add(18, new Line ( slcfg.l18v, 18,"R", visualline18v, Globals.xCor(_Marrom),lineSize));//DIREITA
		Globals.Lines.Add(19, new Line ( slcfg.l19v, 19,"R", visualline19v, Globals.xCor(_Cinza), lineSize));//DIREITA
		Globals.Lines.Add(20, new Line ( slcfg.l20v, 20,"L", visualline20v, Globals.xCor(_Cinza2),  lineSize));

	}
		


}
