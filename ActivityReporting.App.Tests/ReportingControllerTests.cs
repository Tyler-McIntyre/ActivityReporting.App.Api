using NUnit.Framework;
using ActivityReporting.App.Api.Controllers;
using ActivityReporting.App.Api.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using ActivityReporting.App.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace ActivityReporting.App.Tests
{
    public class ActivityReportingTests
    {
        MemoryCache cache;
        ReportController reportController;

        [SetUp]
        public void Setup()
        {
            cache = new(new MemoryCacheOptions());
            reportController = new ReportController(cache);
        }

        [Test]
        [TestCase("Time Sheets", 2)]
        [TestCase("Generic Activity", 3)]
        public void PostKeyValue_NewKey_ReturnsOK(string key, decimal value)
        {
            //Arrange
            ActivityDto activityDto = (ActivityDto)Factory.CreateNewActivity();
            activityDto.Value = value;

            //Act
            ObjectResult response = (ObjectResult)reportController.NewActivityEntry(activityDto, key);
            
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
        }

        [Test]
        [TestCase("Time Sheets", 2)]
        [TestCase("Generic Activity", 3)]
        public void GetKeyValue_RetrieveList_ReturnsList(string key, decimal value)
        {
            //Arrange
            ActivityDto activityDto = (ActivityDto)Factory.CreateNewActivity();
            activityDto.Value = value;

            reportController.NewActivityEntry(activityDto, key);

            //Act
            ObjectResult response = (ObjectResult)reportController.GetKeyValue(key).Result;
            
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(value, response.Value);
        }
    }
}