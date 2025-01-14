using Moq;
using MyMedicationApp.Application.Interfaces;
using MyMedicationApp.Application.Services;
using MyMedicationApp.Domain.Entities;

namespace MyMedicationApp.Tests;

public class MedicationServiceTests
    {
        private readonly Mock<IMedicationRepository> _mockRepo;
        private readonly MedicationService _service;

        public MedicationServiceTests()
        {
            _mockRepo = new Mock<IMedicationRepository>();
            _service = new MedicationService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllMedicationsAsync_ReturnsListOfMedications()
        {
            // Arrange
            var medications = new List<Medication>
            {
                new Medication { Id = Guid.NewGuid(), Name = "Test1", Quantity = 10, CreationDate = DateTime.UtcNow },
                new Medication { Id = Guid.NewGuid(), Name = "Test2", Quantity = 5, CreationDate = DateTime.UtcNow },
            };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(medications);

            // Act
            var result = await _service.GetAllMedicationsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task CreateMedicationAsync_ThrowsIfQuantityIsZeroOrLess()
        {
            // Arrange
            string name = "InvalidMed";
            int quantity = 0;

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
                _service.CreateMedicationAsync(name, quantity));
            Assert.Equal("Quantity must be greater than zero.", ex.Message);
        }

        [Fact]
        public async Task CreateMedicationAsync_SucceedsForValidQuantity()
        {
            // Arrange
            var med = new Medication
            {
                Id = Guid.NewGuid(),
                Name = "ValidMed",
                Quantity = 10,
                CreationDate = DateTime.UtcNow
            };

            _mockRepo.Setup(r => r.CreateAsync(It.IsAny<Medication>())).ReturnsAsync(med);

            // Act
            var result = await _service.CreateMedicationAsync("ValidMed", 10);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("ValidMed", result.Name);
            Assert.Equal(10, result.Quantity);
        }

        [Fact]
        public async Task DeleteMedicationAsync_ReturnsFalse_WhenNotFound()
        {
            // Arrange
            var medId = Guid.NewGuid();
            _mockRepo.Setup(r => r.DeleteAsync(medId)).ReturnsAsync(false);

            // Act
            var result = await _service.DeleteMedicationAsync(medId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteMedicationAsync_ReturnsTrue_WhenDeleted()
        {
            // Arrange
            var medId = Guid.NewGuid();
            _mockRepo.Setup(r => r.DeleteAsync(medId)).ReturnsAsync(true);

            // Act
            var result = await _service.DeleteMedicationAsync(medId);

            // Assert
            Assert.True(result);
        }
    }