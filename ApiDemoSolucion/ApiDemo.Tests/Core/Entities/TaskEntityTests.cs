namespace ApiDemo.Tests.Core.Entities;

/// <summary>
/// Tests unitarios para la entidad TaskEntity
/// Valida todas las propiedades, comportamientos y escenarios de uso
/// </summary>
public class TaskEntityTests
{
    #region Tests de Creación y Propiedades

    [Fact]
    public void TaskEntity_WhenCreated_ShouldHaveDefaultValues()
    {
      // Arrange & Act
      var task = new TaskEntity();

        // Assert
        task.Should().NotBeNull();
        task.Id.Should().Be(Guid.Empty);
        task.Title.Should().Be(string.Empty);
        task.Description.Should().BeNull();
        task.Status.Should().Be(TaskStatus.Pending);
   task.Priority.Should().Be(TaskPriority.Low);
        task.StartDate.Should().BeNull();
        task.DueDate.Should().BeNull();
        task.CreatedAt.Should().Be(default(DateTime));
        task.UpdatedAt.Should().Be(default(DateTime));
    }

    [Fact]
    public void TaskEntity_WhenCreated_ShouldAllowPropertyAssignment()
    {
        // Arrange
    var id = Guid.NewGuid();
        var title = "Test Task";
        var description = "Test Description";
        var status = TaskStatus.InProgress;
        var priority = TaskPriority.High;
      var startDate = DateTime.UtcNow;
var dueDate = DateTime.UtcNow.AddDays(7);
var createdAt = DateTime.UtcNow;
        var updatedAt = DateTime.UtcNow;

   // Act
        var task = new TaskEntity
        {
            Id = id,
    Title = title,
 Description = description,
            Status = status,
      Priority = priority,
    StartDate = startDate,
    DueDate = dueDate,
            CreatedAt = createdAt,
            UpdatedAt = updatedAt
        };

        // Assert
        task.Id.Should().Be(id);
        task.Title.Should().Be(title);
      task.Description.Should().Be(description);
        task.Status.Should().Be(status);
        task.Priority.Should().Be(priority);
    task.StartDate.Should().Be(startDate);
        task.DueDate.Should().Be(dueDate);
      task.CreatedAt.Should().Be(createdAt);
    task.UpdatedAt.Should().Be(updatedAt);
    }

    [Fact]
    public void TaskEntity_Properties_ShouldHaveCorrectTypes()
    {
        // Arrange
  var task = new TaskEntity();

        // Assert - Validar que las propiedades tienen valores válidos de sus tipos
 task.Id.GetType().Should().Be(typeof(Guid));
        task.Title.GetType().Should().Be(typeof(string));
        task.Status.GetType().Should().Be(typeof(TaskStatus));
     task.Priority.GetType().Should().Be(typeof(TaskPriority));
        task.CreatedAt.GetType().Should().Be(typeof(DateTime));
        task.UpdatedAt.GetType().Should().Be(typeof(DateTime));
        
        // Propiedades nullable
     task.Description.Should().BeNull(); // nullable string
        task.StartDate.Should().BeNull(); // nullable DateTime
        task.DueDate.Should().BeNull(); // nullable DateTime
 }

    #endregion

    #region Tests de Validación de Title

    [Fact]
    public void TaskEntity_Title_ShouldAllowEmptyString()
    {
        // Arrange & Act
     var task = new TaskEntity { Title = string.Empty };

        // Assert
        task.Title.Should().Be(string.Empty);
  task.Title.Should().HaveLength(0);
    }

    [Fact]
    public void TaskEntity_Title_ShouldAllowWhitespaceString()
    {
        // Arrange
        var whitespaceTitle = "   ";

     // Act
      var task = new TaskEntity { Title = whitespaceTitle };

      // Assert
        task.Title.Should().Be(whitespaceTitle);
  task.Title.Should().HaveLength(3);
 }

    [Theory]
    [InlineData("A")]
    [InlineData("Short Title")]
    [InlineData("This is a very long title that contains many characters to test the behavior")]
    public void TaskEntity_Title_ShouldAcceptDifferentLengths(string title)
    {
     // Arrange & Act
        var task = new TaskEntity { Title = title };

    // Assert
        task.Title.Should().Be(title);
        task.Title.Should().HaveLength(title.Length);
    }

    [Fact]
    public void TaskEntity_Title_ShouldHaveEmptyStringAsDefault()
    {
      // Arrange & Act
        var task = new TaskEntity();

        // Assert
        task.Title.Should().NotBeNull();
        task.Title.Should().Be(string.Empty);
    }

#endregion

    #region Tests de Description

