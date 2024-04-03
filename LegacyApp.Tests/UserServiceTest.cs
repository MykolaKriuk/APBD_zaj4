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
    
    [Fact]
    public void AddUser_Should_Return_False_When_Age_Is_Less_Then_21()
    {
        
        var userService = new UserService();
        
        var addResult = userService.AddUser(
            "John", 
            "Doe", 
            "johndoe@gmail.com", 
            DateTime.Parse("2008-03-21"), 
            1);
        
        Assert.False(addResult);
    }
    
    [Fact]
    public void Checking_If_Creation_Of_UserService_Object_With_Params_Was_Successful()
    {
        var userService = new UserService(new ClientRepository(), new UserCreditService());
        var addResult = userService.AddUser(
            "John", 
            "Doe", 
            "johndoe@gmail.com", 
            DateTime.Parse("1982-03-21"), 
            1);
        
        Assert.True(addResult);
    }
    
    [Fact]
    public void AddUser_Should_Return_False_If_User_Has_Credit_Limit_Less_Then_500()
    {
        var userService = new UserService(new ClientRepository(), new UserCreditService());
        var addResult = userService.AddUser(
            "Jan", 
            "Kowalski", 
            "kowal@gmail.com", 
            DateTime.Parse("1982-03-21"), 
            1);
        
        Assert.False(addResult);
    }
    
    [Fact]
    public void Checking_If_Very_Important_Client_Is_Added_Correctly()
    {
        var userService = new UserService(new ClientRepository(), new UserCreditService());
        var addResult = userService.AddUser(
            "Jan", 
            "Kowalski", 
            "kowal@gmail.com", 
            DateTime.Parse("1982-03-21"), 
            2);
        
        Assert.True(addResult);
    }
    
    [Fact]
    public void Checking_If_Important_Client_Is_Added_Correctly()
    {
        var userService = new UserService(new ClientRepository(), new UserCreditService());
        var addResult = userService.AddUser(
            "Jan", 
            "Kowalski", 
            "kowal@gmail.com", 
            DateTime.Parse("1982-03-21"), 
            3);
        
        Assert.True(addResult);
    }
    
    [Fact]
    public void AddUser_Should_Give_ArgumentException_If_No_Such_User_In_ClientRepository()
    {
        var userService = new UserService(new ClientRepository(), new UserCreditService());

        Assert.Throws<ArgumentException>(() => userService.AddUser(
            "Jan", 
            "Kowalski", 
            "kowal@gmail.com", 
            DateTime.Parse("1982-03-21"), 
            10)
        );
    }
    
    [Fact]
    public void AddUser_Should_Give_ArgumentException_If_No_Such_User_In_UserCreditService()
    {
        var userService = new UserService(new ClientRepository(), new UserCreditService());

        Assert.Throws<ArgumentException>(() => userService.AddUser(
            "Jan", 
            "Kowalchuk", 
            "kowal@gmail.com", 
            DateTime.Parse("1982-03-21"), 
            1)
        );
    }
}