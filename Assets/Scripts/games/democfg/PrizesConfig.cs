/*
 *  Leandro M. da Costa - 03/2017
 *  
 *  Slots Unity Projec 
 *  
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PrizesConfig 
{
	// _0;// Fantasma
	// _1;// Monstro
	// _2;// Vampiro
	// _3;// Bruxa
	// _4;// Espantalho
	// _5;// Caldeirão
	// _6;// Vassoura
	// _7;// Mago
	// _8;// Capeta
	// _9; // Abobora
	public Prize _prize0 = new Prize(0);
	public Prize _prize1 = new Prize(1);
	public Prize _prize2 = new Prize(2);
	public Prize _prize3 = new Prize(3);
	public Prize _prize4 = new Prize(4);
	public Prize _prize5 = new Prize(5);
	public Prize _prize6 = new Prize(6);
	public Prize _prize7 = new Prize(7);
	public Prize _prize8 = new Prize(8);
	public Prize _prize9 = new Prize(9);

	public PrizesConfig() {
		_prize0.addPrize ("11111", 100);
		_prize0.addPrize ("11110", 50);
		_prize0.addPrize ("01111", 50);
		_prize0.addPrize ("11100", 5);
		_prize0.addPrize ("00111", 5);

		_prize1.addPrize ("11111", 200);
		_prize1.addPrize ("11110", 100);
		_prize1.addPrize ("01111", 100);
		_prize1.addPrize ("11100", 2);
		_prize1.addPrize ("00111", 2);

		_prize2.addPrize ("11111", 150);
		_prize2.addPrize ("11110", 75);
		_prize2.addPrize ("01111", 75);
		_prize2.addPrize ("11100", 5);
		_prize2.addPrize ("00111", 5);

		_prize3.addPrize ("11111", 300);
		_prize3.addPrize ("11110", 150);
		_prize3.addPrize ("01111", 150);
		_prize3.addPrize ("11100", 10);
		_prize3.addPrize ("00111", 10);

		_prize4.addPrize ("11111", 250);
		_prize4.addPrize ("11110", 120);
		_prize4.addPrize ("01111", 120);
		_prize4.addPrize ("11100", 5);
		_prize4.addPrize ("00111", 5);

		_prize5.addPrize ("BBBBB", 0);
		_prize5.addPrize ("BBBB0", 0);
		_prize5.addPrize ("0BBBB", 0);
		_prize5.addPrize ("00BBB", 0);


		_prize6.addPrize ("11111", 300);
		_prize6.addPrize ("11110", 200);
		_prize6.addPrize ("01111", 200);
		_prize6.addPrize ("11100", 50);
		_prize6.addPrize ("00111", 50);

		_prize7.addPrize ("11111", 300);
		_prize7.addPrize ("11110", 250);
		_prize7.addPrize ("01111", 250);
		_prize7.addPrize ("11100", 50);
		_prize7.addPrize ("00111", 50);

		_prize8.addPrize ("AAAAA", 0);
		_prize8.addPrize ("11110", 500);
		_prize8.addPrize ("01111", 500);
		_prize8.addPrize ("11100", 100);
		_prize8.addPrize ("00111", 100);

		_prize9.addPrize ("11111", 700);
		_prize9.addPrize ("11110", 300);
		_prize9.addPrize ("01111", 300);
		_prize9.addPrize ("11100", 75);
		_prize9.addPrize ("00111", 75);


	}
}

