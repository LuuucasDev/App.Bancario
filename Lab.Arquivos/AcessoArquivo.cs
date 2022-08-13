using Lab.Models;
using System;
using System.IO;

namespace Lab.Arquivos
{
    public class AcessoArquivo
    {
        static string path = @"D:\Documentos\Projetos\Impacta\CSharp\Capitulo12.Labs";

        //método para gravar o extrato
        public static void GerarExtrato<T>(T conta) where T: Conta
        {
            try
            {
                if (!Directory.Exists(path + @"\Extrato"))
                {
                    Directory.CreateDirectory(path + @"\Extrato");
                }
                string arquivo = "extrato_" + $"{DateTime.Now:yyyyMMddHHmmsss}.txt";
                File.WriteAllText(path + @"\Extrato\" + arquivo, conta.MostrarExtrato());
            }
            catch
            {
                throw;
            }
        }

        //método para gravar o arquivo de log
        public static void GerarLog(string erro)
        {
            try
            {
                if (!Directory.Exists(path + @"\Log"))
                {
                    Directory.CreateDirectory(path + @"\Log");
                }
                StreamWriter writer = new StreamWriter(path + @"\Log\Erros.log", true);
                writer.WriteLine($"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] - {erro}");
                writer.Close();
            }
            catch
            {
                throw;
            }
        }

        //método para abrir um extrato selecionado
        public static string AbrirExtrato(string caminho)
        {
            try
            {
                StreamReader reader = new StreamReader(caminho);
                return reader.ReadToEnd();
            }
            catch
            {
                throw;
            }
        }

        //método para abrir e apresnetar o log de erros
        public static string AbrirLog()
        {
            try
            {
                StreamReader reader = new StreamReader(path + @"\Log\Erros.log");
                return reader.ReadToEnd();
            }
            catch
            {
                throw;
            }
        }
    }
}