    [Fact]
    public void TaskEntity_Description_ShouldAllowNull()
    {
        // Arrange & Act
     var task = new TaskEntity { Description = null };

    // Assert
task.Description.Should().BeNull();
  }

    [Fact]
    public void TaskEntity_Description_ShouldAllowEmptyString()
    {
      // Arrange & Act
 var task = new TaskEntity { Description = string.Empty };

        // Assert
        task.Description.Should().Be(string.Empty);
        task.Description.Should().HaveLength(0);
    }

    [Fact]
    public void TaskEntity_Description_ShouldAllowLongText()
    {
        // Arrange
  var longDescription = new string('A', 1000);

        // Act
 var task = new TaskEntity { Description = longDescription };

        // Assert
        task.Description.Should().Be(longDescription);
        task.Description.Should().HaveLength(1000);
    }

    [Fact]
    public void TaskEntity_Description_ShouldBeNullByDefault()
    {
   // Arrange & Act
   var task = new TaskEntity();

   // Assert
        task.Description.Should().BeNull();
    }

    #endregion

    #region Tests de Enumeraciones

    [Theory]
    [InlineData(TaskStatus.Pending)]
    [InlineData(TaskStatus.InProgress)]
    [InlineData(TaskStatus.Completed)]
    public void TaskEntity_Status_ShouldAcceptAllValidValues(TaskStatus status)
    {
      // Arrange & Act
      var task = new TaskEntity { Status = status };

// Assert
        task.Status.Should().Be(status);
    }

    [Theory]
    [InlineData(TaskPriority.Low)]
    [InlineData(TaskPriority.Medium)]
    [InlineData(TaskPriority.High)]
    public void TaskEntity_Priority_ShouldAcceptAllValidValues(TaskPriority priority)
    {
        // Arrange & Act
    var task = new TaskEntity { Priority = priority };

        // Assert
        task.Priority.Should().Be(priority);
    }

    [Fact]
    public void TaskEntity_Status_ShouldHavePendingAsDefault()
    {
        // Arrange & Act
        var task = new TaskEntity();

        // Assert
        task.Status.Should().Be(TaskStatus.Pending);
    }

    [Fact]
    public void TaskEntity_Priority_ShouldHaveLowAsDefault()
    {
        // Arrange & Act
    var task = new TaskEntity();

        // Assert
        task.Priority.Should().Be(TaskPriority.Low);
    }

    #endregion

    #region Tests de Fechas

    [Fact]
    public void TaskEntity_StartDate_ShouldAllowPastDate()
    {
        // Arrange
        var pastDate = DateTime.UtcNow.AddDays(-30);

        // Act
 var task = new TaskEntity { StartDate = pastDate };

  // Assert
task.StartDate.Should().Be(pastDate);
        task.StartDate.Should().BeBefore(DateTime.UtcNow);
    }

    [Fact]
    public void TaskEntity_StartDate_ShouldAllowPresentDate()
    {
        // Arrange
        var presentDate = DateTime.UtcNow;

     // Act
        var task = new TaskEntity { StartDate = presentDate };

     // Assert
        task.StartDate.Should().BeCloseTo(presentDate, TimeSpan.FromSeconds(1));
  }

    [Fact]
    public void TaskEntity_StartDate_ShouldAllowFutureDate()
    {
        // Arrange
        var futureDate = DateTime.UtcNow.AddDays(30);

  // Act
      var task = new TaskEntity { StartDate = futureDate };

      // Assert
        task.StartDate.Should().Be(futureDate);
     task.StartDate.Should().BeAfter(DateTime.UtcNow);
    }

[Fact]
    public void TaskEntity_DueDate_ShouldAllowFutureDate()
    {
    // Arrange
      var futureDate = DateTime.UtcNow.AddDays(14);

        // Act
        var task = new TaskEntity { DueDate = futureDate };

        // Assert
        task.DueDate.Should().Be(futureDate);
        task.DueDate.Should().BeAfter(DateTime.UtcNow);
    }

    [Fact]
    public void TaskEntity_StartDate_ShouldAllowNull()
 {
        // Arrange & Act
        var task = new TaskEntity { StartDate = null };

        // Assert
        task.StartDate.Should().BeNull();
    }

    [Fact]
    public void TaskEntity_DueDate_ShouldAllowNull()
    {
        // Arrange & Act
        var task = new TaskEntity { DueDate = null };

        // Assert
        task.DueDate.Should().BeNull();
    }

    [Fact]
    public void TaskEntity_CreatedAt_ShouldAcceptSpecificDate()
    {
        // Arrange
        var specificDate = new DateTime(2024, 1, 15, 10, 30, 0, DateTimeKind.Utc);

    // Act
        var task = new TaskEntity { CreatedAt = specificDate };

        // Assert
        task.CreatedAt.Should().Be(specificDate);
    }

