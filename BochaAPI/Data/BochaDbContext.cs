using BochaAPI.Domain;
using BochaAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BochaAPI.Data
{
    public class BochaDbContext : DbContext
    {
        public BochaDbContext(DbContextOptions<BochaDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }
        //repreentacion coleccion de entidades en el db
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Imagen> Imagenes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //SEED DATA OSEA LLENAR CON DATA
            //FACIL MEDIO DIFICIL

            var marcas = new List<Marca>()
            {
                new Marca()
                {
                    IdMarca=Guid.Parse("7770a7c2-ef67-4852-8f96-49235cba1515"),
                    Nombre="Nike"
                },
                new Marca()
                {
                    IdMarca=Guid.Parse("3d8a684f-75f1-4dce-86d0-e4681e9c1859"),
                    Nombre="Puma"
                },
                new Marca()
                {
                    IdMarca=Guid.Parse("ce67dfa5-2908-406d-89e3-d8aaecb68cef"),
                    Nombre="Adidas"
                }
            };
            //Seed, escoger la entidad que se desea llenar
            //Se le pasa la lista                    -------------
            modelBuilder.Entity<Marca>().HasData(marcas);



            //Seed para las regiones

            var categorias = new List<Categoria>()
            {
                new Categoria
                {

                    IdCategoria= Guid.Parse("6ee6ae5c-e2a9-4c11-80b6-9977d3b2837e"),
                    Nombre="TREND",
                    Code="TRD",
                    CategoriaImageURL="https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.goal.com%2Fen%2Flists%2Flionel-messi-want-make-olynpic-history-paris-2024-invitation-argentina-inter-miami-icon-games-france%2Fbltbc7be24aac1d9483&psig=AOvVaw2GA6GRIX7QwpOn0WxQfCqU&ust=1698706541950000&source=images&cd=vfe&opi=89978449&ved=0CBEQjRxqFwoTCLji-pGtnIIDFQAAAAAdAAAAABAE"

                },
                new Categoria
                {

                    IdCategoria= Guid.Parse("7f994b7e-7eb2-45c4-8a13-80bdc9afd229"),
                    Nombre="FUTBOL",
                    Code="FTB",
                    CategoriaImageURL="https://viapais.com.ar/resizer/fhAUasx4qvTMGOwvqChc_1H7Urk=/1080x1350/smart/cloudfront-us-east-1.images.arcpublishing.com/grupoclarin/6LU6B2QP4NDAHIJFTJGKS7NDKI.jpg"

                },
                new Categoria
                {

                    IdCategoria= Guid.Parse("2ec86d04-967a-408c-83d0-d7d89c1386f6"),
                    Nombre="BASKET",
                    Code="BSKT",
                    CategoriaImageURL="https://freshstreetculture.com/cdn/shop/files/hasbulla1_d677ff1d-cc0f-447d-aa5e-b8339cde6712.jpg?v=1685477923&width=1445"

                }


            };
            modelBuilder.Entity<Categoria>().HasData(categorias);
            

        }


    }
}
