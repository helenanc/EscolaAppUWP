using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EscolaAppUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            escolasList = new List<Models.Escola>();
            medias = new List<double>();
        }

        private string ip = "http://localhost:57495";
        private List<Models.Escola> escolasList;
        private List<double> medias;

        private async void btnInserir_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);

            Models.Escola escola = new Models.Escola();
            escola.Nome = txtNome.Text;
            escola.UF = txtUF.Text;
            escola.CienciasNatureza = Convert.ToDouble(txtNaturezas.Text);
            escola.CienciasHumanas = Convert.ToDouble(txtHumanas.Text);
            escola.LinguagensCodigos = Convert.ToDouble(txtLinguagens.Text);
            escola.Matematica = Convert.ToDouble(txtMat.Text);
            escola.Redacao = Convert.ToDouble(txtRedacao.Text);

            escolasList.Add(escola);

            string s = "=" + JsonConvert.SerializeObject(escolasList);
            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");
            await httpClient.PostAsync("/api/escola/", content);
        }

        private void btnListarEscola_Click(object sender, RoutedEventArgs e)
        {
            lvListar.ItemsSource = null;
            var newList = escolasList.OrderBy(x => x.Nome).ToList();
            foreach (Models.Escola z in newList)
            {
                lvListar.Items.Add("Nome: " + z.Nome +
                    " UF: " + z.UF + " Natureza: " + z.CienciasNatureza +
                    " Humanas: " + z.CienciasHumanas + " Linguagens: " + z.LinguagensCodigos +
                    " Matemática: " + z.Matematica + " Redação: " + z.Redacao);
            }
        }

        private void btnListarUF_Click(object sender, RoutedEventArgs e)
        {
            lvListar.ItemsSource = null;
            foreach (Models.Escola z in (escolasList.OrderBy(x => x.UF).ToList()))
            {
                lvListar.Items.Add("Nome: " + z.Nome +
                    " UF: " + z.UF + " Natureza: " + z.CienciasNatureza +
                    " Humanas: " + z.CienciasHumanas + " Linguagens: " + z.LinguagensCodigos +
                    " Matemática: " + z.Matematica + " Redação: " + z.Redacao);
            }
        }

        private void btnListarMedia_Click(object sender, RoutedEventArgs e)
        {
            /*lvListar.ItemsSource = null;
            List<double> newList = medias.OrderByDescending(x => x).ToList();
            for (int i = 0; i <= medias.Count; i++)
            {
                if(medias[i] == newList[i])
                {
                    for(int x = 0; x <= medias.Count; x++)
                    {
                        lvListar.Items.Add("Nome: " + escolasList[x].Nome + " Média: " + newList[i] +
                   " UF: " + escolasList[x].UF + " Natureza: " + escolasList[x].CienciasNatureza +
                   " Humanas: " + escolasList[x].CienciasHumanas + " Linguagens: " + escolasList[x].LinguagensCodigos +
                   " Matemática: " + escolasList[x].Matematica + " Redação: " + escolasList[x].Redacao);
                    }
                }
            }*/
        }

        private void btnListarArea_Click(object sender, RoutedEventArgs e)
        {
            lvListar.ItemsSource = null;
            foreach (Models.Escola z in ListaPorArea())
            {
                lvListar.Items.Add("Nome: " + z.Nome +
                    " UF: " + z.UF + " Natureza: " + z.CienciasNatureza +
                    " Humanas: " + z.CienciasHumanas + " Linguagens: " + z.LinguagensCodigos +
                    " Matemática: " + z.Matematica + " Redação: " + z.Redacao);
            }
        }

        private void CalcularMedia()
        {
            for(int i = 0; i <= escolasList.Count; i++)
            {
                medias.Add((escolasList[i].LinguagensCodigos + escolasList[i].Matematica +
                    escolasList[i].Redacao + escolasList[i].CienciasHumanas +
                    escolasList[i].CienciasNatureza) / 5);
            }
        }

        private List<Models.Escola> ListaPorArea()
        {
            if (cbArea.SelectedItem.ToString() == "Humanas")
                return escolasList.OrderBy(x => x.CienciasHumanas).ToList();
            if (cbArea.SelectedItem.ToString() == "Naturezas")
                return escolasList.OrderBy(x => x.CienciasNatureza).ToList(); 
            if (cbArea.SelectedItem.ToString() == "Matemática")
                return escolasList.OrderBy(x => x.Matematica).ToList();
            if (cbArea.SelectedItem.ToString() == "Linguagens")
                return escolasList.OrderBy(x => x.LinguagensCodigos).ToList();
            if (cbArea.SelectedItem.ToString() == "Redação")
                return escolasList.OrderBy(x => x.Redacao).ToList();
            else return null;
        }

        private void ListarTodas_Click(object sender, RoutedEventArgs e)
        {
            lvListar.ItemsSource = null;
            foreach (Models.Escola z in escolasList)
            {
                lvListar.Items.Add("Nome: " + z.Nome +
                    " UF: " + z.UF + " Natureza: " + z.CienciasNatureza +
                    " Humanas: " + z.CienciasHumanas + " Linguagens: " + z.LinguagensCodigos +
                    " Matemática: " + z.Matematica + " Redação: " + z.Redacao);
            }
        }

        private async void btnDeletar_Click(object sender, RoutedEventArgs e)
        {
            Models.Escola obj = (Models.Escola)lvEscolasDel.SelectedItem;
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            await httpClient.DeleteAsync("/api/escola/" + obj.Id.ToString());
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
    }
