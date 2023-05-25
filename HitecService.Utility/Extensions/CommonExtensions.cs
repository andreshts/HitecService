using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.Data.Common;
using System.Dynamic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web;

namespace HitecService.Utility.Extensions;

public static class CommonExtensions
{
    #region DateTime

    public static int GetWeekOfYear(this DateTime source)
    {
        var time = source;

        var day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
        if (day is >= DayOfWeek.Monday and <= DayOfWeek.Wednesday)
            time = time.AddDays(3);

        return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
    }

    #endregion

    #region ..IP Check

    public static string? GetIpAddress(this IPAddress source)
    {
        var ipStr   = source.ToString();
        var splitIp = ipStr.Split(':');

        if (splitIp.Length > 1) return source.ToString();

        return splitIp.LastOrDefault();
    }

    #endregion

    private static T? ToJsonObject<T>(this string source)
        => JsonSerializer.Deserialize<T>(source);

    public static string ToJsonString(this object source)
        => JsonSerializer.Serialize(source);

    public static string ToJsonString(this object source, JsonSerializerOptions jsonSerializerSettings)
        => JsonSerializer.Serialize(source, jsonSerializerSettings);

    public static IEnumerable<TResult> ZipThree<T1, T2, T3, TResult>(
        this IEnumerable<T1>      source,
        IEnumerable<T2>           second,
        IEnumerable<T3>           third,
        Func<T1, T2, T3, TResult> func)
    {
        using var e1 = source.GetEnumerator();
        using var e2 = second.GetEnumerator();
        using var e3 = third.GetEnumerator();

        while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext())
            yield return func(e1.Current, e2.Current, e3.Current);
    }


    public static string StringUpperNormalize(this string text)
        => text.ToUpper().Replace("İ", "I").Replace("Ş", "S").Replace("Ö", "O").Replace("Ç", "C");

    public static string ToStringFormatted(this IReadOnlyDictionary<string, string> source)
        => string.Join("|", source.Select(ss => $"{ss.Key}={ss.Value}"));

    public static string? GetPropertyValue(this object value, string propertyName)
    {
        var     modelDictionary  = value.ToJsonString().ToJsonObject<IDictionary<string, string>>();
        var     propertyHasValue = false;
        string? propertyValue    = null;
        if (modelDictionary is not null && modelDictionary.ContainsKey(propertyName))
            propertyHasValue = modelDictionary.TryGetValue(propertyName, out propertyValue);

        return propertyHasValue ? propertyValue : null;
    }

    public static bool IsValidEmail(this string text)
    {
        var emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,10})+)$");
        var match      = emailRegex.Match(text);

        return match.Success;
    }
    public static string EncryptPass(string encrypt)
    {
        try
        {
            var result = BCrypt.Net.BCrypt.HashPassword(encrypt);

            return result;
        }
        catch (HashInformationException e)
        {
            Console.WriteLine(e.Message);

            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);

            throw;
        }
    }

    public static bool ValidatePass(string? hash, string password)
    {
        try
        {
            var result = BCrypt.Net.BCrypt.Verify(password, hash);

            return result;
        }
        catch (HashInformationException e)
        {
            Console.WriteLine(e.Message);

            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);

            throw;
        }
    }

    public static string GenerateJwToken(string issuer, string audience)
    {
        var rsa        = RSA.Create();
        var privateKey = rsa.ExportParameters(true);

        var credentials = new SigningCredentials(new RsaSecurityKey(privateKey), SecurityAlgorithms.RsaSha256);

        var token = new JwtSecurityToken(
            issuer,
            audience,
            signingCredentials: credentials);

        var handler     = new JwtSecurityTokenHandler();
        var tokenString = handler.WriteToken(token);

        return tokenString;
    }

    public async static Task<bool> ValidateJwToken(string tokenString, string issuer, string audience)
    {
        var rsa       = RSA.Create();
        var publicKey = rsa.ExportParameters(false);
        var handler   = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidIssuer      = issuer,
            ValidAudience    = audience,
            IssuerSigningKey = new RsaSecurityKey(publicKey)
        };

        var principal = await handler.ValidateTokenAsync(tokenString, validationParameters);

        return principal.IsValid;
    }


    #region ..Assigned..

    private static bool Assigned(this string? source)
        => !string.IsNullOrWhiteSpace(source ?? string.Empty);

    private static bool Assigned(this double source)
        => !double.IsNaN(source);

    private static bool Assigned(this DateTime source)
        => !DateTime.Equals(source, DateTime.MinValue);

    private static bool Assigned(this List<object>? list)
        => list is not null && list.Any();

    private static bool Assigned(this IEnumerable<object>? list)
        => list is not null && list.Any();

    public static bool Assigned(this object source)
    {
        switch (source)
        {
            case DateTime:
                return Convert.ToDateTime(source).Assigned();
            case string:
                return Convert.ToString(source).Assigned();
            case double:
                return Convert.ToDouble(source).Assigned();
            case List<object> list:
                return list.Assigned();
            case IEnumerable<object> objects:
                return objects.Assigned();
        }

        if (IsKeyValuePair(source)) return AssignedKeyValuePair(source);

        if (source is IDictionary { } dictionary) return Assigned(dictionary);

        return source != DBNull.Value;
    }

    private static bool Assigned(IDictionary? source)
        => source is { Count: > 0 };

    private static bool Assigned<T, TU>(this KeyValuePair<T, TU> pair)
        => !pair.Equals(new KeyValuePair<T, TU>()) && pair.Assigned();

    private static bool AssignedKeyValuePair(object source)
    {
        return source switch
        {
            KeyValuePair<object, object> pair => pair.Assigned(),
            KeyValuePair<object, string> pair => pair.Assigned(),
            KeyValuePair<string, object> pair => pair.Assigned(),
            KeyValuePair<string, string> pair => pair.Assigned(),
            _ => false
        };
    }

    private static bool IsKeyValuePair(object? source)
    {
        if (source is null) return false;
        var type = source.GetType();

        if (type.IsGenericType)
            return type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>);

        return false;
    }

    #endregion ..Assigned..

    #region ..NotAssigned..

    private static bool NotAssigned(this string? source)
        => string.IsNullOrWhiteSpace(source);

    private static bool NotAssigned(this double source)
        => double.IsNaN(source);

    private static bool NotAssigned(this DateTime source)
        => DateTime.Equals(source, DateTime.MinValue);

    private static bool NotAssigned(this List<object>? list)
        => list is null || !list.Any();

    private static bool NotAssigned(this IEnumerable<object>? list)
        => list is null || !list.Any();

    public static bool NotAssigned(this object source)
    {
        switch (source)
        {
            case DateTime:
                return Convert.ToDateTime(source).NotAssigned();
            case string:
                return Convert.ToString(source).NotAssigned();
            case double:
                return Convert.ToDouble(source).NotAssigned();
            case List<object> list:
                return list.NotAssigned();
            case IEnumerable<object> objects:
                return objects.NotAssigned();
        }

        if (IsKeyValuePair(source)) return NotAssignedKeyValuePair(source);

        if (source is IDictionary { } dictionary) return NotAssigned(dictionary);

        return source == DBNull.Value;
    }

    private static bool NotAssigned(IDictionary? source)
        => source is null || source.Count == 0;

    private static bool NotAssigned<T, TU>(this KeyValuePair<T, TU> pair)
        => pair.Equals(new KeyValuePair<T, TU>()) || pair.NotAssigned();

    private static bool NotAssignedKeyValuePair(object source)
    {
        return source switch
        {
            KeyValuePair<object, object> pair => pair.NotAssigned(),
            KeyValuePair<object, string> pair => pair.NotAssigned(),
            KeyValuePair<string, object> pair => pair.NotAssigned(),
            KeyValuePair<string, string> pair => pair.NotAssigned(),
            _ => true
        };
    }

    #endregion ..NotAssigned..

    #region ..TypeConverting..

    public static double ToDouble(this object source)
        => Convert.ToDouble(source);

    public static decimal ToDecimal(this object source)
        => Convert.ToDecimal(source);

    private static int ToInt32(this object source)
        => Convert.ToInt32(source);

    public static long ToInt64(this object source)
        => Convert.ToInt64(source);

    public static short ToInt16(this object source)
        => Convert.ToInt16(source);

    public static float ToFloat(this int source)
        => source;

    public static byte ToByte(this object source)
        => Convert.ToByte(source);

    public static bool ToBool(this object source)
    {
        bool result;
        try
        {
            result = Convert.ToBoolean(source);
        }
        catch (Exception)
        {
            result = Convert.ToBoolean(Convert.ToInt32(source));
        }

        return result;
    }

    public static List<int> ToInt32List(this List<object> source)
        => source.ConvertAll(x => x.ToInt32());

    public static object GetValue(this DbDataReader source, string name)
        => source.GetValue(source.GetOrdinal(name));

    public static string ToUrlEncode(this string source)
        => HttpUtility.UrlEncode(source);

    public static DateTime ToDateTime(this int source)
    {
        var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);

        return dateTime.AddSeconds(source);
    }

    public static DateTime ToDateTime(this string source)
        => DateTime.Parse(source);

    public static int ToUnixTimeSecond(this DateTime source)
        => ((DateTimeOffset)source).ToUnixTimeSeconds().ToInt32();

    public static string? ToEnumString<T>(this object source)
        => Enum.GetName(typeof(T), source);

    #endregion ..TypeConverting..

    #region ..Controls..

    public static bool IsNumeric(this object source)
        => int.TryParse(source.ToString(), out _);

    public static bool IsList(this object source)
        => source is List<object>;

    public static bool IsDictionary(this object source)
        => source is Dictionary<object, object>;

    public static bool IsPropertyExist(this object dynamics, string name)
    {
        if (dynamics is ExpandoObject)
            return ((IDictionary<string, object>)dynamics).ContainsKey(name);

        return dynamics.GetType().GetProperty(name) != null;
    }

    public static bool IsPropertyExists(dynamic dynamics, string name)
    {
        if (dynamics is ExpandoObject)
            return ((IDictionary<string, object>)dynamics).ContainsKey(name);

        return dynamics.GetType().GetProperty(name) != null;
    }

    #endregion

    #region ..Clear && Fix && Update..

    public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector)
        => enumerable.GroupBy(keySelector).Select(grp => grp.First());

    public static string Replace(this string source, ICollection<string> list, string replacement)
    {
        var d = source;
        foreach (var item in list)
            d = d.Replace(item, replacement);

        return d;
    }


    public static string ReplaceAt(this string source, string replace, int index, int length)
    {
        if (index < 0)
            return source;

        return source.Remove(index, Math.Min(length, source.Length - index))
            .Insert(index, replace);
    }

    public static string ToMd5(this string s)
    {
        var builder = new StringBuilder();

        foreach (var b in MD5.HashData(Encoding.UTF8.GetBytes(s)))
            builder.Append(b.ToString("x2").ToLower());

        return builder.ToString();
    }

    public static string ReplaceHtml(this string str, Dictionary<string, string> dict)
    {
        var sb = new StringBuilder(str);

        return sb.ReplaceHtml(dict).ToString();
    }

    private static StringBuilder ReplaceHtml(this StringBuilder sb, Dictionary<string, string> dict)
    {
        foreach (var replacement in dict)
            sb.Replace(replacement.Key, replacement.Value);

        return sb;
    }

    public static string GenerateVerificationCode(int number)
    {
        var checksum = (98 - number * 100 % 97) % 97;
        return $"{number}-{checksum}-000";
    }

    #endregion
}