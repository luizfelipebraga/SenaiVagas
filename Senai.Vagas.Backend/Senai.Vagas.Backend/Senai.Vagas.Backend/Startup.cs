using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Senai.Vagas.Backend.Application.Queries.ImpQueries;
using Senai.Vagas.Backend.Application.Queries.Interfaces;
using Senai.Vagas.Backend.Helpers.Jobs;
using Senai.Vagas.Domain.AggregatesModel.AlterarCredenciaisAggregate;
using Senai.Vagas.Domain.AggregatesModel.AlunoAggregate;
using Senai.Vagas.Domain.AggregatesModel.AreaAggregate;
using Senai.Vagas.Domain.AggregatesModel.ConviteEntrevistaAggregate;
using Senai.Vagas.Domain.AggregatesModel.EmpresaAggregate;
using Senai.Vagas.Domain.AggregatesModel.EnderecoAggregate;
using Senai.Vagas.Domain.AggregatesModel.EstagioAggregate;
using Senai.Vagas.Domain.AggregatesModel.FaixaSalarialAggregate;
using Senai.Vagas.Domain.AggregatesModel.HistoricoStatusEstagioAggregate;
using Senai.Vagas.Domain.AggregatesModel.HistoricoStatusUsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.InscricaoAggregate;
using Senai.Vagas.Domain.AggregatesModel.RequerimentoMatriculaAggregate;
using Senai.Vagas.Domain.AggregatesModel.StatusEstagioAggregate;
using Senai.Vagas.Domain.AggregatesModel.StatusUsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.StatusVagaAggregate;
using Senai.Vagas.Domain.AggregatesModel.TermoOuEgressoAlunoEstagioAggregate;
using Senai.Vagas.Domain.AggregatesModel.TipoAtividadeCnaeAggregate;
using Senai.Vagas.Domain.AggregatesModel.TipoCursoAggregate;
using Senai.Vagas.Domain.AggregatesModel.TipoEmpresaAggregate;
using Senai.Vagas.Domain.AggregatesModel.TipoExperienciaAggregate;
using Senai.Vagas.Domain.AggregatesModel.TipoUsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAdminstradorAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioCandidatoAlunoAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioEmpresaAggregate;
using Senai.Vagas.Domain.AggregatesModel.VagaAggregate;
using Senai.Vagas.Domain.AggregatesModel.ValidacaoUsuarioCandidatoAggregate;
using Senai.Vagas.Infrastructure.Contexts;
using Senai.Vagas.Infrastructure.Repositories;

namespace Senai.Vagas.Backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Configura o AutoMapper
            // E onde irá encontrar os Profiles (perfis)
            services.AddAutoMapper(typeof(Startup).Assembly);

            //DependencyInjecions(DI) Vinculando a dependencia da classe repository com a InterfaceRepository
            //Em outras palavras, você pode usar a implementação dos métodos do repository, apenas instanciando... A interface! loucura neh? Foi o que eu pensei também!

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ITipoUsuarioRepository, TipoUsuarioRepository>();
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();
            services.AddScoped<IAlterarCredenciaisRepository, AlterarCredenciaisRepository>();
            services.AddScoped<IAlunoRepository, AlunoRepository>();
            services.AddScoped<IConviteEntrevistaRepository, ConviteEntrevistaRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IEstagioRepository, EstagioRepository>();
            services.AddScoped<IFaixaSalarialRepository, FaixaSalarialRepository>();
            services.AddScoped<IHistoricoStatusEstagioRepository, HistoricoStatusEstagioRepository>();
            services.AddScoped<IHistoricoStatusUsuarioRepository, HistoricoStatusUsuarioRepository>();
            services.AddScoped<IInscricaoRepository, InscricaoRepository>();
            services.AddScoped<IRequerimentoMatriculaRepository, RequerimentoMatriculaRepository>();
            services.AddScoped<IStatusEstagioRepository, StatusEstagioRepository>();
            services.AddScoped<IStatusUsuarioRepository, StatusUsuarioRepository>();
            services.AddScoped<IStatusVagaRepository, StatusVagaRepository>();
            services.AddScoped<ITermoOuEgressoAlunoRepository, TermoOuEgressoAlunoRepository>();
            services.AddScoped<ITipoAtividadeCnaeRepository, TipoAtividadeCnaeRepository>();
            services.AddScoped<ITipoCursoRepository, TipoCursoRepository>();
            services.AddScoped<ITipoEmpresaRepository, TipoEmpresaRepository>();
            services.AddScoped<ITipoExperienciaRepository, TipoExperienciaRepository>();
            services.AddScoped<IUsuarioAdministradorRepository, UsuarioAdministradorRepository>();
            services.AddScoped<IUsuarioCandidatoAlunoRepository, UsuarioCandidatoAlunoRepository>();
            services.AddScoped<IUsuarioEmpresaRepository, UsuarioEmpresaRepository>();
            services.AddScoped<IVagaRepository, VagaRepository>();
            services.AddScoped<IValidacaoUsuarioCandidatoRepository, ValidacaoUsuarioCandidatoRepository>();
            services.AddScoped<IAreaRepository, AreaRepository>();

            // Vinculando Interfaces Queries com a implementação Queries

            services.AddTransient<IEnderecoQueries, EnderecoQueries>();
            services.AddTransient<IVagaQueries, VagaQueries>();
            services.AddTransient<IAdministradorQueries, AdministradorQueries>();
            services.AddTransient<IInscricaoQueries, InscricaoQueries>();
            services.AddTransient<IUsuarioQueries, UsuarioQueries>();
            services.AddTransient<IEmpresaQueries, EmpresaQueries>();
            services.AddTransient<IAreaQueries, AreaQueries>();
            services.AddTransient<IExperienciaQueries, ExperienciaQueries>();
            services.AddTransient<IFaixaSalarialQueries, FaixaSalarialQueries>();

            //**
            //Configurações da DB
            //**
            services.AddDbContext<SenaiVagasContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                });
            }, ServiceLifetime.Scoped);

            //Configurações swagger (melhor entendimento dos endpoints controllers) documentação
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Senai Vagas", Version = "v1" });

                //Adiciona os comentários (summary) do controller
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            //*
            //Configurando o JWT (Autentificação)
            //*
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            }).AddJwtBearer("JwtBearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //Verificando...

                    //Quem esta solicitando
                    ValidateIssuer = true,

                    //Quem esta recebendo
                    ValidateAudience = true,

                    //Valida o tempo de expiração do token
                    ValidateLifetime = true,

                    //Forma da criptografia
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("senaivagas-chave-autenticacao")),

                    //Tempo de expiração do token
                    ClockSkew = TimeSpan.FromMinutes(30),

                    // Nome da issuer, de onde está vindo
                    ValidIssuer = "Senai.Vagas.Backend",

                    // Nome da audience, de onde está vindo
                    ValidAudience = "Senai.Vagas.Backend"
                };
            });

            //Necessário acrescentar política de CORS para ser possível o acesso da API com domínios diferentes
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime, IServiceScopeFactory services)
        {
            //Método que aplica jobs sempre que a API é inicializada. (registra o serviço na inicialização)
            applicationLifetime.ApplicationStarted.Register(() =>
            {
                app.ConfigureJobsAsync();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Senai.Vagas.Backend");
            });

            // Habilita o uso de autenticação
            app.UseAuthentication();

            //Usa a "CorsPolicy" criada anteriormente
            app.UseCors("CorsPolicy");

            app.UseMvc();
        }
    }
}
