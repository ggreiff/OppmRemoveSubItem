using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OppmRemoveSubItem.Utility
{
    /// <summary>
    /// Common Used Extention Methods
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Count the number of words in a string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns></returns>
        public static int WordCount(this string input)
        {
            return input.Words().Length;
        }

        public static String[] Words(this String input)
        {
            return input.Split(new[] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static String Word(this String input, int index)
        {
            var words = input.Words();
            if (index < 0 || index >= words.Length) return String.Empty;
            return words[index];
        }

        /// <summary>
        /// Convert ArrayList to List.
        /// </summary>
        public static List<T> ToList<T>(this ArrayList arrayList)
        {
            var list = new List<T>(arrayList.Count);
            list.AddRange(arrayList.Cast<T>());
            return list;
        }

        static public string CleanString(this String input)
        {
            var sb = new StringBuilder();
            if (input.IsNotNullOrEmpty())
                foreach (var c in input.Where(c => !Char.IsControl(c))) sb.Append(c);
            return sb.ToString().Trim();
        }

        /// <summary>
        /// Determines whether [contains] [the specified source].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="toCheck">To check.</param>
        /// <param name="comp">The comp.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified source]; otherwise, <c>false</c>.
        /// </returns>
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            if (string.IsNullOrEmpty(toCheck) || string.IsNullOrEmpty(source))
                return false;

            return source.IndexOf(toCheck, comp) >= 0;
        }

        /// <summary>
        /// Converts a String to an Int32?
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns></returns>
        public static Int32? ToInt(this string input)
        {
            int val;
            if (int.TryParse(input, out val))
                return val;
            return null;
        }

        /// <summary>
        /// Converts a string to a DateTime?
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns></returns>
        public static DateTime? ToDate(this string input)
        {
            DateTime val;
            if (DateTime.TryParse(input, out val))
                return val;
            return null;
        }

        /// <summary>
        /// Converts a string to a Decimal?.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns></returns>
        public static Decimal? ToDecimal(this string input)
        {
            decimal val;
            if (decimal.TryParse(input, out val))
                return val;
            return null;
        }

        /// <summary>
        /// Converts a string to a Single?.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns></returns>
        public static Single? ToSingle(this string input)
        {
            Single val;
            if (Single.TryParse(input, out val))
                return val;
            return null;
        }

        /// <summary>
        /// Converts a string to a Double?.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns></returns>
        public static Double? ToDouble(this string input)
        {
            Double val;
            if (Double.TryParse(input, out val))
                return val;
            return null;
        }

        /// <summary>
        /// Almosts the equals.
        /// </summary>
        /// <param name="double1">The double1.</param>
        /// <param name="double2">The double2.</param>
        /// <param name="precision">The precision.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool AlmostEquals(this Double double1, Double double2, Double precision)
        {
            return (Math.Abs(double1 - double2) <= precision);
        }


        /// <summary>
        /// Almosts the equals.
        /// </summary>
        /// <param name="single1">The single1.</param>
        /// <param name="single2">The single2.</param>
        /// <param name="precision">The precision.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool AlmostEquals(this Single single1, Single single2, Single precision)
        {
            return (Math.Abs(single1 - single2) <= precision);
        }

        /// <summary>
        /// Almosts the zero.
        /// </summary>
        /// <param name="double1">The double1.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool AlmostZero(this Double double1)
        {
            return (Math.Abs(double1 - 0.0) <= .00000001);
        }

        /// <summary>
        /// Almosts the zero.
        /// </summary>
        /// <param name="single1">The single1.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool AlmostZero(this Single single1)
        {
            return (Math.Abs(single1 - 0.0f) <= .00000001);
        }

        public static Boolean IsTrimEqualTo(this String input , String compareTo, Boolean ignoreCase)
        {
            return String.Compare(input.Trim(), compareTo.Trim(), ignoreCase) == 0;
        }

        /// <summary>
        /// Determines whether the compareTo string [is equal to] [the specified input string].
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="compareTo">The string to compare to.</param>
        /// <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        /// <returns>
        ///   <c>true</c> if [is equal to] [the specified string]; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsEqualTo(this String input, String compareTo, Boolean ignoreCase)
        {
            return String.Compare(input, compareTo, ignoreCase) == 0;
        }

        /// <summary>
        /// Determines whether [is not equal to] [the specified input].
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="compareTo">The compare to.</param>
        /// <param name="ignoreCase">The ignore case.</param>
        public static Boolean IsNotEqualTo(this String input, String compareTo, Boolean ignoreCase)
        {
            return String.Compare(input, compareTo, ignoreCase) != 0;
        }




        /// <summary>
        /// Determines whether the specified input string is empty.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>
        ///   <c>true</c> if the specified input is empty; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsEmpty(this String input)
        {
            return (input != null && input.Length == 0);
        }

        /// <summary>
        /// Determines whether the specified input is null.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>
        ///   <c>true</c> if the specified input string is null; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsNull(this String input)
        {
            return (input == null);
        }

        /// <summary>
        /// Determines whether the input string [is null or empty].
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>
        ///   <c>true</c> if [is null or empty] [the specified input]; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsNullOrEmpty(this String input)
        {
            return String.IsNullOrEmpty(input);
        }

        /// <summary>
        /// Determines whether the input string [is not null or empty].
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>
        ///   <c>true</c> if [is not null or empty] [the specified input string]; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsNotNullOrEmpty(this String input)
        {
            return !String.IsNullOrEmpty(input);
        }

        /// <summary>
        /// Gets a CLS complaint string based on the input string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns></returns>
        public static String GetClsString(this String input)
        {
            return Regex.Replace(input.Trim(), @"[\W]", @"_");
        }

        /// <summary>
        /// Gets the name of the month.
        /// </summary>
        /// <param name="monthNumber">The month number.</param>
        /// <returns></returns>
        public static String GetMonthName(Int32 monthNumber)
        {
            return GetMonthName(monthNumber, false);
        }

        /// <summary>
        /// Gets the name of the month.
        /// </summary>
        /// <param name="monthNumber">The month number.</param>
        /// <param name="abbreviateMonth">if set to <c>true</c> [abbreviate month name].</param>
        /// <returns></returns>
        public static String GetMonthName(Int32 monthNumber, Boolean abbreviateMonth)
        {
            if (monthNumber < 1 || monthNumber > 12)
                throw new ArgumentOutOfRangeException("monthNumber");
            var date = new DateTime(1, monthNumber, 1);
            return abbreviateMonth ? date.ToString("MMM") : date.ToString("MMMM");
        }

        /// <summary>
        /// Get the Rows in a list for a the table.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        public static List<DataRow> RowList(this DataTable table)
        {
            return table.Rows.Cast<DataRow>().ToList();
        }

        /// <summary>
        /// Gets the field item frp a row.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        public static object GetItem(this DataRow row, String field)
        {
            return !row.Table.Columns.Contains(field) ? null : row[field];
        }

        /// <summary>
        /// Filters the rows to a list.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="ids">The ids.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        public static List<DataRow> FilterRowsToList(this DataTable table, List<Int32> ids, String fieldName)
        {
            Func<DataRow, bool> filter = row => ids.Contains((Int32)row.GetItem(fieldName));
            return table.RowList().Where(filter).ToList();
        }

        /// <summary>
        /// Filters the rows to a list.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="ids">The ids.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        public static List<DataRow> FilterRowsToList(this DataTable table, List<String> ids, String fieldName)
        {
            Func<DataRow, bool> filter = row => ids.Contains((String)row.GetItem(fieldName));
            return table.RowList().Where(filter).ToList();
        }

        /// <summary>
        /// Filters the rows to a data table.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="ids">The ids.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        public static DataTable FilterRowsToDataTable(this DataTable table, List<Int32> ids, String fieldName)
        {
            DataTable filteredTable = table.Clone();
            List<DataRow> matchingRows = FilterRowsToList(table, ids, fieldName);

            foreach (DataRow filteredRow in matchingRows)
            {
                filteredTable.ImportRow(filteredRow);
            }
            return filteredTable;
        }

        /// <summary>
        /// Filters the rows to a data table.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="ids">The ids.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        public static DataTable FilterRowsToDataTable(this DataTable table, List<String> ids, String fieldName)
        {
            DataTable filteredTable = table.Clone();
            List<DataRow> matchingRows = FilterRowsToList(table, ids, fieldName);

            foreach (DataRow filteredRow in matchingRows)
            {
                filteredTable.ImportRow(filteredRow);
            }
            return filteredTable;
        }

        /// <summary>
        /// Selects the into table the rows base on the filter.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static DataTable SelectIntoTable(this DataTable table, String filter)
        {
            DataTable selectResults = table.Clone();
            DataRow[] rows = table.Select(filter);
            foreach (DataRow row in rows)
            {
                selectResults.ImportRow(row);
            }
            return selectResults;
        }

        /// <summary>
        /// Deletes rows from the table base on the filter.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static DataTable Delete(this DataTable table, string filter)
        {
            table.Select(filter).Delete();
            return table;
        }

        /// <summary>
        /// Deletes the specified rows.
        /// </summary>
        /// <param name="rows">The rows.</param>
        public static void Delete(this IEnumerable<DataRow> rows)
        {
            foreach (DataRow row in rows)
                row.Delete();
        }

        /// <summary>
        /// Gets the or add value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dict">The dict.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static TValue GetOrAddValue<TKey, TValue>(
            this IDictionary<TKey, TValue> dict, TKey key)
                where TValue : new()
        {
            TValue value;
            if (dict.TryGetValue(key, out value))
                return value;
            value = new TValue();
            dict.Add(key, value);
            return value;
        }

        /// <summary>
        /// Gets the or add value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dict">The dict.</param>
        /// <param name="key">The key.</param>
        /// <param name="generator">The generator.</param>
        /// <returns></returns>
        public static TValue GetOrAddValue<TKey, TValue>(
            this IDictionary<TKey, TValue> dict, TKey key, Func<TValue> generator)
        {
            TValue value;
            if (dict.TryGetValue(key, out value))
                return value;
            value = generator();
            dict.Add(key, value);
            return value;
        }

        /// <summary>
        /// Determines whether the specified enumerable has items.
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns>
        ///   <c>true</c> if the specified enumerable has items; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean HasItems(this IEnumerable enumerable)
        {
            if (enumerable == null) return false;
            try
            {
                var enumerator = enumerable.GetEnumerator();
                if (enumerator.MoveNext()) return true;
            }
            catch
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified enumerable is empty.
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns>
        ///   <c>true</c> if the specified enumerable is empty; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsEmpty(this IEnumerable enumerable)
        {
            return !enumerable.HasItems();
        }

        /// <summary>
        /// Removes the invalid chars.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static String RemoveInvalidChars(this String fileName)
        {
            var regex = String.Format("[{0}]", Regex.Escape(new String(Path.GetInvalidFileNameChars())));
            var removeInvalidChars = new Regex(regex,
                                               RegexOptions.Singleline | RegexOptions.Compiled |
                                               RegexOptions.CultureInvariant);
            return removeInvalidChars.Replace(fileName, "");
        }

        /// <summary>
        /// Properties the set on a given object.
        /// </summary>
        /// <param name="p">The object to set the property on.</param>
        /// <param name="propName">Name of the prop.</param>
        /// <param name="value">The value.</param>
        public static void PropertySet(this Object p, String propName, Object value)
        {
            var t = p.GetType();
            var info = t.GetProperty(propName);
            if (info == null) return;
            if (!info.CanWrite) return;
            info.SetValue(p, value, null);
        }

        /// <summary>
        /// Determines whether the specified item is between.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns>
        ///   <c>true</c> if the specified item is between; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsBetween<T>(this T item, T start, T end)
        {
            return Comparer<T>.Default.Compare(item, start) >= 0
                && Comparer<T>.Default.Compare(item, end) <= 0;
        }
    }
}