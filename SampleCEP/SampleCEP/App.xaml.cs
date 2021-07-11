using SampleCEP.Backend.Repository;
using SampleCEP.Frontend.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleCEP.Comum
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MinhasConfiguracoes();
            MainPage = new NavigationPage(new MenuPage());
        }

        private void MinhasConfiguracoes()
        {            
            var MinhaConfiguracaoDeConexao = new Conexao();
            MinhaConfiguracaoDeConexao.IniciarBanco();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }

}
