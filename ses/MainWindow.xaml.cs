using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;

namespace ses
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static readonly string ApplicationName = "Ejemplo";
        static readonly string SpreadsheetId = "17oxmBQ9LgsKpKb7CdiOmarm57NNrHEfFTHnCRKbmLIg";
        static readonly string sheet = "Ejemplo";
        static SheetsService service;
        public MainWindow()
        {
            InitializeComponent();
            GoogleCredential credential;
            using (var stream = new FileStream("siasoft.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }

            service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });


            //ReadEntries();
            //CreateEntry();
            //UpdateEntry();
            //DeleteEntry();
        }
        //eliminar
        static void DeleteEntry()
        {
            var range = $"{sheet}";
            var requestBody = new ClearValuesRequest();

            var deleteRequest = service.Spreadsheets.Values.Clear(requestBody, SpreadsheetId, range);
            var deleteReponse = deleteRequest.Execute();
        }

        //update
        public void UpdateEntry(string cambiar,string posicion)
        {
            var range = $"{sheet}!Q"+posicion+":Q"+posicion+"";
            var valueRange = new ValueRange();

            var oblist = new List<object>() {cambiar};
            valueRange.Values = new List<IList<object>> { oblist };

            var updateRequest = service.Spreadsheets.Values.Update(valueRange, SpreadsheetId, range);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            var appendReponse = updateRequest.Execute();

            MessageBox.Show("actualizacion exitosa");
        }

        //insertar
        static void CreateEntry()
        {
            int i = 0;
            do
            {

                var range = $"{sheet}!A:F";
                var valueRange = new ValueRange();

                var oblist = new List<object>() { i, (i + 1), (i + 2), (i + 3), (i + 4), (i + 5) };
                valueRange.Values = new List<IList<object>> { oblist };

                var appendRequest = service.Spreadsheets.Values.Append(valueRange, SpreadsheetId, range);
                appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
                var appendReponse = appendRequest.Execute();
                i++;
            } while (i < 10);
        }

        //leer
        public void ReadEntries(string cadena)
        {
            var range = $"{sheet}!N1:N";
            SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(SpreadsheetId, range);
            var response = request.Execute();
            IList<IList<object>> values = response.Values;
            List<object> valores = new List<object>(values);

            if (values != null && values.Count > 0)
            {
                int a = 1;
                foreach (var row in values)
                {
                    if (row[0].ToString() == cadena)
                    {
                        UpdateEntry("Cambio", a.ToString());
                        row[4] = "siiiisas";
                    }

                    //Console.WriteLine("{0} ", row[0]);
                    a = a + 1;
                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }
            Console.Read();
        }




        private void Crear_Click(object sender, RoutedEventArgs e)
        {
            ReadEntries(BOxBusc.Text);
        }






    }
}
