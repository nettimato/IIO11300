using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bindingDemo
{
    public class HockeyPlayer : INotifyPropertyChanged
    {
        public string name, number;
        public string Name
        {
            set
            {
                name = value;
                Notify("Name");
                Notify("NameAndNumber");
            }
            get
            {
                return name;
            }
        }
        public string Number
        {
            set
            {
                number = value;
                Notify("Number");
                Notify("NameAndNumber");
            }
            get
            {
                return number;
            }
        }
        public string NameAndNumber
        {
            get
            {
                return name + " #" + number;
            }
        }
        // constructor
        public HockeyPlayer()
        {

        }
        public HockeyPlayer(string name, string number)
        {
            this.name = name;
            this.number = number;
        }
        // methods
        public override string ToString()
        {
            return name + " #" + number;
        }
        // INotifyPropertyChanged members
        public event PropertyChangedEventHandler PropertyChanged;
        void Notify(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
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
            return Name + " @ " + City;
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
