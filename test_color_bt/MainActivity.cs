using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using Skydoves.ColorPickerViewLib;
using Skydoves.ColorPickerViewLib.Listeners;
using Android.Bluetooth;
using Android.Widget;
using System.IO;
using Java.Util;
using System.Threading.Tasks;
using System.Linq;

namespace test_color_bt
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Java.Lang.String dataToSend;
        private BluetoothAdapter mBluetoothAdapter = null;
        private BluetoothSocket btSocket = null;
        private Stream outStream = null;
        // don't forget change addres to your device:
        private static string address = "00:20:12:08:8E:A0";
        // MY_UUID can be saved as is
        private static UUID MY_UUID = UUID.FromString("00001101-0000-1000-8000-00805F9B34FB");
        private Stream inStream = null;
        ColorPickerView color_wheel;
        Switch bltSwitch;
        SeekBar bright;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;
             color_wheel = FindViewById<ColorPickerView>(Resource.Id.colorPickerView1);
            bltSwitch = FindViewById<Switch>(Resource.Id.switch2);
            bright = FindViewById<SeekBar>(Resource.Id.seekBar1);
            bltSwitch.CheckedChange += bltSwitch_HandleCheckedChange;
            CheckBt();

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }
        private void CheckBt()
        {
            mBluetoothAdapter = BluetoothAdapter.DefaultAdapter;

            if (!mBluetoothAdapter.Enable())
            {
                Toast.MakeText(this, "Bluetooth Off",
                    ToastLength.Short).Show();
            }

            if (mBluetoothAdapter == null)
            {
                Toast.MakeText(this,
                    "Bluetooth does not exist or is busy", ToastLength.Short)
                    .Show();
            }
        }


        public void Connect()
        {
            BluetoothDevice device = mBluetoothAdapter.GetRemoteDevice(address);
            System.Console.WriteLine("Connection in progress" + device);
            mBluetoothAdapter.CancelDiscovery();
            try
            {
                btSocket = device.CreateRfcommSocketToServiceRecord(MY_UUID);
                btSocket.Connect();
                System.Console.WriteLine("Correct Connection");
                //status.Text = "Correct Connection to bluetooth";
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                try
                {
                    btSocket.Close();
                    System.Console.WriteLine("Connection closed");
                }
                catch (System.Exception)
                {
                    System.Console.WriteLine("Impossible to connect");
                 //   status.Text = "Impossible to connect";
                }
                System.Console.WriteLine("Socket Created");

            }
            beginListenForData();

        }

        public void beginListenForData()
        {
            try
            {
                inStream = btSocket.InputStream;
            }
            catch (System.IO.IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Task.Factory.StartNew(() => {
                byte[] buffer = new byte[1024];
                int bytes;

                while (true)
                {

                    try
                    {
                        bytes = inStream.Read(buffer, 0, 1024);
                        System.Console.WriteLine("bytes " + bytes.ToString());
                        if (bytes > 0)
                        {

                            RunOnUiThread(() => {
                                string valor = System.Text.Encoding.ASCII.GetString(buffer).Replace("5", String.Empty);
                                // transform string for deleate all symbols except 1-4(command from canny).
                                string command = new string(valor.Where(char.IsDigit).ToArray());

                             
                                  
                            });
                        }
                    }
                    catch (Java.IO.IOException)
                    {
                        RunOnUiThread(() => {
                            //EndSensor.Text = "End sensor status - undefined";
                            //rSwitch.Text = "Reed switch status - undefined ";
                        });
                        break;
                    }
                }
            });
        }

        void bltSwitch_HandleCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (e.IsChecked)
            {
                Connect();
            }
            else
            {
                //status.Text = "bluetooth not connected";
                if (btSocket.IsConnected)
                {
                    try
                    {
                        btSocket.Close();
                        System.Console.WriteLine("Connection closed");
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
        string str;
        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            str = "";
            
                View view = (View)sender;
                int[] RGB = new int[4];
                RGB = color_wheel.ColorEnvelope.GetArgb();

            str = RGB[1].ToString()+" " + RGB[2].ToString() + " " + RGB[3].ToString() + " " + bright.Progress;
                Snackbar.Make(view, str, Snackbar.LengthLong)
                   .SetAction("Action", (View.IOnClickListener)null).Show();
            if (bltSwitch.Checked)
            {
                try
                {
                    dataToSend = new Java.Lang.String(str);
                    writeData(dataToSend);
                    System.Console.WriteLine("Send signal to siren");
                }
                catch (System.Exception execept)
                {
                    System.Console.WriteLine("Error when send data" + execept.Message);
                }

            }
           // else status.Text = "bluetooth not connected";
            //OutputStreamAdapter outStream;
            // mSocket = mbluetoothAdapter.GetRemoteDevice(mbluetoothAdapter.Address.ToString());
            // OutStream = mSocket.OutputStream;

        }

        private void writeData(Java.Lang.String data)
        {
            try
            {
                outStream = btSocket.OutputStream;
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Error with OutputStream when write to Serial port" + e.Message);
            }

            Java.Lang.String message = data;

            byte[] msgBuffer = message.GetBytes();

            try
            {
                outStream.Write(msgBuffer, 0, msgBuffer.Length);
                System.Console.WriteLine("Message sent");
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Error with  when write message to Serial port" + e.Message);
               // status.Text = "Error with  when write message to Serial port";
            }
        }




        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}
