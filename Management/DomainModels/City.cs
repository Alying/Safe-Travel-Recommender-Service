using Common;

namespace Management.DomainModels
{
    public class City : TaggedString<City>
    {
        private City(string city)
                : base(city)
        {
        }

        public static City Wrap(string city) => new City(city);
    }
}
