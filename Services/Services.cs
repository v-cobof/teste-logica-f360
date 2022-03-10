namespace teste_logica
{
	public class Services
	{

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


			return s;
		}


		public static List<string[]> RetornaVendaEParcelas()
		{
			string[] lines = File.ReadAllLines(@"../../../resources/ExtratoEletronicoGetNet.txt");

			List<string> dados = new();

			dados = lines.Where(x => x.StartsWith('1')).ToList();

			List<string[]> results = new();

			string valorVenda, qtdParcelas;

			foreach (string line in dados)
			{
				string[] pairOfResults = new string[2];

				valorVenda = line.Substring(85, 11);
				qtdParcelas = line.Substring(173, 2);

				pairOfResults[0] = valorVenda;
				pairOfResults[1] = qtdParcelas;

				results.Add(pairOfResults);
			}

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
			else if (numeros.Length % 2 != 0)
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


		private static List<List<string>> ExtrairDados(string caminhoArquivo)
		{
			List<string> dados = (File.ReadAllLines(caminhoArquivo)).ToList();
			List<List<string>> dadosSemEspaco = new();

			foreach (string s in dados)
			{
				List<string> parts = new(s.Split(" "));
				parts.RemoveAll(x => string.IsNullOrWhiteSpace(x));

				dadosSemEspaco.Add(parts);

			}
			return dadosSemEspaco;
		}

		public static List<double> ConverterBytesEmMB(string caminhoArquivo)
		{

			List<List<string>> dados = ExtrairDados(caminhoArquivo);
			List<double> valoresEmMB = new();

			foreach (List<string> linha in dados)
			{
				double valorEmBytes = Convert.ToDouble(linha[1]);

				valoresEmMB.Add(valorEmBytes / 1048576);
			}

			return valoresEmMB;
		}

		public static List<double> CalcularPorcentagem(string caminhoArquivo)
		{
			List<double> valores = ConverterBytesEmMB(caminhoArquivo);
			List<double> porcentagens = new();

			double soma = valores.Sum();

			foreach (double valor in valores)
			{
				porcentagens.Add(valor / soma);
			}

			return porcentagens;
		}

		private static void GerarRelatorio(List<List<string>> matriz, List<double> valores, List<double> porcentagens, string formato, int nPessoas = 0)
		{
			nPessoas = (nPessoas == 0 || nPessoas > valores.Count) ? valores.Count : nPessoas;

			string fileName = "relatorio";
			string filePath = @$"../../../reports/{fileName}.{formato}";

			// o padding que eu coloco depende também do tamanho da string, e do espaço que está entre as chaves
			string colunasRelatorio = String.Format("{0,-4} {1,-14} {2,-20} {3,-10}", "Nr.", "Usuário", "Espaço utilizado", "% do uso");

			string[] cabecalhoRelatorio =
			{
				"ACME Inc.           Uso do espaço em disco pelos usuários",
				"-------------------------------------------------------------------",
				$"{colunasRelatorio}\n"
			};

			string[] conteudoRelatorio = new string[valores.Count];

			string[] rodapeRelatorio = new string[2];

			for (int i = 0; i < nPessoas; i++)
			{
				conteudoRelatorio[i] = String.Format("{0, -4} {1,-9} {2, 13:n2} {3, 2} {4, 16:p2}", i + 1, matriz[i][0], valores[i], "MB", porcentagens[i]);
			}

			string espaco = formato == "html" ? "\n\n" : "\n";

			rodapeRelatorio[0] = $"{espaco}Espaço total ocupado: {valores.Take(nPessoas).Sum():n2} MB";
			rodapeRelatorio[1] = $"Espaço médio ocupado: {valores.Take(nPessoas).Average():n2} MB";

			string[] relatorio = cabecalhoRelatorio.Concat(conteudoRelatorio).Concat(rodapeRelatorio).ToArray();

			if (formato == "html")
			{
				for (int i = 0; i < relatorio.Length; i++)
				{
					relatorio[i] = "<pre>" + relatorio[i] + "</pre>";
				}
			}

			File.WriteAllLines(filePath, relatorio);
		}



		public static void GerarRelatorioCompleto(string caminhoArquivo, string formato, int nPessoas = 0)
		{

			GerarRelatorio(ExtrairDados(caminhoArquivo), ConverterBytesEmMB(caminhoArquivo), CalcularPorcentagem(caminhoArquivo), formato, nPessoas);

		}

		public static void GerarRelatorioCompletoOrdenado(string caminhoArquivo, string formato, int nPessoas = 0)
		{

			var matriz = ExtrairDados(caminhoArquivo);
			List<double> valores = ConverterBytesEmMB(caminhoArquivo);
			List<double> porcentagens = CalcularPorcentagem(caminhoArquivo);

			matriz = matriz.OrderByDescending(x => x.ElementAt(1).Length).ThenByDescending(x => x.ElementAt(1)).ToList();
			valores = valores.OrderByDescending(x => x).ToList();
			porcentagens = porcentagens.OrderByDescending(x => x).ToList();

			GerarRelatorio(matriz, valores, porcentagens, formato, nPessoas);
		}

	
		private static long GetDirectorySize(string p)
		{
			
			string[] a = Directory.GetFiles(p, "*.*");

			
			long b = 0;
			foreach (string name in a)
			{
				
				FileInfo info = new FileInfo(name);
				b += info.Length;
			}
			
			return b;
		}

		// só funciona executando como administrador, e os valores da memória não estão corretos
		public static void LerUsuariosEspacoConsumido()
		{

			string fileName = "usuariosSistema";
			string filePath = @$"../../../resources/{fileName}.txt";

			DirectoryInfo directoryInfo = new DirectoryInfo(@"C:\Users");

			DirectoryInfo[] dirInfos = directoryInfo.GetDirectories();

			dirInfos = dirInfos.Where(x => x.Name != "Default User" && x.Name != "All Users" && x.Name != "Usuário Padrão" && x.Name != "Todos os Usuários").ToArray();

			string[] users = new string[dirInfos.Length];

			for (int i = 0; i < dirInfos.Length; i++)
			{
				users[i] = string.Format("{0, -10} {1, 10}", dirInfos[i].Name, GetDirectorySize(dirInfos[i].FullName));
			}

			File.WriteAllLines(filePath, users);
		}

	}
}