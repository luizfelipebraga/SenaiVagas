using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Senai.Vagas.Backend.Helpers.Jobs.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.Jobs
{
    public static class ConfigureJobs
    {
        public static async void ConfigureJobsAsync(this IApplicationBuilder app)
        {
            List<Type> jobs = new List<Type>()
            {
                //typeof's com classes JOBS dentro

                //Padrões para o sistema funcionar
                typeof(TipoUsuarioJobs),
                typeof(TipoCursoJobs),
                typeof(TipoExperienciaJobs),
                typeof(TipoAtividadeCnaeJobs),
                typeof(AreaJobs),
                typeof(StatusEstagioJobs),
                typeof(RequerimentoMatriculaJobs),
                typeof(StatusUsuarioJobs),
                typeof(TipoEmpresaJobs),
                typeof(StatusVagaJobs),
                typeof(FaixaSalarialJobs),
                typeof(TermoOuEgressoAlunoJobs),

                //Usuarios padrões
                typeof(UsuarioAdmJobs),
            };

            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                foreach (var item in jobs)
                {
                    var job = (IJobs)ActivatorUtilities.CreateInstance(scope.ServiceProvider, item);

                    await job.ExecuteAsync();
                }
            }
        }
    }
}
