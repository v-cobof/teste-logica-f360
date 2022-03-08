using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace teste_logica
{
	public class Services
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

		public static string RemoveCaracteresEspeciais(string s)
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


		public static List<string[]> RetornaVendaEParcelas()
        {
			string[] lines = File.ReadAllLines(@"../../../resources/ExtratoEletronicoGetNet.txt");

			List<string> dados = new();

			dados = lines.Where(x => x.StartsWith('1')).ToList();

			List<string[]> results = new();

			string valorVenda, qtdParcelas;

			foreach(string line in dados)
            {
				string[] pairOfResults = new string[2];

				valorVenda = line.Substring(85, 11);
				qtdParcelas = line.Substring(173, 2);

				pairOfResults[0] = valorVenda;
				pairOfResults[1] = qtdParcelas;

				results.Add(pairOfResults);
            }

			// Teste da saída
			
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
			// ----------------------------------

			return results;
			
        }

		public static double CalculaTaxa(double venda, double taxa)			
        {
			return (venda / taxa) / 100;
        }

		public static double[] CalculaMediaEMediana(double[] numeros)
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

		private static bool EhVogal(char charValue)
		{
			char[] vowelList = { 'a', 'e', 'i', 'o', 'u' };

			char casedChar = char.ToLower(charValue);

			foreach (char vowel in vowelList)
			{
				if (vowel == casedChar)
				{
					return true;
				}
			}

			return false;
		}

		public static string InverteEAddEspacoAposVogal(string palavra)
        {
            char[] array = palavra.ToCharArray();
			Array.Reverse(array);

			List<char> list = array.ToList();
			
			for (int i = 0; i < list.Count; i++)
            {
                if (EhVogal(list[i]))
                {
					list.Insert(i + 1, ' ');
                }
            }

			array = list.ToArray();

			return new string(array);
        }


		private static List<List<string>> RemoverEspacos(string filePath)
        {
			List<string> dados = (File.ReadAllLines(filePath)).ToList();
			List<List<string>> dadosSemEspaco = new();

			foreach (string s in dados)
			{
				List<string> parts = new(s.Split(" "));
				parts.RemoveAll(x => string.IsNullOrWhiteSpace(x));

				dadosSemEspaco.Add(parts);
				
			}
			return dadosSemEspaco;
		}

		public static List<double> ConverterBytesEmMB()
        {

			List<List<string>> dados = RemoverEspacos(@"../../../resources/usuarios.txt");
			List<double> valoresEmMB = new();

			foreach(List<string> linha in dados)
            {
				double valorEmBytes = Convert.ToDouble(linha[1]);
				
				valoresEmMB.Add(valorEmBytes / 1048576);
            }

			return valoresEmMB;
        }

		public static List<double> CalcularPorcentagem()
        {
			List<double> valores = ConverterBytesEmMB();
			List<double> porcentagens = new();

			double soma = valores.Sum();

			foreach(double valor in valores)
            {
				porcentagens.Add(valor / soma);
            }

			return porcentagens;
		}

		/*public static string Gerarrelatorio()
        {

        }*/

		

	}
}
