using System;

namespace teste_logica
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string usuarios = @"../../../resources/usuarios.txt";

            Services.LerUsuariosEspacoConsumido();

            string usuariosSistema = @"../../../resources/usuariosSistema.txt";

            Services.GerarRelatorioCompleto(usuariosSistema, "txt");
            //Services.GerarRelatorioCompleto(usuarios, "txt", 2);

            //Services.GerarRelatorioCompletoOrdenado(usuarios, "txt");
        }
    }
}