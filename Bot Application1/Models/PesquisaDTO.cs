using System;
using System.Collections.Generic;

namespace Bot_Application1.Models
{
    public class PesquisaDTO
    {
        public string sexo { get; set; }
        public IList<SeriesAmigosDTO> SeriesDTO { get; set; }
        public long TotalConsultaSeries { get; set; }
        public long TotalPaginasSeries { get; set; }
        public int CurrentPageSeries { get; set; }
        public IList<SeriesAmigosDTO> AmigosDTO { get; set; }
        public long TotalConsultaAmigos { get; set; }
        public long TotalPaginasAmigos { get; set; }
        public int CurrentPageAmigos { get; set; }
        public string Search { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string UserName { get; set; }
        public string UF { get; set; }
        public string Email { get; set; }
        public UsuarioPesquisaSeriesDTO UsuarioLogado { get; set; }

        public class SeriesAmigosDTO
        {
            public bool IsSerie { get; set; }
            public string Nome { get; set; }
            public string Img { get; set; }
            public string Descricao { get; set; }
            public string CodSerie { get; set; }
            public bool IsFollow { get; set; }
            public bool AguardandoAceitar { get; set; }
            public long TotalConsulta { get; set; }
            public int TotalPaginas { get; set; }
            public bool Favorita { get; set; }
            public bool Vendo { get; set; }
            public bool? QueroVer { get; set; }
            public int Id { get; set; }
            public string Inicio { get; set; }
            public string Fim { get; set; }
            public string Status { get; set; }
            public string Pais { get; set; }
            public string Aka { get; set; }
            public string Path { get; set; }
            public string UserName { get; set; }
            public DateTime DataCadastro { get; set; }
            public DateTime? UltimaAtividade { get; set; }
            public Episodios_EUA UltimoEpisodio { get; set; }
            public virtual DateTime? Data_Adicionada { get; set; }


        }

        public class UsuarioPesquisaSeriesDTO
        {
 
        }
        public class Episodios_EUA
        {
            public int Serie_Id { get; set; }
            public string Existente { get; set; }
            public int EpisodioExistente { get; set; }
            public string Proximo { get; set; }
            public int ProximoEpisodio { get; set; }
            public string Data_Proximo { get; set; }
            public DateTime? Hoje { get; set; }
            public Episodios ExistenteInfo { get; set; }
        }
        public class Episodios
        {
            public virtual string Cod_Serie { get; set; }
            public virtual int Serie_Id { get; set; }
            public virtual string Cod_Episodio { get; set; }
            public virtual string Desc_Episodio { get; set; }
            public virtual int Temporada { get; set; }
            public virtual int Numero { get; set; }
            public virtual string NomeEpisodio { get; set; }
            public virtual string Resumo { get; set; }
            public virtual DateTime? DataUSA { get; set; }
            public virtual DateTime? DataBR { get; set; }
            public virtual string ImagemEpisodio { get; set; }
            public virtual Uri ImagemMaze { get; set; }
            public virtual bool IsSpoiler { get; set; }
            public virtual int QuantidadeComentarios { get; set; }
        }

    }
}