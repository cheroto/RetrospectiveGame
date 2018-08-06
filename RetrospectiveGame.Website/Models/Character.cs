using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetrospectiveGame.Website.Models
{
    public class Character
    {
        int Constitution { get; set; }
        int Life { get; set; }
        int Modifier { get; set; }
        string Name { get; set; }
        int Strength { get; set; }
    }
}