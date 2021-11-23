using ApiUser = Management.ApiModels.User;
using DomainUser = Management.DomainModels.User;

namespace Management.Mapping
{
    /// <summary>
    /// Mapper class that maps domain user to api user
    /// </summary>
    public class DomainToApiMapper
    {
        /// <summary>
        /// Maps domain user to api user
        /// </summary>
        /// <param name="domain">domain user.</param>
        /// <returns>api user.</returns>
        public static ApiUser ToApi(DomainUser domain)
            => new ApiUser
            {
                UserId = domain.UserId.Value,
                FullName = domain.FullName.Value,
                PassportId = domain.PassportId.ToString(),
                CreatedAt = domain.CreatedAt,
                CountryCode = domain.CountryCode.ToString(),
            };

        public static ApiModels.Recommendation ToApi(DomainModels.Recommendation recommendation)
            => new ApiModels.Recommendation
                {
                    CountryCode = recommendation.CountryCode.ToString(),
                    State = recommendation.State.Value,
                    RecommendationState = recommendation.RecommendationState.ToString(),
                    OverallScore = recommendation.OverallScore,
                    AirQualityScore = recommendation.AirQualityScore,
                    CovidIndexScore = recommendation.CovidIndexScore,
                    WeatherScore = recommendation.WeatherScore,
                };
    }
}
