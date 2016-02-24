using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bindingDemo
{
    public class HockeyTeam
    {
        #region PROPERTIES
        // huom public field ei kelpaa WPF:n databindingissä, pitää olla Property
        public string Name { get; set; }
        public string City { get; set; }
        #endregion
        #region CONSTRUCTORS
        public HockeyTeam()
        {
            Name = "Anon";
            City = "Unknown";
        }
        public HockeyTeam (string name, string city)
        {
            Name = name;
            City = city;
        }
        #endregion
        public override string ToString()
        {
            return Name + "@" + City;
        }
    }

    

    public class HockeyLeague
    {
        // perustetaan SM-liiga joka sisältää x kpl joukkueita
        // HUOM! jos halutaan, että databindaus huomaa muutokset kokoelmassa automaattisesti, kaytä ObservableCollection-kokoelmaa 
        List<HockeyTeam> teams = new List<HockeyTeam>();
        public HockeyLeague()
        {
            teams.Add(new HockeyTeam("HIFK", "Helsinki"));
            teams.Add(new HockeyTeam("JYP", "Jyväskylä"));
            teams.Add(new HockeyTeam("Kalpa", "Kuopio"));
            teams.Add(new HockeyTeam("Sport", "Vaasa"));
        }
        // metodi joka palauttaa liigan joukkueet
        public List<HockeyTeam> GetTeams()
        {
            return teams;
        }
    }
}
