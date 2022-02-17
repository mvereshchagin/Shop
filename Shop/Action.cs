using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    public enum Action : UInt16
    {
        [Description("Print reviews")]
        ListReviews = 1,

        [Description("List products")]
        ListProducts = 2,

        AddProduct = 3,
        AddReview = 4,
        Register = 5,
        Authorize = 6,
        Exit = 7,
    }

    internal static class ActionUtils
    {
        public static string PrintMessage()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var value in Enum.GetValues(typeof(Action)))
            {
                var msg = $"{GetName((Action)value)} - ({(UInt16)value})";
                sb.AppendLine(msg);
            }
            return sb.ToString();
        }

        public static string GetName(Action action)
        {
            switch(action)
            {
                case Action.ListReviews:
                    return "Print reviews";
                case Action.ListProducts:
                    return "Print prodcuts list";
                default:
                    return Enum.GetName(action);
            }
        }

        public static Shop.Action ReadAction(string invalidNumberMessage)
        {
            while (true)
            {
                var strNumber = Console.ReadLine();
                try
                {
                    ushort iNumber = UInt16.Parse(strNumber);
                    if (Enum.IsDefined(typeof(Action), iNumber))
                        return (Action)iNumber;
                }
                catch { }

                Console.WriteLine(invalidNumberMessage);
            }
        }
    }
}
