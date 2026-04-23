using ApiPetshop.Data;
using Infra;
using Domain;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Infra.Data;
using Domain.Interfaces;
using Infra.Repositories;

// Inicializa o criador (builder) da aplicação Web
var builder = WebApplication.CreateBuilder(args);

// Configuração do CORS (Cross-Origin Resource Sharing)
// Permite que nossa API seja acessada pelo frontend (ex: HTML/JS na porta 5500 ou 3000)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        // "AllowAnyOrigin" permite requisições de qualquer origem (IP ou domínio)
        // "AllowAnyMethod" permite métodos como GET, POST, PUT, DELETE
        // "AllowAnyHeader" permite o envio de qualquer cabeçalho na requisição
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Obtém a string de conexão configurada no appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configurando o Banco de Dados (Entity Framework Core com MySQL)
builder.Services.AddDbContext<MeuDbContext>(options =>
    options.UseMySql(
        connectionString,
        // Define a versão do servidor MySQL utilizado
        new MySqlServerVersion(new Version(8, 0, 36)),
        // Configura uma resiliência básica a falhas de conexão
        mySqlOptions => mySqlOptions.EnableRetryOnFailure()
    )
);

// Adiciona os serviços necessários para trabalhar com Controllers (as rotas/APIs)
builder.Services.AddControllers();

// Adiciona recursos para descobrir automatiamente os endpoints e mostrar na documentação
builder.Services.AddEndpointsApiExplorer();

// Configura o gerador da documentação interativa da API (Swagger)
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "ApiPetshop", Version = "v1" });
});

// Registro do Repositório (Injeção de Dependência)
builder.Services.AddScoped<IContatoRepository, ContatoRepository>();

// A partir daqui, as configurações de serviços (injeção de dependência) terminaram
// E inicia-se o "pipeline de requisições HTTP" (Middlewares)
var app = builder.Build();

// Verifica se o ambiente de execução é "Desenvolvimento"
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.MapScalarApiReference(options =>
    {
        options.WithOpenApiRoutePattern("/swagger/{documentName}/swagger.json");

        // Esta linha força o tema claro (tela branca)
        options.WithTheme(ScalarTheme.None);
    });
}

// Redireciona requisições HTTP para HTTPS, aumentando a segurança
app.UseHttpsRedirection();

// Habilita o servidor para retornar arquivos estáticos (ex: imagens, HTML que estiverem na pasta wwwroot)
app.UseStaticFiles();

// Aplica a política de CORS criada anteriormente ("AllowAll")
app.UseCors("AllowAll");

// Habilita a autorização (caso tivéssemos sistema de login com token JWT, por exemplo)
app.UseAuthorization();

// Mapeia nossas classes Controllers para que respondam às requisições chamadas
app.MapControllers();

// Bloco para inicializar ou atualizar o banco de dados e inserir dados iniciais na primeira execução
// ... (resto do código acima igual)

// Bloco para inicializar ou atualizar o banco de dados
using (var scope = app.Services.CreateScope())
{
    // 1. Mude para MeuDbContext aqui
    var db = scope.ServiceProvider.GetRequiredService<MeuDbContext>();

    // Executa as migrações (Cria as tabelas no MySQL)
    db.Database.Migrate();

    // 2. Agora o 'db' (MeuDbContext) vai ser aceito pelo SeedData 
    // se você já tiver mudado a assinatura lá no arquivo SeedData.cs
    SeedData.Seed(db);
}

app.Run();
