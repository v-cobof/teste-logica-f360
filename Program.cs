﻿using System;

namespace teste_logica
{
    internal class Program
    {
        static void Main(string[] args)
        {



            /*Services.removeSpecialCharacters("DINHEIRO.");
            Services.removeSpecialCharacters(".A VISTA,");
            Services.removeSpecialCharacters("PARCELADO-");
            Services.removeSpecialCharacters("DBT%");
            Services.removeSpecialCharacters("CRÉDITO A VISTA");
            Services.removeSpecialCharacters("DÉBITO");*/

            //Services.returnValues();

            //Console.WriteLine(Services.retornaTaxa(150, 15).ToString("p0"));

            double[] n = {5, 0, 16, 10 , 9, 9, 9, 5, 16, 16, 16, 16, 16};

            double[] r = Services.CalculaMediaEMediana(n);

            foreach(double v in r)
            {
                Console.WriteLine(v);
            }

        }
    }
}