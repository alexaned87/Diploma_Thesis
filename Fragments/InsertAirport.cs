
using System.Collections.Generic;
using Android.OS;
using Android.Views;
using Android.Widget;

using System.Net.Http;
using Newtonsoft.Json;
using Droid.Demo.Models;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Droid.Demo.Fragments
{
    public class InsertAirport : Android.Support.V4.App.Fragment
    {
        EditText textName;
        EditText textLat;
        EditText textLon;





      
        private Button buttonConfirm;
      

        public async Task<string> TopEntriesAsync1(Entry arpt)
        {
            var client = new HttpClient();
            string url = "http://192.168.1.3//api/AirportAPI/InsertAirport";
            var json = JsonConvert.SerializeObject(arpt);
            var content = new StringContent (json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content);
            Console.Out.WriteLine(response);
            return JsonConvert.SerializeObject(arpt);

        }


        public async Task<string> TopEntriesAsync2(Entry airport)
        {
            var client = new HttpClient();
            string url = "http://192.168.1.3//api/AirportAPI/UpdateAirport";
            var json = JsonConvert.SerializeObject(airport);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content);
            Console.Out.WriteLine(response);
            return JsonConvert.SerializeObject(airport);

        }

        public async Task<List<Entry>> TopEntriesAsync()
        {
            var client = new HttpClient();

            var result = await client.GetStringAsync("http://192.168.1.3//api/AirportAPI/GetAllAirport");
            Console.Out.WriteLine(result);
            return JsonConvert.DeserializeObject<List<Entry>>(result);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {




            var view = inflater.Inflate(Resource.Layout.Insert, container, false);
            textName = view.FindViewById<EditText>(Resource.Id.txtName);
      
            textLat = view.FindViewById<EditText>(Resource.Id.txtLat);
            textLon = view.FindViewById<EditText>(Resource.Id.txtLon);
            buttonConfirm = view.FindViewById<Button>(Resource.Id.btnConfirm);

           


            buttonConfirm.Click += async (sender, e) =>
            {
                var entry = new Entry
                {
                    name = textName.Text,
                    lat = textLat.Text,
                    lon = textLon.Text
                   
                };

                IEnumerable<Entry> airport = await TopEntriesAsync();
                List<string> list = airport.Where(x => x.name == textName.Text).Select(x => x.name).ToList();
                int count = list.Count;
                if (list.Count == 0)
                {

                    var properties = await TopEntriesAsync1(entry);
                    Toast.MakeText(Activity, "Airport: " + textName.Text + " created successfully!", ToastLength.Long).Show();
                }
                if (count == 1)
                {
                    list = airport.Where(x => x.name == textName.Text).Select(x => x.Id).ToList();
                    foreach (string element in list)
                    {
                        var point = new Entry
                        {
                            Id = element
                        };

                        point.name = textName.Text;
                        point.lat = textLat.Text;
                        point.lon = textLon.Text;
                        var properties = await TopEntriesAsync2(point);
                    };
                    
                    Toast.MakeText(Activity, "Airport: " + textName.Text + " updated successfully!", ToastLength.Long).Show();

                }
            };
            return view;

        }


               
            };


               
        }


       
    




      
    
