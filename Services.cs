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

		internal static string RemoveCaracteresEspeciais(string s)
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

		
		private static List<string> LeArquivoDeTexto()
        {

			string filePath = @"../../../resources/ExtratoEletronicoGetNet.txt";

			string text = File.ReadAllText(filePath);
			

			string[] lines = File.ReadAllLines(filePath);

			List<string> startsWithOne = new List<string>();

			startsWithOne = lines.Where(x => x.StartsWith('1')).ToList();

			return startsWithOne;

		}

		internal static List<string[]> RetornaVendaEParcelas()
        {
			List<string> data = new(LeArquivoDeTexto());

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

			// Teste da saída
			/*
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
			*/

			return results;
			
        }

		internal static double CalculaTaxa(double venda, double taxa)			
        {
			return (venda / taxa) / 100;
        }

		internal static double[] CalculaMediaEMediana(double[] numeros)
        {
			double[] result = new double[2];
			double aux;
			int auxInt;

			result[0] = numeros.Average();
			Array.Sort(numeros);

			if (numeros.Length % 2 == 0)
            {
				auxInt = numeros.Length / 2;

				result[1] = (numeros[auxInt] + numeros[auxInt - 1]) / 2;

			} 
			else if(numeros.Length % 2 != 0)
            {
				aux = numeros.Length / 2;
				auxInt = (int)Math.Ceiling(aux);

				result[1] = numeros[auxInt];
			}

			return result;
        }



	}
}
