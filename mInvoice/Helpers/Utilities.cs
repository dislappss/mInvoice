using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mInvoice.Helpers
{
    public class Utilities
    {
        public static string getSearchedStringFromStringBetween2Target(string target, String searched)
        {
            string _ret_value = null;

            try
            {
                List<int> _arr = new List<int>();

                _arr = FindAllStrings(target, searched);

                if (_arr.Count == 2)
                {
                    int _start_ind = _arr[0] + target.Length;

                    _ret_value = searched.Substring(_start_ind, _arr[1] - _start_ind);

                    return _ret_value;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        static List<int> FindAllStrings(string target, String searched)
        {
            List<int> _ret_arr = new List<int>();

            Console.Write(
                "The character '{0}' occurs at position(s): ",
                target);

            int startIndex = -1;
            int hitCount = 0;

            // Search for all occurrences of the target.
            while (true)
            {
                startIndex = searched.IndexOf(
                    target, startIndex + 1,
                    searched.Length - startIndex - 1);

                // Exit the loop if the target is not found.
                if (startIndex < 0)
                    break;

                Console.Write("{0}, ", startIndex);

                _ret_arr.Add(startIndex);

                hitCount++;
            }

            Console.WriteLine("occurrences: {0}", hitCount);

            return _ret_arr;
        }
    }
}