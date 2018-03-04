using Microsoft.Bot.Builder.CognitiveServices.QnAMaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Threading.Tasks;

namespace Bot_Application1.Dialogs
{
    [Serializable]
    public class QnaDialog : QnAMakerDialog
    {
        public QnaDialog() : base(new QnAMakerService(new QnAMakerAttribute(ConfigurationManager.AppSettings["QnaSubscriptionKey"], ConfigurationManager.AppSettings["QnaKnowledgebaseId"], "Não encontrei sua resposta", 0.5)))
        {

        }

        protected override async Task RespondFromQnAMakerResultAsync(IDialogContext context, IMessageActivity message, QnAMakerResults result)
        {
            var primeiraresposta = result.Answers.First().Answer;

            Activity resposta = ((Activity)context.Activity).CreateReply();
            var dadosResposta = primeiraresposta.Split(';');
            if (dadosResposta.Length == 1)
            {
                await context.PostAsync(primeiraresposta);
                return;
            }

            var titulo = dadosResposta[0];
            var descricao = dadosResposta[1];
            var url = dadosResposta[2];
            var urlImagem = dadosResposta[3];

            HeroCard card = new HeroCard
            {
                Title = titulo,
                Subtitle = descricao

            };

            card.Buttons = new List<CardAction>
            {
                new CardAction(ActionTypes.OpenUrl, "Saiba mais", value:url.Trim())
            };

            card.Images = new List<CardImage>
            {
                new CardImage(url = urlImagem)
            };

            resposta.Attachments.Add(card.ToAttachment());
            await context.PostAsync(resposta);
        }
    }
}