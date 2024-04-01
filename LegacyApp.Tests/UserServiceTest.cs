using System;
using JetBrains.Annotations;
using LegacyApp;
using Xunit;

namespace LegacyApp.Tests;

[TestSubject(typeof(UserService))]
public class UserServiceTest
{

    [Fact]
    public void AddUser_Should_Return_False_When_FirstName_Is_Incorrect()
    {
        // Arrange - przygotowanie zaleznosci z ktorych bedziemy korzystac w trakcie testu
        var userService = new UserService();
        // Act - wywolanie testowanej funkconalnosci
        var addResult = userService.AddUser("", "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);
        // Assert - sprawdzenie rezultatow
        // Assert.Equal(false, addResult);
        Assert.False(addResult);
    }
    
    [Fact]
    public void AddUser_Should_Return_False_When_Email_Is_Incorrect()
    {
        // Arrange - przygotowanie zaleznosci z ktorych bedziemy korzystac w trakcie testu
        var userService = new UserService();
        // Act - wywolanie testowanej funkconalnosci
        var addResult = userService.AddUser("John", "Doe", "johndoegmailcom", DateTime.Parse("1982-03-21"), 1);
        // Assert - sprawdzenie rezultatow
        // Assert.Equal(false, addResult);
        Assert.False(addResult);
    }
}