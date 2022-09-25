using Microsoft.EntityFrameworkCore;
using OamCake.Entity;
using OamCake.Common.Helpers;

namespace OamCake.Data
{
    public static class SeedDb
    {
        public static void SeedDbAsync(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                Id = 1,
                Name = "Yunior",
                LastName = "Laureano",
                Address = "San Luis",
                Phone = "8295343561",
                CreatedAt = DateTime.Now,
                CreatedBy = 1
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Email = "yuniorlaureano@gmail.com",
                CreatedAt = DateTime.Now,
                CreatedBy = 1,
                Password = "123".HashedPassword(),
                IsActive = true,
                EmployeeId = 1
            });


            modelBuilder.Entity<Category>().HasData(new List<Category>
            {
                new Category { Id = 1, Name = "Boda", CreatedAt = DateTime.Now, CreatedBy = 1},
                new Category { Id = 2,Name = "Cumple años", CreatedAt = DateTime.Now, CreatedBy = 1},
                new Category { Id = 3,Name = "Velorios", CreatedAt = DateTime.Now, CreatedBy = 1},
            });

            modelBuilder.Entity<Product>().HasData(new List<Product>
            {
                new Product { Id = 1, Name = "Azucar", Code = "C-453", Description = "Negra", CreatedAt = DateTime.Now, CreatedBy = 1},
                new Product { Id = 2, Name = "Vainilla ", Code = "C-451", Description = "Blanca", CreatedAt = DateTime.Now, CreatedBy = 1},
                new Product {Id = 3, Name = "Arina", Code = "C-457", Description = "Integral", CreatedAt = DateTime.Now, CreatedBy = 1},
            });

            modelBuilder.Entity<Inventory>().HasData(new List<Inventory>
            {
                new Inventory {Id = 1, ProductId = 1, Date = DateTime.Now, Quantity = 43, CreatedAt = DateTime.Now, CreatedBy = 1},
                new Inventory {Id = 2, ProductId = 2, Date = DateTime.Now, Quantity = 12, CreatedAt = DateTime.Now, CreatedBy = 1},
                new Inventory {Id = 3, ProductId = 3, Date = DateTime.Now, Quantity = 4, CreatedAt = DateTime.Now, CreatedBy = 1},
                new Inventory {Id = 4, ProductId = 1, Date = DateTime.Now, Quantity = -2, CreatedAt = DateTime.Now, CreatedBy = 1},
                new Inventory {Id = 5, ProductId = 2, Date = DateTime.Now, Quantity = -10, CreatedAt = DateTime.Now, CreatedBy = 1},
                new Inventory {Id = 6, ProductId = 3, Date = DateTime.Now, Quantity = -2, CreatedAt = DateTime.Now, CreatedBy = 1},
            });

            modelBuilder.Entity<Cake>().HasData(new List<Cake>
            {
                new Cake { Id = 1, Name = "Bizcocho", Photo = "7a0d3b02-a564-48ad-9328-9d225ab0dbd4.jpg", CategoryId = 1, CreatedAt = DateTime.Now, CreatedBy = 1},
                new Cake { Id = 2, Name = "Zahahoria", Photo = "5b67ce7b-3744-4bc8-9337-72ef6928ea9b.jfif", CategoryId = 2, CreatedAt = DateTime.Now, CreatedBy = 1},
                new Cake { Id = 3, Name = "Chocolate", Photo = "daca910f-94b4-4201-99ea-f5df6488bb2a.jfif", CategoryId = 3, CreatedAt = DateTime.Now, CreatedBy = 1},
                new Cake { Id = 4, Name = "Sandia", Photo = "c6ea26ac-f3ad-47f1-9e13-772a33c91667.jpg", CategoryId = 1, CreatedAt = DateTime.Now, CreatedBy = 1},
            });

            modelBuilder.Entity<Ingredient>().HasData(new List<Ingredient>
            {
                new Ingredient
                {
                    Id = 1,
                    CakeId = 1,
                    ProductId = 1,
                    Quantity = 4,
                    Unid = 'l',
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1,
                },
                new Ingredient
                {
                    Id = 2,
                    CakeId = 1,
                    ProductId = 2,
                    Quantity = 4,
                    Unid = 'l',
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1,
                },
                new Ingredient
                {
                    Id = 3,
                    CakeId = 1,
                    ProductId = 3,
                    Quantity = 4,
                    Unid = 'l',
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1,
                },
                new Ingredient
                {
                    Id = 4,
                    CakeId = 2,
                    ProductId = 1,
                    Quantity = 4,
                    Unid = 'l',
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1,
                },
                new Ingredient
                {
                    Id = 5,
                    CakeId = 2,
                    ProductId = 2,
                    Quantity = 4,
                    Unid = 'l',
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1,
                },
                new Ingredient
                {
                    Id = 6,
                    CakeId = 2,
                    ProductId = 3,
                    Quantity = 5,
                    Unid = 'g',
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1,
                },
                new Ingredient
                {
                    Id = 7,
                    CakeId = 3,
                    ProductId = 1,
                    Quantity = 4,
                    Unid = 'l',
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1,
                },
                new Ingredient
                {
                    Id = 8,
                    CakeId = 3,
                    ProductId = 2,
                    Quantity = 2,
                    Unid = 'g',
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1,
                },
                new Ingredient
                {
                    Id = 9,
                    CakeId = 3,
                    ProductId = 3,
                    Quantity = 3,
                    Unid = 'g',
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1,
                },
                new Ingredient
                {
                    Id = 10,
                    CakeId = 4,
                    ProductId = 1,
                    Quantity = 7,
                    Unid = 'l',
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1,
                },
                new Ingredient
                {
                    Id = 11,
                    CakeId = 4,
                    ProductId = 2,
                    Quantity = 8,
                    Unid = 'g',
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1,
                },
                new Ingredient
                {
                    Id = 12,
                    CakeId = 4,
                    ProductId = 3,
                    Quantity = 11,
                    Unid = 'g',
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1,
                }
            });
        }
    }
}
