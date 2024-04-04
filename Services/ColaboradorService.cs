using Api_test.Enums;
using Api_test.Models;
using System;

namespace Api_test.Services
{
    public class ColaboradorService
    {
        public ColaboradorModel ConverterParaModel(ColaboradorDTO dto)
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
                Cargo = ParseCargo(dto.Cargo)
            };
        }

        private Cargo ParseCargo(string cargo)
        {
            switch (cargo)
            {
                case "Gerente":
                    return Cargo.Gerente;
                case "Desenvolvedor":
                    return Cargo.Desenvolvedor;
                case "Analista":
                    return Cargo.Analista;
                case "Designer":
                    return Cargo.Designer;
                case "Administrador":
                    return Cargo.Administrador;
                default:
                    throw new ArgumentException("Cargo inválido.");
            }
        }
    }
}
