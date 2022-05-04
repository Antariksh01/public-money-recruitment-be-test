using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using VacationRental.Domain.Models;
using Xunit;

namespace VacationRental.Api.Tests
{
    [Collection("Integration")]
    public class PostRentalTests
    {
        private readonly HttpClient _client;

        public PostRentalTests(IntegrationFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task GivenCompleteRequest_WhenPostRental_ThenAGetReturnsTheCreatedRental()
        {
            var request = new RentalBindingModel
            {
                Units = 25,
                PreparationTimeInDays = 20
            };

            ResourceIdViewModel postResult;
            using (var postResponse = await _client.PostAsJsonAsync($"/api/v1/rentals", request))
            {
                Assert.True(postResponse.IsSuccessStatusCode);
                postResult = await postResponse.Content.ReadAsAsync<ResourceIdViewModel>();
            }

            using (var getResponse = await _client.GetAsync($"/api/v1/rentals/{postResult.Id}"))
            {
                Assert.True(getResponse.IsSuccessStatusCode);

                var getResult = await getResponse.Content.ReadAsAsync<RentalViewModel>();
                Assert.Equal(request.Units, getResult.Units);
                Assert.Equal(request.PreparationTimeInDays, getResult.PreparationTimeInDays);
            }
        }

        [Fact]
        public async Task GivenCompleteRequest_WhenPostRental_ThenAGetReturnsTheCreatedRental_ThenUpdateRental()
        {
            var postRentalRequest = new RentalBindingModel
            {
                Units = 25,
                PreparationTimeInDays = 25
            };

            ResourceIdViewModel postRentalResult;
            using (var postRentalResponse = await _client.PostAsJsonAsync($"/api/v1/rentals", postRentalRequest))
            {
                Assert.True(postRentalResponse.IsSuccessStatusCode);
                postRentalResult = await postRentalResponse.Content.ReadAsAsync<ResourceIdViewModel>();
            }

            using (var getResponse = await _client.GetAsync($"/api/v1/rentals/{postRentalResult.Id}"))
            {
                Assert.True(getResponse.IsSuccessStatusCode);


                var getResult = await getResponse.Content.ReadAsAsync<RentalViewModel>();
                Assert.Equal(postRentalRequest.Units, getResult.Units);
                Assert.Equal(postRentalRequest.PreparationTimeInDays, getResult.PreparationTimeInDays);
            }

            var updateRentalRequest = new RentalBindingModel
            {
                Units = 55,
                PreparationTimeInDays = 55
            };

            ResourceIdViewModel putRentalResult;
            using (var putResponse = await _client.PutAsJsonAsync($"/api/v1/rentals/{postRentalResult.Id}", updateRentalRequest))
            {
                Assert.True(putResponse.IsSuccessStatusCode);
                putRentalResult = await putResponse.Content.ReadAsAsync<ResourceIdViewModel>();
            }

            using (var getPutResponse = await _client.GetAsync($"/api/v1/rentals/{putRentalResult.Id}"))
            {
                Assert.True(getPutResponse.IsSuccessStatusCode);

                var get2Result = await getPutResponse.Content.ReadAsAsync<RentalViewModel>();

                Assert.Equal(updateRentalRequest.Units, get2Result.Units);
                Assert.Equal(updateRentalRequest.PreparationTimeInDays, get2Result.PreparationTimeInDays);
            }
        }
    }
}
