using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    // Come si dichiara un Generic?
    // Dichiarare un Generic vuol dire comunque vada dentro l’array
	// myObjectArray potrebbe avere solamente 1 tipi, non object che li accetta tutti.
    internal class MioGenerico<TValue>
	{
		// Come si dichiara un array di string?
		private string[] myStringArray = new string[10];
		private TValue[] myObjectArray = new TValue[10];

		public void Add(string nome, TValue valore)
		{
			myStringArray[0] = nome;
			myObjectArray[0] = valore;
        }
	}
}