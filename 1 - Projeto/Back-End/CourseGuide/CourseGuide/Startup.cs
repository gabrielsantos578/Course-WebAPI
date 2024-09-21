using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using CourseGuide.Contexts;
using CourseGuide.Services.Server;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace CourseGuide
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration; // Armazena a configuração da aplicação.
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configuração do banco de dados usando PostgreSQL
            services.AddDbContext<AppDBContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            // Configuração do Swagger para documentação da API
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CourseGuide", Version = "v1" });
            });

            // Adiciona suporte a controleadores e configura opções de serialização JSON
            services.AddControllers().AddJsonOptions(
                c => c.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddEndpointsApiExplorer();

            // Configuração de CORS (Cross-Origin Resource Sharing)
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:3000", "http://localhost:5173") // Permite origens específicas
                    .AllowAnyMethod() // Permite qualquer método HTTP
                    .AllowAnyHeader() // Permite qualquer cabeçalho
                    .AllowCredentials(); // Permite o uso de credenciais
            }));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // Configuração do AutoMapper

            // Injeção de Dependências
            services.InjectDependencies();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Página de erro no desenvolvimento
                app.UseSwagger(); // Habilita o Swagger
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sua API V1"); // Endpoint do Swagger
                    c.DocExpansion(DocExpansion.None); // Expansão inicial da documentação
                    c.DisplayRequestDuration(); // Exibe a duração da requisição
                    c.EnableDeepLinking(); // Permite links diretos
                    c.EnableFilter(); // Habilita filtro
                    c.ShowExtensions(); // Mostra extensões
                    c.EnableValidator(); // Habilita validador de requisições
                    c.SupportedSubmitMethods(SubmitMethod.Get, SubmitMethod.Post, SubmitMethod.Put, SubmitMethod.Delete, SubmitMethod.Patch); // Métodos suportados
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); // Manipulador de erros em produção
                app.UseHsts(); // Habilita HSTS
            }

            app.UseHttpsRedirection(); // Redireciona para HTTPS
            app.UseStaticFiles(); // Habilita arquivos estáticos

            app.UseRouting(); // Configura o roteamento

            app.UseCors("MyPolicy"); // Aplica a política de CORS

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Mapeia controladores
            });
        }
    }
}
