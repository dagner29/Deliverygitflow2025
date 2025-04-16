using Delivery.Domain.Entities;
using Delivery.Infraestructure.Persistence.Repositories;
using Delivery.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Delivery.Test.Infraestructura
{

    public class PackageRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public PackageRepositoryTests()
        {
            // Configuración de la base de datos en memoria
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
        }

        private ApplicationDbContext CreateDbContext()
        {
            return new ApplicationDbContext(_dbContextOptions);
        }

        [Fact]
        public async Task AddAsync_ShouldAddPackage()
        {
            using var context = CreateDbContext();
            var repository = new PackageRepository(context);
            var package = new Package("Test Package", 5.5, Guid.NewGuid());

            // Act
            await repository.AddAsync(package);

            // Assert
            var result = await context.Packages.FindAsync(package.Id);
            Assert.NotNull(result);
            Assert.Equal("Test Package", result.ContentDescription);
            Assert.Equal(5.5, result.Weight);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeletePackage()
        {
            using var context = CreateDbContext();
            var repository = new PackageRepository(context);
            var package = new Package("Test Package", 5.5, Guid.NewGuid());
            await repository.AddAsync(package);

            // Act
            await repository.DeleteAsync(package.Id);

            // Assert
            var result = await context.Packages.FindAsync(package.Id);
            Assert.Null(result);  // El paquete debe ser eliminado
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllPackages()
        {
            using var context = CreateDbContext();
            var repository = new PackageRepository(context);
            var package1 = new Package("Package 1", 5.5, Guid.NewGuid());
            var package2 = new Package("Package 2", 10.5, Guid.NewGuid());
            await repository.AddAsync(package1);
            await repository.AddAsync(package2);

            // Act
            var packages = await repository.GetAllAsync();

            // Assert
            Assert.Equal(2, packages.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectPackage()
        {
            using var context = CreateDbContext();
            var repository = new PackageRepository(context);
            var package = new Package("Test Package", 5.5, Guid.NewGuid());
            await repository.AddAsync(package);

            // Act
            var result = await repository.GetByIdAsync(package.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(package.Id, result.Id);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdatePackage()
        {
            using var context = CreateDbContext();
            var repository = new PackageRepository(context);
            var package = new Package("Test Package", 5.5, Guid.NewGuid());
            await repository.AddAsync(package);

            // Modify the package
            package.ContentDescription = "Updated Package";
            package.Weight = 6.5;

            // Act
            await repository.UpdateAsync(package);

            // Assert
            var result = await context.Packages.FindAsync(package.Id);
            Assert.NotNull(result);
            Assert.Equal("Updated Package", result.ContentDescription);
            Assert.Equal(6.5, result.Weight);
        }
    }
    }
