using Lab.Arquivos;
using Lab.Models;
using Lab.Models.Delegates;
using Lab.Models.Utils;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            sexoComboBox.Items.Add(Sexos.Masculino);
            sexoComboBox.Items.Add(Sexos.Feminino);
            sexoComboBox.SelectedIndex = 0;

            operacaoComboBox.Items.Add(Operacoes.Deposito);
            operacaoComboBox.Items.Add(Operacoes.Saque);
            operacaoComboBox.SelectedIndex = 0;
        }

        private void incluirClienteButton_Click(object sender, 
            RoutedEventArgs e)
        {
            try
            {
                //obtendo os dados do endereço
                Endereco endereco = new Endereco(
                    ruaTextBox.Text,
                    int.Parse(numeroTextBox.Text),
                    cidadeTextBox.Text,
                    cepTextBox.Text);

                //obtendo os dados do cliente
                int digitos = documentoTextBox.Text.Length;
                Cliente cliente;

                if (digitos == 11)
                {
                    cliente = new ClientePF(
                        documentoTextBox.Text,
                        nomeTextBox.Text,
                        (Sexos)sexoComboBox.SelectedItem,
                        int.Parse(idadeTextBox.Text),
                        endereco);
                }
                else if (digitos == 14)
                {
                    cliente = new ClientePJ(
                        documentoTextBox.Text,
                        nomeTextBox.Text,
                        (Sexos)sexoComboBox.SelectedItem,
                        int.Parse(idadeTextBox.Text),
                        endereco);

                }
                else
                {
                    throw new Exception("Documento inválido");
                }

                //adicionando o novo cliente na lista
                Metodos.AdicionarCliente(cliente);

                //vinculando a lista de clientes ao
                //componente ComboBox
                clienteComboBox.ItemsSource = Metodos.ListarClientes();
                MessageBox.Show("Cliente incluído com sucesso!");
            }
            catch (Exception ex)
            {
                AcessoArquivo.GerarLog(ex.Message);
                MessageBox.Show(ex.Message, 
                    "Erro", 
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);                
            }


            //MessageBox.Show(cliente.Exibir());
        }


        private bool VerificarEspecial { get; set; }

        private void especialRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                var radio = sender as RadioButton;
                VerificarEspecial = (radio == especialRadioButton);

                limiteLabel.IsEnabled = VerificarEspecial;
                limiteTextBox.IsEnabled = VerificarEspecial;
            }
            catch (Exception ex)
            {
                AcessoArquivo.GerarLog(ex.Message);
                MessageBox.Show(ex.Message,
                    "Erro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void incluirContaButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Conta conta;
                var cliente = (Cliente)clienteComboBox.SelectedItem;

                int banco = int.Parse(bancoTextBox.Text);
                string agencia = agenciaTextBox.Text;
                string numConta = contaTextBox.Text;

                if (VerificarEspecial)
                {
                    conta = new ContaEspecial(banco, agencia, numConta);
                    ((ContaEspecial)conta).Limite = double.Parse(limiteTextBox.Text);
                }
                else
                {
                    conta = new ContaCorrente(banco, agencia, numConta);
                }
                //vinculamos o cliente selecionado à nova conta
                conta.ClienteInfo = cliente;

                //adicionamos a nova conta à lista de contas do 
                //cliente selecionado
                cliente.Contas.Add(conta);

                //incluimos a nova conta à lista global de contas
                Metodos.AdicionarConta(conta);

                //vinculamos a lista global de contas ao
                //componente ComboBox
                numeroContaComboBox.ItemsSource = Metodos.ListarContas();

                MessageBox.Show("Conta adicionada com sucesso!");
            }
            catch (Exception ex)
            {
                AcessoArquivo.GerarLog(ex.Message);
                MessageBox.Show(ex.Message,
                    "Erro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void executarButton_Click(object sender, 
            RoutedEventArgs e)
        {
            try
            {
                //obtendo a conta selecionada
                var conta = (Conta)numeroContaComboBox.SelectedItem;

                //obtendo a operação
                var operacao = (Operacoes)operacaoComboBox.SelectedItem;

                //obtendo o valor da operação
                double valor = double.Parse(valorTextBox.Text);

                //executar a operação
                conta.EfetuarOperacao(valor, operacao);

                MessageBox.Show("Operação realizada com sucesso!");
                valorTextBox.Clear();
            }
            catch (Exception ex)
            {
                AcessoArquivo.GerarLog(ex.Message);
                MessageBox.Show(ex.Message,
                    "Erro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

        }

        private void extratoButton_Click(object sender, 
            RoutedEventArgs e)
        {
            try
            {
                var conta = (Conta)numeroContaComboBox.SelectedItem;

                extratoTextBox.Text = conta.MostrarExtrato();
            }
            catch (Exception ex)
            {
                AcessoArquivo.GerarLog(ex.Message);
                MessageBox.Show(ex.Message,
                    "Erro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void buscarButton_Click(object sender, RoutedEventArgs e)
        {
            ListaElementos<Cliente> lista = new ListaElementos<Cliente>();
            lista.CriarElementos(Metodos.ListarClientes().ToArray());

            var item = lista.Buscar(p => p.Nome.Contains(buscaTextBox.Text));

            resultadoListBox.Items.Clear();
            resultadoListBox.Items.Add(item);

        }

        private void listarButton_Click(object sender, RoutedEventArgs e)
        {
            ListaElementos<Cliente> lista = new ListaElementos<Cliente>();
            lista.CriarElementos(Metodos.ListarClientes().ToArray());

            var itens = lista.Listar(p => p.Nome.Contains(buscaTextBox.Text));
            
            resultadoListBox.Items.Clear();
            resultadoListBox.ItemsSource = itens;
        }

        private void salvarExtratoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var conta = (Conta)numeroContaComboBox.SelectedItem;
                AcessoArquivo.GerarExtrato<Conta>(conta);
                MessageBox.Show("Extrato armazenado com sucesso!");
            }
            catch (Exception ex)
            {
                AcessoArquivo.GerarLog(ex.Message);
                MessageBox.Show(ex.Message,
                    "Erro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void abrirExtratoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Arquivos txt (*.txt)|*.txt";
                dialog.InitialDirectory = 
                    @"D:\Documentos\Projetos\Impacta\CSharp\Capitulo12.Labs\Extrato";

                if (dialog.ShowDialog() == true)
                {
                    arquivoTextBox.Text = File.ReadAllText(dialog.FileName);
                }
            }
            catch (Exception ex)
            {
                AcessoArquivo.GerarLog(ex.Message);
                MessageBox.Show(ex.Message,
                    "Erro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void abrirLogButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string arquivo =
                    @"D:\Documentos\Projetos\Impacta\CSharp\Capitulo12.Labs\Log\Erros.log";
                arquivoTextBox.Text = File.ReadAllText(arquivo);
            }
            catch (Exception ex)
            {
                AcessoArquivo.GerarLog(ex.Message);
                MessageBox.Show(ex.Message,
                    "Erro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
