using appSGSales2.Database;
using appSGSales2.Model;
using appSGSales2.Util;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net;
using System.Text;

namespace appSGSales2.Service
{
    public class ServiceWS
    {
        //private static string EnderecoBase = @"http://rosset.microquest.com.br:8081/WebAPI/api";
        private static string EnderecoBase = @"https://softgrafts.ddns.net:15051/api";
        //Este endereço será modoficado de acordo com o sorteio dos ips que Conveyour apresenta
        //private static string EnderecoBase = @"http://169.254.36.101:45455/api"; 
        private GerenciadorDB peddb;
        
        public ServiceWS(GerenciadorDB gerenciadorDB)
        {
            peddb = gerenciadorDB;
        }

        public async static Task<Mensagem> validaUsuario(Login login)
        {
            var URI = new Uri(EnderecoBase + "/Login");

            IEnumerable<KeyValuePair<string, string>> param = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string,string>("login",login.login),
                new KeyValuePair<string,string>("password",login.password)
            };
            var paramJson = new FormUrlEncodedContent(param);
            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = await requisicao.PostAsync(URI, paramJson);
            
            if (resposta.StatusCode == HttpStatusCode.OK)
            {
                var conteudo = await resposta.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Mensagem>(conteudo);
            }
            return null;
        }

        public async Task<List<Pedido>> enviarPedido(List<Pedido> PedidosLista, List<IPedido> ItensPedidos)
        {
            var URI = EnderecoBase + "/Pedido";

            /*
            StringBuilder sb = new StringBuilder();
            sb.Append("{ PedidosLista:[{");
            sb.Append("\"numpede\":1,");
            sb.Append("\"numped\":2,");
            sb.Append("\"codvend\":\"ANA\",");
            sb.Append("\"codcli\":\"BACIO\"");
            sb.Append(" }]}");
            */
            DeviceServices DevServices = new DeviceServices();
            string imei = Preferences.Default.Get("IMEI", "");
            DevServices.IMEI = imei;

            string json = JsonSerializer.Serialize(new { PedidosLista, DevServices, ItensPedidos });

            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = requisicao.PostAsync(URI,stringContent).GetAwaiter().GetResult();

            if ( resposta.StatusCode == HttpStatusCode.OK)
            {
                string lista = resposta.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                if (lista.Length == 2)
                    return null;

                List<Pedido> listaAPI = JsonSerializer.Deserialize<List<Pedido>>(lista);
                List<Pedido> uploadItems = PedidosLista.Where(n => listaAPI.Select(n1 => n1.id_numpede_app).Contains(n.id_numpede_app)).ToList();
                    

                foreach (Pedido item in uploadItems)
                {
                    item.IsSend = true;
                    peddb.envioStatusPedido(item);
                }

                return await peddb.ConsultarPedido();
            }
            return null;
        }

        public async Task<List<Cliente>> listaClienteVendedor(string pCodVend)
        {
            var URI = EnderecoBase + string.Format("/Cliente?pCodVend={0}", pCodVend );

            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = await requisicao.GetAsync(URI);

            if (resposta.StatusCode == HttpStatusCode.OK)
            {

                string lista = await resposta.Content.ReadAsStringAsync();
                List<Cliente> listaCliVend = JsonSerializer.Deserialize<List<Cliente>>(lista);


                return listaCliVend;
            }
            return null;
        }

        public async Task<List<ListaPreco>> listaPreco(string pCodVend)
        {
            var URI = EnderecoBase + string.Format("/ListaPreco?pCodVend={0}", pCodVend);

            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = await requisicao.GetAsync(URI);

            if (resposta.StatusCode == HttpStatusCode.OK)
            {

                string lista = await resposta.Content.ReadAsStringAsync();
                List<ListaPreco> listaCliVend = JsonSerializer.Deserialize<List<ListaPreco>>(lista);

                return listaCliVend;
            }
            return null;
        }

        public async Task<List<Unidade>> listaUnidades()
        {
            var URI = EnderecoBase + "/Unidade";

            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = await requisicao.GetAsync(URI);

            if (resposta.StatusCode == HttpStatusCode.OK)
            {

                string _unidade = await resposta.Content.ReadAsStringAsync();
                List<Unidade> listaUnidades = JsonSerializer.Deserialize<List<Unidade>>(_unidade);

                return listaUnidades;
            }
            return null;
        }

        public async Task<List<Pedido>> listaAPIPedido(Pedido pedido)
        {
            string imei = Preferences.Default.Get("IMEI", "");
            var URI = EnderecoBase + string.Format("/Pedido?pCodVend={0}&pDtEmiss={1}&pImei={2}", pedido.codvend, pedido.DTEMISS.ToString("yyyy-MM-dd"), imei);
            
            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = requisicao.GetAsync(URI).GetAwaiter().GetResult();

            if (resposta.StatusCode == HttpStatusCode.OK)
            {

                string lista = resposta.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                List<Pedido> listaAPI = JsonSerializer.Deserialize<List<Pedido>>(lista);


                return listaAPI;
            }
            return null;
        }

    }
}

