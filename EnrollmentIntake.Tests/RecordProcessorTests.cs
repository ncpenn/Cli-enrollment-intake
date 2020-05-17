using EnrollmentIntake.Interfaces;
using EnrollmentIntake.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EnrollmentIntake.Tests
{
    [TestClass]
    public class RecordProcessorTest
    {
        private Mock<IRules<UnprocessedModel>> rulesMock;

        public RecordProcessorTest()
        {
            rulesMock = new Mock<IRules<UnprocessedModel>>();
        }

        [TestMethod]
        public void ProcessRecord_Maps_Processed_Record()
        {
            //Arrange
            var unprocessed = new UnprocessedModel
            {
                Str = "twinkle, twinkle little bat",
                Number = 42
            };

            rulesMock.Setup(x => x.Do(It.IsAny<UnprocessedModel>())).Returns(true);
            var sut = new RecordProcessor<UnprocessedModel, ProcessedModel>(rulesMock.Object);

            //Act
            var result = sut.ProcessRecord(unprocessed);

            //Assert
            Assert.AreEqual(unprocessed.Number, result.Number);
            Assert.AreEqual(unprocessed.Str, result.Str);
        }

        [TestMethod]
        public void ProcessRecord_Rules_False_Gives_Status_Rejected()
        {
            //Arrange
            var unprocessed = new UnprocessedModel();

            rulesMock.Setup(x => x.Do(It.IsAny<UnprocessedModel>())).Returns(false);
            var sut = new RecordProcessor<UnprocessedModel, ProcessedModel>(rulesMock.Object);

            //Act
            var result = sut.ProcessRecord(unprocessed);

            //Assert
            Assert.AreEqual(Status.Rejected, result.RecordStatus);
        }

        [TestMethod]
        public void ProcessRecord_Rules_True_Gives_Status_Accepted()
        {
            //Arrange
            var unprocessed = new UnprocessedModel();

            rulesMock.Setup(x => x.Do(It.IsAny<UnprocessedModel>())).Returns(true);
            var sut = new RecordProcessor<UnprocessedModel, ProcessedModel>(rulesMock.Object);

            //Act
            var result = sut.ProcessRecord(unprocessed);

            //Assert
            Assert.AreEqual(Status.Accepted, result.RecordStatus);
        }

        [TestMethod]
        public void ProcessRecord_Event_Is_Raised_With_Processed_Model()
        {
            //Arrange
            ProcessedModel processed = null;

            var unprocessed = new UnprocessedModel
            {
                Str = "A long time ago, in a galaxy far, far away",
                Number = 0.01
            };

            rulesMock.Setup(x => x.Do(It.IsAny<UnprocessedModel>())).Returns(true);
            var sut = new RecordProcessor<UnprocessedModel, ProcessedModel>(rulesMock.Object);
            sut.RecordReceivedEvent += (model) => processed = model;

            //Act
            sut.ProcessRecord(unprocessed);

            //Assert
            Assert.AreEqual(Status.Accepted, processed.RecordStatus);
            Assert.AreEqual(unprocessed.Number, processed.Number);
            Assert.AreEqual(unprocessed.Str, processed.Str);
        }
    }

    public class UnprocessedModel
    {
        public string Str { get; set; }
        public double Number { get; set; }
    }

    public class ProcessedModel : UnprocessedModel, IProcessedRecord
    {
        public Status RecordStatus { get; set; }
    }
}
