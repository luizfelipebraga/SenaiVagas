using Senai.Vagas.Backend.Application.ResponseViewModels;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.Queries.Interfaces
{
    public interface IAdministradorQueries
    {
        List<UsuarioAdministradorViewModel> GetAllAdministradores();
    }
}
