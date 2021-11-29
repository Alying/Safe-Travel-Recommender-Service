using System.Collections.Generic;
using Management.DomainModels;

namespace Management
{
    /// <summary>
    /// Static maps that maps abbreviated states to full name states and supported cities
    /// </summary>
    public static class AbbreviationToState
    {
        private static readonly Dictionary<string, State> _stateLookUp = new Dictionary<string, State>
        {
            { "AK", State.Wrap("Alaska") },
            { "AL", State.Wrap("Alabama") },
            { "AR", State.Wrap("Arkansas") },
            { "AZ", State.Wrap("Arizona") },
            { "CA", State.Wrap("California") },
            { "CO", State.Wrap("Colorado") },
            { "CT", State.Wrap("Connecticut") },
            { "DC", State.Wrap("Washington, D.C.") },
            { "DE", State.Wrap("Delaware") },
            { "FL", State.Wrap("Florida") },
            { "GA", State.Wrap("Georgia") },
            { "HI", State.Wrap("Hawaii") },
            { "IA", State.Wrap("Iowa") },
            { "ID", State.Wrap("Idaho") },
            { "IL", State.Wrap("Illinois") },
            { "IN", State.Wrap("Indiana") },
            { "KS", State.Wrap("Kansas") },
            { "KY", State.Wrap("Kentucky") },
            { "LA", State.Wrap("Louisiana") },
            { "MA", State.Wrap("Massachusetts") },
            { "MD", State.Wrap("Maryland") },
            { "ME", State.Wrap("Maine") },
            { "MI", State.Wrap("Michigan") },
            { "MN", State.Wrap("Minnesota") },
            { "MO", State.Wrap("Missouri") },
            { "MS", State.Wrap("Mississippi") },
            { "MT", State.Wrap("Montana") },
            { "NC", State.Wrap("North Carolina") },
            { "ND", State.Wrap("North Dakota") },
            { "NE", State.Wrap("Nebraska") },
            { "NH", State.Wrap("New Hampshire") },
            { "NJ", State.Wrap("New Jersey") },
            { "NM", State.Wrap("New Mexico") },
            { "NV", State.Wrap("Nevada") },
            { "NY", State.Wrap("New York") },
            { "OH", State.Wrap("Ohio") },
            { "OK", State.Wrap("Oklahoma") },
            { "OR", State.Wrap("Oregon") },
            { "PA", State.Wrap("Pennsylvania") },
            { "RI", State.Wrap("Rhode Island") },
            { "SC", State.Wrap("South Carolina") },
            { "SD", State.Wrap("South Dakota") },
            { "TN", State.Wrap("Tennessee") },
            { "TX", State.Wrap("Texas") },
            { "UT", State.Wrap("Utah") },
            { "VA", State.Wrap("Virginia") },
            { "VT", State.Wrap("Vermont") },
            { "WA", State.Wrap("Washington") },
            { "WI", State.Wrap("Wisconsin") },
            { "WV", State.Wrap("West Virginia") },
            { "WY", State.Wrap("Wyoming") },
        };

