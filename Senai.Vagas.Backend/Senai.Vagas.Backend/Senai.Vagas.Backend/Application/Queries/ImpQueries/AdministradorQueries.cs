using AutoMapper;
using Senai.Vagas.Backend.Application.Queries.Interfaces;
using Senai.Vagas.Backend.Application.ResponseViewModels;
using Senai.Vagas.Backend.Helpers.DefaultValues;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate.Models;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.Queries.ImpQueries
{
    public class AdministradorQueries : IAdministradorQueries
    {
        public SenaiVagasContext _ctx { get; set; }
        public IMapper _mapper { get; set; }

        public AdministradorQueries(SenaiVagasContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public List<UsuarioAdministradorViewModel> GetAllAdministradores()
        {
            // Busca o tipoUsuario Administrador
            var tipoAdm = _ctx.TipoUsuarios.FirstOrDefault(x => x.Descricao == TipoUsuarioDefaultValuesAccess.GetValue(TipoUsuarioDefaultValues.Administrador));

            // Busca todos os usuários com o tipo usuário "Administrador"
            var usuarios = _ctx.Usuarios.Where(x => x.TipoUsuarioId == tipoAdm.Id).ToList();

            List<UsuarioAdministradorViewModel> usuariosAdms = new List<UsuarioAdministradorViewModel>();
            
            // Cria um ViewModel específico que só irá retornar id, nome, nif e email de Admin
            foreach (var u in usuarios)
            {
                // Busca o usuarioAdministrador do usuarioId
                var usuarioAdm = _ctx.UsuarioAdministradores.FirstOrDefault(x => x.UsuarioId == u.Id);

                // Monta o ViewModel
                var usuarioResult = new UsuarioAdministradorViewModel(u.Id, u.Nome, usuarioAdm.NIF, u.Email);
                
                // Adiciona na lista
                usuariosAdms.Add(usuarioResult);
            }

            // Retorna o Adm
            return usuariosAdms;
        }
    }
}
