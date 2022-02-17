using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class ConsoleUtils
    {
        /// <summary>
        /// Reads string from console
        /// </summary>
        /// <param name="question">question to answer</param>
        /// <param name="minLength">minLength</param>
        /// <param name="maxLength">maxLength</param>
        /// <param name="fieldName">Field Name to generate messages based on it</param>
        /// <param name="invalidLengthMessage">Meesage to show if string is invalid</param>
        /// <returns>resulting string</returns>
        public static string ReadString(string? question = null, int minLength = 0, int maxLength = 100,
            string? fieldName = null, string? invalidLengthMessage = null)
        {
            if (fieldName is null)
                fieldName = "Field";
            if (question is null)
                question = $"Please, enter {fieldName}";
            if (invalidLengthMessage is null)
                invalidLengthMessage = $"{fieldName} must be not less than {minLength} and not more than {maxLength}";

            Console.WriteLine(question);

            string? answer = null;
            for (; ; )
            //while(true)
            {
                answer = Console.ReadLine();
                if (answer == null || answer.Length < minLength || answer.Length > maxLength)
                    Console.WriteLine(invalidLengthMessage);
                else
                    break;
            }

            return answer;
        }

        public static int ReadInt(string? question = null, int? minValue = null, 
            int? maxValue = null, string? fieldName = null, 
            string? invalidStringMessage = null, string? invalidNumberMessage = null)
        {
            if (fieldName is null)
                fieldName = "Field";
            if (question is null)
                question = $"Please, enter {fieldName}";
            if (invalidStringMessage is null)
                invalidStringMessage = $"{fieldName} must of type integer";
            if (invalidNumberMessage is null)
            {
                var minMsg = minValue.HasValue ? $"must be not less than {minValue}" : String.Empty;
                var maxMsg = maxValue.HasValue ? $"not more than {maxValue}" : String.Empty;
                var msgArr = new List<string>(new string[] { minMsg, maxMsg }).Where(x => !String.IsNullOrEmpty(x));
                var minMaxMsg = String.Join(" and ", msgArr);

                invalidNumberMessage = $"{fieldName} {minMaxMsg}";
            }

            Console.WriteLine(question);

            while(true)
            {
                var strValue = Console.ReadLine();
                int iVal;
                if (!Int32.TryParse(strValue, out iVal))
                {
                    Console.WriteLine(invalidStringMessage);
                    continue;
                }

                if ((minValue.HasValue && iVal < minValue) || (maxValue.HasValue && iVal > maxValue))
                {
                    Console.WriteLine(invalidNumberMessage);
                    continue;
                }

                return iVal;
            }           
        }
    }
}
