using Api_test.Enums;
using Api_test.Models;
using Api_test.Models.Response;
using Api_test.Models.Request;

namespace Api_test.Converters
{
    public class ColaboradorConverter
    {
        public ColaboradorModel ConverterParaModel(ColaboradorResponse dto)
        {
            return new ColaboradorModel
            {
                Id = dto.Id,
                Nome = dto.Nome,
                Genero = dto.Genero,
                Idade = dto.Idade,
                Ativo = dto.Ativo,
                DataNasc = dto.DataNasc,
                Telefone = dto.Telefone,
                Email = dto.Email,
                DataDeCriacao = DateTime.Now.ToLocalTime(),
                DataDeAlteracao = DateTime.Now.ToLocalTime(),
                Cargo = Enum.Parse<Cargo>(dto.Cargo)
            };
        }

        public ColaboradorResponse ConverterParaDTO(ColaboradorModel model)
        {
            return new ColaboradorResponse
            {
                Id = model.Id,
                Nome = model.Nome,
                Genero = model.Genero,
                Idade = model.Idade,
                Ativo = model.Ativo,
                DataNasc = model.DataNasc,
                Telefone = model.Telefone,
                Email = model.Email,
                Cargo = model.Cargo.ToString(),
                DataDeCriacao = model.DataDeCriacao,
                DataDeAlteracao = model.DataDeAlteracao
            };
        }

        public ColaboradorModel ConverterCadastroParaModel(ColaboradorCadastroRequest dto)
        {
            return new ColaboradorModel
            {
                Nome = dto.Nome,
                Genero = dto.Genero,
                Idade = dto.Idade,
                DataNasc = dto.DataNasc,
                Telefone = dto.Telefone,
                Email = dto.Email,
                Cargo = Enum.Parse<Cargo>(dto.Cargo),
            };
        }
        public ColaboradorModel ConverterPutParaModel(ColaboradorPutRequest dto)
        {
            return new ColaboradorModel
            {
                Id = dto.Id,
                Nome = dto.Nome,
                Genero = dto.Genero,
                Idade = dto.Idade,
                Ativo = dto.Ativo,
                DataNasc = dto.DataNasc,
                Telefone = dto.Telefone,
                Email = dto.Email,
                Cargo = Enum.Parse<Cargo>(dto.Cargo),
                DataDeAlteracao = DateTime.Now.ToLocalTime()
            };
        }
    }
}
