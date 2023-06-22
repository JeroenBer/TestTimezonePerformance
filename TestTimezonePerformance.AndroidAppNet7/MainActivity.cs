using Android.OS;

namespace TestTimezonePerformance.AndroidAppNet7
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var btnStartTests = FindViewById<Android.Widget.Button>(Resource.Id.btnStartTests);
            btnStartTests.Click += BtnStartTests_Click;
        }

        private async void BtnStartTests_Click(object sender, EventArgs e)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            var lst = TimeZoneInfo.GetSystemTimeZones();

            sw.Stop();

            System.Diagnostics.Debug.WriteLine($"GetSystemTimeZones {lst.Count}");

            await ShowMessage($"Results", $"Performance {sw.Elapsed.TotalSeconds}.{sw.Elapsed.Milliseconds} sec");
        }

        private async Task ShowMessage(string title, string message)
        {
            var wait = new SemaphoreSlim(0);
            var dialog = new Android.App.AlertDialog.Builder(this);
            var alert = dialog.Create();
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetButton("OK", (c, ev) =>
            {
                wait.Release(1);
            });
            alert.Show();
            await wait.WaitAsync();
        }

    }
}