using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace teste_logica
{
	internal class Services
	{
		/*
        public static string removeSpecialCharacters(string text) {

            text = text.Replace('É', 'E');
            text = text.Replace('.', ' ').Trim();
            text = text.Replace('-', ' ').Trim();
            text = text.Replace('%', ' ').Trim();
            text = text.Replace(',', ' ').Trim();

            return text;
        }
        */

		internal static string removeSpecialCharacters(string s)
		{

			Dictionary<string, string> Replacements = new Dictionary<string, string>()
			{
				{"É", "E"},
				{".", ""},
				{"-", ""},
				{"%", ""},
				{",", ""}

			};

			foreach (string toReplace in Replacements.Keys)
			{
				s = s.Replace(toReplace, Replacements[toReplace]);
			}

			//Console.WriteLine(s);
			return s;
		}

		
		internal static List<string> readTextFile()
        {

			string filePath = @"../../../resources/ExtratoEletronicoGetNet.txt";

			string text = File.ReadAllText(filePath);
			

			string[] lines = File.ReadAllLines(filePath);

			List<string> startsWithOne = new List<string>();

			startsWithOne = lines.Where(x => x.StartsWith('1')).ToList();

			return startsWithOne;

		}

		internal static List<string[]> returnValues()
        {
			List<string> data = new (readTextFile());

			List<string[]> results = new();

			string valorVenda, qtdParcelas;

			foreach(string line in data)
            {
				string[] pairOfResults = new string[2];

				valorVenda = line.Substring(85, 11);
				qtdParcelas = line.Substring(173, 2);

				pairOfResults[0] = valorVenda;
				pairOfResults[1] = qtdParcelas;

				results.Add(pairOfResults);
            }

			
			Console.WriteLine("{");
			foreach (string[] pair in results)
            {
				
				Console.Write("[");
				Console.Write(pair.ElementAt(0));
				Console.Write(" , ");
				Console.Write(pair.ElementAt(1));
				Console.Write("]");
				

				Console.WriteLine(" , ");


			}
			Console.WriteLine("}");
			
			return results;
			
        }

	}
}
