using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bot_Application1.Dialogs
{

    [Serializable]
    [LuisModel("IDLUIS", "PASSWORDLUIS")]
    public class SeriesDialog : LuisDialog<object>
    {
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Desculpe, não consegui entender a frase {result.Query}");
        }
        [LuisIntent("Sobre")]
        public async Task Sobre(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Eu sou o Séries Bot e estou aqui para tirar todas as suas dúvidas sobre séries.");
        }
        [LuisIntent("Cumprimento")]
        public async Task Cumprimento(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Olá! Como posso te ajudar?");
        }
        [LuisIntent("Serie")]
        public async Task Serie(IDialogContext context, LuisResult result)
        {
            var series = result.Entities?.Select(e => e.Entity);
            /* usar com API*/
            var filtro = string.Join(",", series.ToArray());
            var endpoint = $"https://vejoseriesapi.azurewebsites.net/Series/Busca?search={filtro}&usuario_id=0&page=1&pageSize=1";
            await context.PostAsync("Aguarde um momento, enquanto eu procuro as informações...");

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(endpoint);
                if (!response.IsSuccessStatusCode)
                {
                    await context.PostAsync("Ocorreu um erro... Tente mais tarde");
                    return;
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var resultado = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.PesquisaDTO>(json);
                    var tvserie = resultado.SeriesDTO.FirstOrDefault();

                    HeroCard card = new HeroCard
                    {
                        Title = tvserie.Nome,
                        Subtitle = tvserie.Descricao

                    };

                    var url = $"http://www.vejoseries.com/series/{tvserie.Path.Trim()}";
                    card.Buttons = new List<CardAction>
                    {
                        new CardAction(ActionTypes.OpenUrl, "Saiba mais", value:url)
                    };

                    card.Images = new List<CardImage>
                    {
                        new CardImage(tvserie.Img)
                    };
                    Activity resposta = ((Activity)context.Activity).CreateReply();

                    resposta.Attachments.Add(card.ToAttachment());
                    await context.PostAsync(resposta);
                }
            }
        }
        [LuisIntent("Episodio")]
        public async Task Episodio(IDialogContext context, LuisResult result)
        {
            var series = result.Entities?.Select(e => e.Entity);
            /* usar com API*/
            var filtro = string.Join(",", series.ToArray());
            var endpoint = $"https://vejoseriesapi.azurewebsites.net/Series/Busca?search={filtro}&usuario_id=0&page=1&pageSize=1";
            await context.PostAsync("Aguarde um momento, enquanto eu procuro as informações...");

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(endpoint);
                if (!response.IsSuccessStatusCode)
                {
                    await context.PostAsync("Ocorreu um erro... Tente mais tarde");
                    return;
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var resultado = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.PesquisaDTO>(json);
                    var tvserie = resultado.SeriesDTO.FirstOrDefault();

                    await context.PostAsync($"O último episódio de {tvserie.Nome} foi {tvserie.UltimoEpisodio.ExistenteInfo.Desc_Episodio} - {tvserie.UltimoEpisodio.ExistenteInfo.NomeEpisodio} exibido em {tvserie.UltimoEpisodio.ExistenteInfo.DataUSA?.ToString("dd/MM/yyyy")} ");
                }
            }
        }
    }
}