using System.Net;
using System.Net.Http;
using System.Text;

using CodeChallenge.Models;

using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeChallenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void CreateCompensation_Returns_Created()
        {
            // Arrange
            var employee = new Employee()
            {
                EmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f",
                Department = "Engineering",
                FirstName = "John",
                LastName = "Lennon",
                Position = "Development Manager",
            };

            var compensation = new Compensation()
            {
                Employee = employee,
                Salary = 60000,
                EffectiveDate = "09/11/2022"
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newCompensation = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(newCompensation.CompensationId);
            Assert.AreEqual(compensation.Employee.EmployeeId, newCompensation.Employee.EmployeeId);
            Assert.AreEqual(compensation.Salary, newCompensation.Salary);
            Assert.AreEqual(compensation.EffectiveDate, newCompensation.EffectiveDate);
        }

        [TestMethod]
        public void GetCompensationByEmployeeId_Returns_Ok()
        {
            // Arrange
            var e = new Employee()
            {
                EmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f",
                Department = "Engineering",
                FirstName = "John",
                LastName = "Lennon",
                Position = "Development Manager",
            };

            var c = new Compensation()
            {
                Employee = e,
                Salary = 60000,
                EffectiveDate = "09/11/2022"
            };

            var requestContent = new JsonSerialization().ToJson(c);

            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            var expectedSalary = 60000;
            var expectedEffectiveDate = "09/11/2022";

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));

            var getRequestTask = _httpClient.GetAsync($"api/compensation/employee/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var compensation = response.DeserializeContent<Compensation>();
            Assert.AreEqual(employeeId, compensation.Employee.EmployeeId);
            Assert.AreEqual(expectedSalary, compensation.Salary);
            Assert.AreEqual(expectedEffectiveDate, compensation.EffectiveDate);
        }
    }
}
