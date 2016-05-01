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
    public class GameController : Controller
    {
        private static IDictionary<string, GammerState> GammerStates = 
            new Dictionary<string, GammerState>();

        private ActionResult Result(object result)
        {
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult sync(GammerState state)
        {
            if (state == null || string.IsNullOrEmpty(state.token))
                return Result(GammerStates.Select(g=>g.Value));
            if (!GammerStates.ContainsKey(state.token))
                GammerStates[state.token] = state;
            GammerStates[state.token].sync(state);
            return Result(GammerStates.Where(s => state.group.StartsWith(s.Value.group)).Select(g => g.Value));
        }

        public ActionResult reset()
        {
            GammerStates.Clear();
            return Result(GammerStates.Select(g => g.Value));
        }
    }
}
