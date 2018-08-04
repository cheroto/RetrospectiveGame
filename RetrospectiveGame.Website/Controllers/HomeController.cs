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

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult RetrospectiveGame()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DiceRoller>().As<IDiceRoller>().SingleInstance();
            builder.RegisterType<Character>().As<ICharacter>();
            builder.RegisterType<CombatHandler>().As<ICombatHandler>();
            var container = builder.Build();

            ICharacter char1;
            ICharacter char2;
            ICombatHandler combatHandler;

            using (var scope = container.BeginLifetimeScope())
            {
                char1 = scope.Resolve<ICharacter>(new NamedParameter("name", "LB"));
                char2 = scope.Resolve<ICharacter>(new NamedParameter("name", "Villain"));
                combatHandler = scope.Resolve<ICombatHandler>();
            }
            return View(char1);
        }
    }
}