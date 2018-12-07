using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace sos
{
    class Program
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json
        static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static string ApplicationName = "Google Sheets API .NET Quickstart";

        static void Main(string[] args)
        {

            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define request parameters.
            String spreadsheetId = "1rRb5S7oA6tgrC6w2a8bAd1z4dIgb_isBW6gdjHTWOjk";
            String range = "Fichero 08-NOV-2018!A:C";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);
            // Prints the names and majors of students in a sample spreadsheet:
            // https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit
            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                Console.WriteLine("Name, Gender, Class, State, Major, Activity ");
                foreach (var row in values)
                {
                    // Print columns A and E, which correspond to indices 0 and 4.
                    Console.WriteLine("{0}, {1}, {2}", row[0], row[1], row[2]);
                    Console.WriteLine("_______________________________________________________________________________________________________________________");
                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }
            Console.Read();
        }
        //private const string SpreadsheetId = "1VgqOsA-GnYAzIldTPVGgqm-Mr4G90xallzp2gLw4ZzY";
        //private const string Sheet = "ciudad";
        //private static void CreateEntry()
        //{
        //    var range = $"{Sheet}!A2:F2";
        //    var valueRange = new ValueRange();

        //    var oblist = new List<object>() { "Hello!", "This", "was", "insertd", "via", "C#" };
        //    valueRange.Values = new List<IList<object>> { oblist };

        //    var appendRequest = _service.Spreadsheets.Values.Append(valueRange, SpreadsheetId, range);
        //    appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
        //    appendRequest.Execute();
        //}
    }
}