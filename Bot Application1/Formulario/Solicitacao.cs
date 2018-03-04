using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Dialogs;
using System;

namespace Bot_Application1.Formulario
{
    [Serializable]
    [Template(TemplateUsage.NotUnderstood, "Desculpe não entendi \"{0}\".")]
    public class Solicitacao
    {
        public TipoResposta TipoResposta { get; set; }
        public TipoSolicitacao TipoSolicitacao { get; set; }
        public TipoObjeto TipoObjeto { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }

        public static IForm<Solicitacao> BuildForm()
        {
            var form = new FormBuilder<Solicitacao>();
            form.Configuration.DefaultPrompt.ChoiceStyle = ChoiceStyleOptions.Buttons;
            form.Configuration.Yes = new string[] { "sim", "yes", "s", "y", "yep" };
            form.Configuration.No = new string[] { "nao", "não", "n", "not", "no" };
            form.Message("Olá, seja bem-vindo. Será um prazer atender você.");
            form.OnCompletion(async (context, solicitacao) =>
            {
                // salvar na base de dados
                // gerar pedido
                // integrar com serviço xpto

                await context.PostAsync("Sua solicitação foi realizada com sucesso.");
            });
            return form.Build();
        }

    }

    public enum TipoResposta
    {
        Email = 1,
        Mensagem
    }

    public enum TipoSolicitacao
    {
        Nova = 1,
        Erro,
        Alterar
    }
    [Describe("Tipo de Objeto")]

    public enum TipoObjeto
    {
        [Terms("serie", "show", "série")]
        [Describe("Série")]
        Serie = 1,
        Canal,
        Episodio,
        Site
    }
}