using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NotAClue.ComponentModel.DataAnnotations;

namespace NotAClue.Web.DynamicData
{
    public static class Constants
    {
        public const CommandButtons FormViewButtons = CommandButtons.Delete | CommandButtons.Details | CommandButtons.Edit | CommandButtons.Insert;
        public const CommandButtons ListViewButtons = CommandButtons.Delete | CommandButtons.Edit | CommandButtons.Insert;
    }
}
