using LibraryDatabaseClassLibrary.Context;
using LibraryDatabaseClassLibrary.Interfaces;
using LibraryDatabaseClassLibrary.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LibraryDatabseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAuthorBookService, AuthorBookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookGenreService, BookGenreService>();
builder.Services.AddScoped<IBookLibraryService, BookLibraryService>();
builder.Services.AddScoped<IBookPublisherService, BookPublisherService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IHoldService, HoldService>();
builder.Services.AddScoped<ILibraryService, LibraryService>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
