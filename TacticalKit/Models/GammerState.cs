using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace TacticalKit.Models
{
    public class StatePackage
    {
        public string group { get; set; }
        public IList<GameState> infos { get; set; }
        public IList<GammerState> gammers { get; set; }
        public IList<Overlay> overlays { get; set; }
    }
    public class GameState
    {
        public string identifier { get; set; }
        public string group { get; set; }
        public dynamic info { get; set; }
        public void sync(GameState src)
        {
            this.group = src.group;
            this.info = src.info; // @todo: may have problem in the future
        }
    }
    public class GammerState
    {
        public const string ALIVE = "ALIVE";
        public const string DEAD = "DEAD";
        public GammerState()
        {
            this.charactor = new Charactor() { 
                identifier = string.Empty,
                name = Charactor.ANONYMOUS,
                credential = string.Empty
            };
            this.token = string.Empty;
            this.group = string.Empty;
            this.status = ALIVE;
        }
        public Charactor charactor { get; set; }
        public string token { get; set; }
        public string group { get; set; }
        public string status { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double altitude { get; set; }
        public double direction { get; set; }
        public double lastModified { get; set; }
        public void sync(GammerState src)
        {
            this.latitude = src.latitude;
            this.longitude = src.longitude;
            this.altitude = src.altitude;
            this.direction = src.direction;
            this.group = src.group;
            this.status = src.status;
        }
    }
    public class Charactor
    {
        public const string ANONYMOUS = "ANONYMOUS";
        private Dictionary<string, object> dictionary = new Dictionary<string, object>();
        public string identifier { get; set; }
        public string name { get; set; }
        public string credential { get; set; }
        public dynamic info { get; set; }
    }
    public class Overlay
    {
        public const string TEXT_OVERLAY = "TEXT";
        public const string IMAGE_OVERLAY = "IMAGE";
        public const string GRID_OVERLAY = "GRID";
        public string type { get; set; }
        public dynamic content { get; set; }
    }
}