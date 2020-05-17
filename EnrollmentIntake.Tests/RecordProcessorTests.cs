using System;
using EnrollmentIntake.Interfaces;
using EnrollmentIntake.Models;
using EnrollmentIntake.Models.Enrollment;
using EnrollmentIntake.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EnrollmentIntake.Tests
{
    [TestClass]
    public class EnrollmentDateRulesTests
    {
        [TestMethod]
        public void Do_DOB_Nineteen_Years_Ago_And_EffectiveDate_Now_Returns_True()
        {
            //Arrange
            var model = new EnrollmentRecord
            {
                DOB = DateTime.Now.AddYears(-19),
                EffectiveDate = DateTime.Now
            };

            var sut = new EnrollmentDateRules();

            //Act
            var result = sut.Do(model);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Do_DOB_Eighteen_Years_Ago_And_EffectiveDate_Now_Returns_True()
        {
            //Arrange
            var model = new EnrollmentRecord
            {
                DOB = DateTime.Now.AddYears(-18),
                EffectiveDate = DateTime.Now
            };

            var sut = new EnrollmentDateRules();

            //Act
            var result = sut.Do(model);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Do_DOB_Seventeen_Years_Ago_And_EffectiveDate_Now_Returns_False()
        {
            //Arrange
            var model = new EnrollmentRecord
            {
                DOB = DateTime.Now.AddYears(-17),
                EffectiveDate = DateTime.Now
            };

            var sut = new EnrollmentDateRules();

            //Act
            var result = sut.Do(model);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Do_DOB_Eighteen_Years_In_The_Future_And_EffectiveDate_Now_Returns_False()
        {
            //Arrange
            var model = new EnrollmentRecord
            {
                DOB = DateTime.Now.AddYears(18),
                EffectiveDate = DateTime.Now
            };

            var sut = new EnrollmentDateRules();

            //Act
            var result = sut.Do(model);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Do_DOB_Twenty_Years_Ago_And_EffectiveDate_One_Hundred_Years_Ago_Returns_True()
        {
            //Arrange
            var model = new EnrollmentRecord
            {
                DOB = DateTime.Now.AddYears(-20),
                EffectiveDate = DateTime.Now.AddYears(-100)
            };

            var sut = new EnrollmentDateRules();

            //Act
            var result = sut.Do(model);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Do_DOB_Twenty_Years_Ago_And_EffectiveDate_Thirty_Days_In_Future_Returns_True()
        {
            //Arrange
            var model = new EnrollmentRecord
            {
                DOB = DateTime.Now.AddYears(-20),
                EffectiveDate = DateTime.Now.AddDays(30)
            };

            var sut = new EnrollmentDateRules();

            //Act
            var result = sut.Do(model);

            //Assert
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void Do_DOB_Twenty_Years_Ago_And_EffectiveDate_Thirty_One_Days_In_Future_Returns_False()
        {
            //Arrange
            var model = new EnrollmentRecord
            {
                DOB = DateTime.Now.AddYears(-20),
                EffectiveDate = DateTime.Now.AddDays(31)
            };

            var sut = new EnrollmentDateRules();

            //Act
            var result = sut.Do(model);

            //Assert
            Assert.IsFalse(result);
        }
    }
}
