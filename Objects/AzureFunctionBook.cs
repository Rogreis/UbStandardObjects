using System;
using System.Collections.Generic;
using System.Text;

namespace UbStandardObjects.Objects
{
    /// <summary>
    /// Export a book for Azure Function
    /// </summary>
    public class AzureFunctionBook : BootstrapBook
    {
        public AzureFunctionBook(Parameters parameters) : base(parameters)
        {
        }

        //public void MainPage(StringBuilder sb, short paperNo)
        //{
        //    sb.AppendLine("<!DOCTYPE html> ");
        //    sb.AppendLine("<html> ");
        //    sb.AppendLine(" ");
        //    sb.AppendLine("<head>  ");
        //    sb.AppendLine("    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=windows-1252\"> ");
        //    sb.AppendLine("    <title>Paper 1</title> ");
        //    sb.AppendLine("    <meta charset=\"utf-8\">  ");
        //    sb.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">  ");
        //    sb.AppendLine("    <link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/css/bootstrap.min.css\" rel=\"stylesheet\">   ");
        //    sb.AppendLine("    <script src=\"https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/js/bootstrap.bundle.min.js\"></script>  ");
        //    sb.AppendLine("    <script src=\"https://cdn.jsdelivr.net/npm/jquery@3.6.1/dist/jquery.min.js\"></script>   ");
        //    sb.AppendLine("    <link href=\"css/tub_pt_br.css\" rel=\"stylesheet\">    ");
        //    sb.AppendLine("    <script src=\"js/tub_pt_br.js\"></script>  ");
        //    sb.AppendLine("</head>  ");
        //    sb.AppendLine(" ");
        //    sb.AppendLine("<body class=\"bg-dark text-white\" onload=\"StartPage()\"> ");
        //    sb.AppendLine("<base target=\"_blank\">  ");
        //    sb.AppendLine("<div class=\"container-fluid mt-5 bg-dark text-white\">  ");
        //    sb.AppendLine(" ");
        //    sb.AppendLine("    <div class=\"container-fluid p-5 bg-primary text-white text-center\">  ");
        //    sb.AppendLine($"        <h1>O Livro de Urântia - Documento {paperNo}</h1>   ");
        //    sb.AppendLine("        <p>PT-BR Online Version</p>");
        //    sb.AppendLine($"        <a class=\"page-link\" href=\"https://sxs.urantia.org/en/pt/papers/{paperNo:000}\">Urantia Foundation Multi Language</a>   ");
        //    sb.AppendLine("    </div>  ");
        //    sb.AppendLine(" ");
        //    sb.AppendLine("<div class=\"container-fluid mt-5 \">  ");
        //    sb.AppendLine("    <div class=\"row\">  ");
        //    sb.AppendLine(" ");
        //    sb.AppendLine("        <div id=\"leftColumn\" class=\"col-sm-3 black\"> <!-- Start left column --> ");
        //    sb.AppendLine("        <h3>Index</h3>  ");
        //    sb.AppendLine("        </div> <!-- End left column --> ");
        //    sb.AppendLine(" ");
        //    sb.AppendLine("        <div id= \"rightColumn\" class=\"col-sm-9 black\"> <!-- Start text column --> ");
        //    sb.AppendLine("        </div> <!-- End text column --> ");
        //    sb.AppendLine(" ");
        //    sb.AppendLine("    </div>  <!-- End row --> ");
        //    sb.AppendLine("</div>  <!-- End left column --> ");
        //    sb.AppendLine(" ");
        //    sb.AppendLine("</BODY> ");
        //    sb.AppendLine("</HTML> ");
        //}


    }
}