        private static readonly Dictionary<string, List<City>> _cityLookUp = new Dictionary<string, List<City>>()
        {
            { "AK", new List<City>() { City.Wrap("Juneau"), City.Wrap("College"), City.Wrap("Anchorage"), City.Wrap("Healy"), City.Wrap("Valdez") } },
            { "AL", new List<City>() { City.Wrap("Montgomery"), City.Wrap("Birmingham"), City.Wrap("Huntsville"), City.Wrap("Irondale"), City.Wrap("Cuba") } },
            { "AR", new List<City>() { City.Wrap("Fayetteville"), City.Wrap("Springdale"), City.Wrap("Bella Vista"), City.Wrap("Jacksonville"), City.Wrap("Marion") } },
            { "AZ", new List<City>() { City.Wrap("Sacaton"), City.Wrap("Sedona"), City.Wrap("Payson"), City.Wrap("Hidden Valley"), City.Wrap("Buckeye") } },
            { "CA", new List<City>() { City.Wrap("Emeryville"), City.Wrap("Cedar Ridge"), City.Wrap("Big Pine"), City.Wrap("Bolinas"), City.Wrap("San Diego") } },
            { "CO", new List<City>() { City.Wrap("Englewood"), City.Wrap("Indian Hills"), City.Wrap("Paonia"), City.Wrap("Twin Lakes"), City.Wrap("Keystone") } },
            { "CT", new List<City>() { City.Wrap("New Haven"), City.Wrap("Westport"), City.Wrap("Storrs"), City.Wrap("South Coventry"), City.Wrap("Darien") } },
            { "DC", new List<City>() { City.Wrap("Chevy Chase"), City.Wrap("Washington") } },
            { "DE", new List<City>() { City.Wrap("Milton"), City.Wrap("Rehoboth Beach"), City.Wrap("Bear"), City.Wrap("Harrington"), City.Wrap("New Castle") } },
            { "FL", new List<City>() { City.Wrap("Olustee"), City.Wrap("Spring Hill"), City.Wrap("Palmetto"), City.Wrap("The Crossings"), City.Wrap("Bonita Springs") } },
            { "GA", new List<City>() { City.Wrap("Avondale Estates"), City.Wrap("Tucker"), City.Wrap("Fannin"), City.Wrap("Marietta"), City.Wrap("North Druid Hills") } },
            { "HI", new List<City>() { City.Wrap("Kahului"), City.Wrap("Mountain View"), City.Wrap("Kihei"), City.Wrap("Lihue"), City.Wrap("Ocean View") } },
            { "IA", new List<City>() { City.Wrap("Emmetsburg"), City.Wrap("Council Bluffs"), City.Wrap("Cedar Rapids"), City.Wrap("Muscatine"), City.Wrap("Keosauqua") } },
            { "ID", new List<City>() { City.Wrap("Nezperce"), City.Wrap("Post Falls"), City.Wrap("Nampa"), City.Wrap("Arbon"), City.Wrap("Idaho Falls") } },
            { "IL", new List<City>() { City.Wrap("Chicago"), City.Wrap("Joliet"), City.Wrap("Naperville"), City.Wrap("Wilmington"), City.Wrap("Riverside") } },
            { "IN", new List<City>() { City.Wrap("Hanover"), City.Wrap("Greencastle"), City.Wrap("Portage"), City.Wrap("Santa Claus"), City.Wrap("Warren Park") } },
            { "KS", new List<City>() { City.Wrap("Hutchinson"), City.Wrap("Dodge City"), City.Wrap("Wichita"), City.Wrap("Lawrence"), City.Wrap("Halstead") } },
            { "KY", new List<City>() { City.Wrap("Hazard"), City.Wrap("Smiths Grove"), City.Wrap("Oak Grove"), City.Wrap("Ashland"), City.Wrap("Louisville") } },
            { "LA", new List<City>() { City.Wrap("Shreveport"), City.Wrap("Central"), City.Wrap("Slidell"), City.Wrap("Montegut"), City.Wrap("Swartz") } },
            { "MA", new List<City>() { City.Wrap("Brockton"), City.Wrap("Barre"), City.Wrap("Brookline"), City.Wrap("Medford"), City.Wrap("Swampscott") } },
            { "MD", new List<City>() { City.Wrap("Cheverly"), City.Wrap("Elkton"), City.Wrap("Redland"), City.Wrap("Hagerstown"), City.Wrap("Beltsville") } },
            { "ME", new List<City>() { City.Wrap("Lisbon"), City.Wrap("Hallowell"), City.Wrap("Madawaska"), City.Wrap("Portland"), City.Wrap("Washington") } },
            { "MI", new List<City>() { City.Wrap("Pigeon"), City.Wrap("Ann Arbor"), City.Wrap("Pontiac"), City.Wrap("Midland"), City.Wrap("Walker") } },
            { "MN", new List<City>() { City.Wrap("Woodbury"), City.Wrap("Grand Marais"), City.Wrap("Sturgeon Lake"), City.Wrap("Crosby"), City.Wrap("Pequot Lakes") } },
            { "MO", new List<City>() { City.Wrap("Olivette"), City.Wrap("Sunset Hills"), City.Wrap("Wentzville"), City.Wrap("Stoutsville"), City.Wrap("Town and Country") } },
            { "MS", new List<City>() { City.Wrap("Cleveland"), City.Wrap("Lumberton"), City.Wrap("Jackson"), City.Wrap("Hattiesburg"), City.Wrap("Waveland") } },
            { "MT", new List<City>() { City.Wrap("Frenchtown"), City.Wrap("Evergreen"), City.Wrap("Lewistown"), City.Wrap("West Yellowstone"), City.Wrap("Broadus") } },
            { "NC", new List<City>() { City.Wrap("Fayetteville"), City.Wrap("Statesville"), City.Wrap("Scotts Mill"), City.Wrap("Huntersville"), City.Wrap("Princeton") } },
            { "ND", new List<City>() { City.Wrap("Fordville"), City.Wrap("Bismarck"), City.Wrap("Medora"), City.Wrap("Bottineau"), City.Wrap("Belcourt") } },
            { "NE", new List<City>() { City.Wrap("Scottsbluff"), City.Wrap("Weeping Water"), City.Wrap("Lincoln"), City.Wrap("Valentine"), City.Wrap("Grand Island") } },
            { "NH", new List<City>() { City.Wrap("Brentwood"), City.Wrap("Chesterfield"), City.Wrap("Londonderry"), City.Wrap("Westmoreland"), City.Wrap("Gilford") } },
            { "NJ", new List<City>() { City.Wrap("Raritan"), City.Wrap("High Bridge"), City.Wrap("West Milford"), City.Wrap("North Beach Haven"), City.Wrap("Salem") } },
            { "NM", new List<City>() { City.Wrap("Edgewood"), City.Wrap("South Valley"), City.Wrap("Carlsbad"), City.Wrap("Las Cruces"), City.Wrap("Los Alamos") } },
            { "NV", new List<City>() { City.Wrap("Virginia City"), City.Wrap("Yerington"), City.Wrap("Indian Hills"), City.Wrap("Fernley"), City.Wrap("Smith Valley") } },
            { "NY", new List<City>() { City.Wrap("Naples"), City.Wrap("Port Richmond"), City.Wrap("Glen Cove"), City.Wrap("White Plains"), City.Wrap("Katonah") } },
            { "OH", new List<City>() { City.Wrap("Athens"), City.Wrap("Chesterland"), City.Wrap("Harrison"), City.Wrap("Kirtland"), City.Wrap("Orrville") } },
            { "OK", new List<City>() { City.Wrap("Tulsa"), City.Wrap("McAlester"), City.Wrap("Ardmore"), City.Wrap("Miami"), City.Wrap("Lawton") } },
            { "OR", new List<City>() { City.Wrap("Redwood"), City.Wrap("Cottage Grove"), City.Wrap("West Linn"), City.Wrap("La Pine"), City.Wrap("Redmond") } },
            { "PA", new List<City>() { City.Wrap("Wescosville"), City.Wrap("Beaver"), City.Wrap("Port Vue"), City.Wrap("Trafford"), City.Wrap("Waynesboro") } },
            { "RI", new List<City>() { City.Wrap("Providence"), City.Wrap("North Kingstown"), City.Wrap("West Greenwich"), City.Wrap("Rumford"), City.Wrap("Newport") } },
            { "SC", new List<City>() { City.Wrap("Aiken"), City.Wrap("Charleston"), City.Wrap("Simpsonville"), City.Wrap("Fort Mill"), City.Wrap("Blacksburg") } },
            { "SD", new List<City>() { City.Wrap("Summerset"), City.Wrap("Aberdeen"), City.Wrap("Rapid City"), City.Wrap("Hot Springs"), City.Wrap("Crooks") } },
            { "TN", new List<City>() { City.Wrap("Maryville"), City.Wrap("Kingsport"), City.Wrap("Ridgetop"), City.Wrap("Townsend"), City.Wrap("Gatlinburg") } },
            { "TX", new List<City>() { City.Wrap("McAllen"), City.Wrap("Southlake"), City.Wrap("Seabrook"), City.Wrap("Laredo"), City.Wrap("Mission") } },
            { "UT", new List<City>() { City.Wrap("Centerville"), City.Wrap("Santa Clara"), City.Wrap("Granite"), City.Wrap("Highland"), City.Wrap("South Jordan Heights") } },
            { "VA", new List<City>() { City.Wrap("Crozet"), City.Wrap("Vinton"), City.Wrap("West Gate"), City.Wrap("Springfield"), City.Wrap("Stephens City") } },
            { "VT", new List<City>() { City.Wrap("Washington"), City.Wrap("Bennington"), City.Wrap("Burlington"), City.Wrap("Hyde Park"), City.Wrap("Windsor") } },
            { "WA", new List<City>() { City.Wrap("Mead"), City.Wrap("Medina"), City.Wrap("Mirrormont"), City.Wrap("Seattle"), City.Wrap("Yacolt") } },
            { "WI", new List<City>() { City.Wrap("La Crosse"), City.Wrap("Madison"), City.Wrap("Butler"), City.Wrap("Baraboo"), City.Wrap("Rice Lake") } },
            { "WV", new List<City>() { City.Wrap("Wheeling"), City.Wrap("Shannondale"), City.Wrap("Huntington"), City.Wrap("Washington"), City.Wrap("Martinsburg") } },
            { "WY", new List<City>() { City.Wrap("South Greeley"), City.Wrap("Kaycee"), City.Wrap("Moose Wilson Road"), City.Wrap("Wilson"), City.Wrap("Saratoga") } },
        };

        /// <summary>
        /// Get the state's full name
        /// </summary>
        /// <param name="abbreviation">the abbreviated name of the state.</param>
        /// <returns>the state's full name</returns>
        public static State GetStateFullName(string abbreviation)
            => _stateLookUp[abbreviation];

        /// <summary>
        /// Get supported cities of a state
        /// </summary>
        /// <param name="state">the name of the state.</param>
        /// <returns>the supported cities of the state.</returns>
        public static IEnumerable<City> GetSupportedCities(string state)
            => _cityLookUp[state];
    }
}
