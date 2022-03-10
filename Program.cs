using System;

namespace teste_logica
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //---- Teste para a 2 ----- //

            /*
            var results = Services.RetornaVendaEParcelas();
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

            // ----- Teste para a 6 ----- //

            /*
            string usuarios = @"../../../resources/usuarios.txt";
            string usuariosSistema = @"../../../resources/usuariosSistema.txt";

            if (!File.Exists(usuariosSistema))
            {
                Services.LerUsuariosEspacoConsumido();
            }

            Services.GerarRelatorioCompleto(usuarios, "txt");
            Services.GerarRelatorioCompletoOrdenado(usuarios, "html", 3);
            */
        }
    }
}