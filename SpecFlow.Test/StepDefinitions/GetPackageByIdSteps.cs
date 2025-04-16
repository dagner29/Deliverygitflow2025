using FluentAssertions;
using TechTalk.SpecFlow;
//using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using TechTalk.SpecFlow;
//using Delivery.API; // Asegúrate de que el namespace es correcto

namespace SpecFlow.Test.StepDefinitions
{
    [Binding]
    public class GetPackageByIdSteps
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private HttpResponseMessage _response;

        public GetPackageByIdSteps()
        {
            // Configuración para el servidor de prueba (donde 'Startup' es tu clase de configuración del API)
            _factory = new WebApplicationFactory<Startup>();
        
        }

        [Given("the package with ID \"(.*)\" exists")]
        public async Task GivenThePackageWithIdExists(string packageId)
        {
            // Aquí puedes crear el paquete en la base de datos o simularlo de alguna manera
            // Para pruebas reales, deberías agregar un paquete con ese ID en el contexto de pruebas.
            // O puedes usar una base de datos en memoria o cualquier mecanismo que se use en tus pruebas.
            // Simulando que el paquete existe en este escenario
            await Task.CompletedTask;
        }

        [Given("the package with ID \"(.*)\" does not exist")]
        public async Task GivenThePackageWithIdDoesNotExist(string packageId)
        {
            // Simulando que el paquete no existe en la base de datos
            await Task.CompletedTask;
        }

        [When("I send a GET request to \"(.*)\"")]
        public async Task WhenISendAGETRequestTo(string url)
        {
            var client = _factory.CreateClient();
            _response = await client.GetAsync(url);
        }

        [Then("the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int expectedStatusCode)
        {
            _response.StatusCode.ShouldBe((System.Net.HttpStatusCode)expectedStatusCode);
        }

        [Then("the response body should contain (.*)")]
        public async Task ThenTheResponseBodyShouldContain(string expectedMessage)
        {
            var content = await _response.Content.ReadAsStringAsync();
            content.ShouldContain(expectedMessage);
        }
    }

}