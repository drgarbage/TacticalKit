using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using TacticalKit.Models;

namespace TacticalKit.Controllers
{
    public class BattlefieldController : Controller
    {
        private static IDictionary<string, GammerState> GammerStates =
            new Dictionary<string, GammerState>();
        private static IDictionary<string, GameState> GameStates =
            new Dictionary<string, GameState>();
        private static IList<Overlay> Overlays =
            new List<Overlay>();

        public ActionResult sync(StatePackage package)
        {
            foreach (GammerState gammer in package.gammers)
            {
                sync(gammer);
            }

            foreach (GameState info in package.infos)
            {
                sync(info);
            }

            IList<GammerState> gammers = GammerStates
                .Where(s => s.Value.group.StartsWith(package.group))
                .Select(g => g.Value)
                .ToList();

            IList<GameState> infos = GameStates
                .Where(s => s.Value.group.StartsWith(package.group))
                .Select(g => g.Value)
                .ToList();

            return Result(new StatePackage(){
                group = package.group,
                infos = infos,
                gammers = gammers
            });
        }

        public ActionResult update(IList<GameState> infos)
        {
            return Result(GameStates);
        }

        public ActionResult reset()
        {
            GammerStates.Clear();
            GameStates.Clear();
            return Result(GammerStates.Select(g => g.Value));
        }

        //
        // internal service
        //

        private ActionResult Result(object result)
        {
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private void sync(GammerState state)
        {
            if (!GammerStates.ContainsKey(state.token))
                GammerStates[state.token] = state;
            GammerStates[state.token].sync(state);
        }

        private void sync(GameState info)
        {
            if (!GameStates.ContainsKey(info.identifier))
                GameStates[info.identifier] = info;
            GameStates[info.identifier].sync(info);
        }
    }
}
