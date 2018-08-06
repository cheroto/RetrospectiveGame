using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetrospectiveGame.Logic;
using Autofac;
using System.Text;

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

            return View();
        }

        public ActionResult RetrospectiveGameAction()
        {
            Session["roundResults"] = null;

            InstantiateCharacters();

            return View();
        }

        public ActionResult BattleRound()
        {
            var char1 = Session["char1"] as ICharacter;
            var char2 = Session["char2"] as ICharacter;
            var combatHandler = Session["combatHandler"] as CombatHandler;

            var roundResults = BattleRound(char1, char2, combatHandler);
            Session["roundResults"] = roundResults;

            if (Request.IsAjaxRequest())
            {
                IEnumerable<ICharacter> charList = new List<ICharacter>()
                {
                    char1,
                    char2
                };
                return PartialView("_BattleCanvas", charList);
            }
            return View("RetrospectiveGameAction");
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
                Session["char1"] = scope.Resolve<ICharacter>(new NamedParameter("name", "Lighting Bolts"));
                Session["char2"] = scope.Resolve<ICharacter>(new NamedParameter("name", "Villain"));
                Session["combatHandler"] = scope.Resolve<ICombatHandler>();
            }
        }

        private static void DoBattle(ICharacter char1, ICharacter char2, ICombatHandler combatHandler, int roundNumber)
        {
            while (combatHandler.AreCharactersAlive(char1, char2))
            {
                roundNumber += 1;
                BattleRound(char1, char2, combatHandler);
            }
            if (char1.Life < 1) PlayHeroDeathRattle(char1);
            else PlayVillainDeathRattle(char2);
        }

        private static string BattleRound(ICharacter char1, ICharacter char2, ICombatHandler combatHandler)
        {
            var attackResultsChar1 = AttackSequence(char1, char2, combatHandler);
            var attackResultsChar2 = AttackSequence(char2, char1, combatHandler);

            return attackResultsChar1 + attackResultsChar2;
        }

        private static string AttackSequence(ICharacter attacker, ICharacter defender, ICombatHandler combatHandler)
        {
            var damageTaken = combatHandler.GetAttackRoundDamage(attacker, defender);
            defender.TakeDamage(damageTaken);

            var sb = new StringBuilder();
            sb.Append(string.Format("{0} attacks!", attacker.Name));
            sb.Append("\n");
            sb.Append(combatHandler.LastAttackStatus);
            if (damageTaken > 0) sb.AppendLine(string.Format("{0} has lost {1} life points.", defender.Name, damageTaken));
            sb.Append("\n");
            return sb.ToString();
        }

        private static void PlayVillainDeathRattle(ICharacter character)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            System.Threading.Thread.Sleep(500);
            Console.WriteLine(string.Format("{0} constantly parries sword attacks but exhaustion is clearly taking it's toll.", character.Name));
            Console.WriteLine("With sluggish responses, he is finally unable to parry a strike aiming for the neck.");
            Console.WriteLine();
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine(string.Format("{0}'s body stumbles to the ground, head rolling on the opposite direction. " +
                "After a few last twiches, he is gone.", character.Name));
            System.Threading.Thread.Sleep(3000);
        }

        private static void PlayHeroDeathRattle(ICharacter character)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            System.Threading.Thread.Sleep(500);
            Console.WriteLine(string.Format("{0} valiantly fights despite numerous broken bones and cuts throughout his body.", character.Name));
            Console.WriteLine("A moment of distraction, however, leads to a sword through the heart.");
            Console.WriteLine();
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine(string.Format("{0} stumbles to the ground, coughs blood one last time and dies.", character.Name));
            System.Threading.Thread.Sleep(3000);
        }
    }
}