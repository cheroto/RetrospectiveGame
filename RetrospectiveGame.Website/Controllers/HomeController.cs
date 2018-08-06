using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetrospectiveGame.Logic;
using Autofac;

namespace RetrospectiveGame.Website.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            if (Request.HttpMethod == "POST")
            {
                return RedirectToAction("About");
            }
            else
            {
                return View();
            }
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult RetrospectiveGame()
        {

            InstantiateCharacters();

            return View();
        }

        public ActionResult BattleRound()
        {
            var char1 = Session["char1"] as Character;
            var char2 = Session["char2"] as Character;

            return
        }

        private void InstantiateCharacters()
        {

            var builder = new ContainerBuilder();
            builder.RegisterType<DiceRoller>().As<IDiceRoller>().SingleInstance();
            builder.RegisterType<Character>().As<ICharacter>();
            builder.RegisterType<CombatHandler>().As<ICombatHandler>();
            var container = builder.Build();


            using (var scope = container.BeginLifetimeScope())
            {
                Session["char1"] = scope.Resolve<ICharacter>(new NamedParameter("name", "LB"));
                Session["char2"] = scope.Resolve<ICharacter>(new NamedParameter("name", "Villain"));
                Session["combatHandler"] = scope.Resolve<ICombatHandler>();
            }
        }
    }
}