    [Fact]
    public void TaskEntity_UpdatedAt_ShouldAcceptSpecificDate()
    {
        // Arrange
        var specificDate = new DateTime(2024, 1, 15, 11, 45, 0, DateTimeKind.Utc);

        // Act
      var task = new TaskEntity { UpdatedAt = specificDate };

        // Assert
        task.UpdatedAt.Should().Be(specificDate);
    }

    [Fact]
    public void TaskEntity_DatePrecision_ShouldBeAccurate()
    {
     // Arrange
        var expectedDate = DateTime.UtcNow;

        // Act
        var task = new TaskEntity { CreatedAt = expectedDate };

        // Assert
        task.CreatedAt.Should().BeCloseTo(expectedDate, TimeSpan.FromMilliseconds(1));
    }

    #endregion

    #region Tests de Guid

    [Fact]
    public void TaskEntity_Id_ShouldAllowEmptyGuid()
  {
        // Arrange & Act
     var task = new TaskEntity { Id = Guid.Empty };

  // Assert
        task.Id.Should().Be(Guid.Empty);
    }

  [Fact]
  public void TaskEntity_Id_ShouldAllowUniqueGuid()
  {
        // Arrange
        var uniqueId = Guid.NewGuid();

// Act
        var task = new TaskEntity { Id = uniqueId };

        // Assert
      task.Id.Should().Be(uniqueId);
  task.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void TaskEntity_MultipleInstances_ShouldHaveDifferentIds()
    {
  // Arrange & Act
        var task1 = new TaskEntity { Id = Guid.NewGuid() };
   var task2 = new TaskEntity { Id = Guid.NewGuid() };

   // Assert
      task1.Id.Should().NotBe(task2.Id);
    }

    #endregion

    #region Tests de Escenarios Completos

    [Fact]
    public void TaskEntity_CompleteScenario_ShouldCreateWithAllFields()
  {
        // Arrange
    var id = Guid.NewGuid();
        var title = "Complete Task";
        var description = "This is a complete task with all fields populated";
        var status = TaskStatus.InProgress;
  var priority = TaskPriority.High;
        var startDate = DateTime.UtcNow;
   var dueDate = DateTime.UtcNow.AddDays(7);
      var createdAt = DateTime.UtcNow;
  var updatedAt = DateTime.UtcNow;

        // Act
  var task = new TaskEntity
     {
     Id = id,
 Title = title,
   Description = description,
   Status = status,
            Priority = priority,
    StartDate = startDate,
          DueDate = dueDate,
      CreatedAt = createdAt,
    UpdatedAt = updatedAt
        };

        // Assert
        task.Should().NotBeNull();
        task.Id.Should().Be(id);
        task.Title.Should().Be(title);
        task.Description.Should().Be(description);
        task.Status.Should().Be(status);
      task.Priority.Should().Be(priority);
     task.StartDate.Should().Be(startDate);
        task.DueDate.Should().Be(dueDate);
        task.CreatedAt.Should().Be(createdAt);
        task.UpdatedAt.Should().Be(updatedAt);
    }

    [Fact]
    public void TaskEntity_MinimalScenario_ShouldCreateWithRequiredFieldsOnly()
    {
        // Arrange
        var id = Guid.NewGuid();
        var title = "Minimal Task";

        // Act
    var task = new TaskEntity
        {
Id = id,
 Title = title
      };

   // Assert
        task.Should().NotBeNull();
        task.Id.Should().Be(id);
        task.Title.Should().Be(title);
        task.Description.Should().BeNull();
        task.Status.Should().Be(TaskStatus.Pending);
        task.Priority.Should().Be(TaskPriority.Low);
        task.StartDate.Should().BeNull();
 task.DueDate.Should().BeNull();
    }

    [Fact]
    public void TaskEntity_ModificationScenario_ShouldAllowPropertyModification()
    {
        // Arrange
   var task = new TaskEntity
        {
         Id = Guid.NewGuid(),
  Title = "Original Title",
            Status = TaskStatus.Pending,
            Priority = TaskPriority.Low
        };

// Act
      task.Title = "Modified Title";
    task.Status = TaskStatus.Completed;
      task.Priority = TaskPriority.High;
        task.Description = "Added description";

      // Assert
        task.Title.Should().Be("Modified Title");
        task.Status.Should().Be(TaskStatus.Completed);
        task.Priority.Should().Be(TaskPriority.High);
        task.Description.Should().Be("Added description");
    }

    #endregion
}
