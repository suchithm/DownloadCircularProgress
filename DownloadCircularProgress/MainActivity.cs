using Android.App;
using Android.Widget;
using Android.OS; 
using DownloadCircularProgress.Shared;
using System;
using System.Threading.Tasks;
using Android.Graphics;

namespace DownloadCircularProgress
{//https://github.com/xamarin/recipes/blob/master/cross-platform/networking/download_progress
	[Activity (Label = "DownloadCircularProgress", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{ 
		ProgressBar _progressBar;
		InitCircularProgressClass initCircularProgressBar;  

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
 
			SetContentView (Resource.Layout.Main);
 
			Button button = FindViewById<Button> (Resource.Id.myButton);
			_progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
			fnInitializeCircleBar();
			button.Click += StartDownloadHandler;
 
		}
		async void StartDownloadHandler(object sender, System.EventArgs e)
		{ 
			//initCircularProgressBar.setProgress(secondUpdate); 
			_progressBar.Progress = 0;
			var progressReporter = new Progress<DownloadProgressClass>(); 
			progressReporter.ProgressChanged += (s, args) => initCircularProgressBar.setProgress((int)(100 * args.PercentComplete));
			progressReporter.ProgressChanged += (s, args) => _progressBar.Progress=((int)(100 * args.PercentComplete));
			Task<int> downloadTask =  DownloadHelperClass.CreateDownloadTask(DownloadHelperClass.ImageToDownload, progressReporter);
			int bytesDownloaded = await downloadTask;
			System.Diagnostics.Debug.WriteLine("Downloaded {0} bytes.", bytesDownloaded);
		}
		void fnInitializeCircleBar()
		{
			initCircularProgressBar = (InitCircularProgressClass)FindViewById(Resource.Id.rate_progress_bar);
			initCircularProgressBar.setMax(100);
			initCircularProgressBar.ClearAnimation(); 
			initCircularProgressBar.setTextSize (24);
			initCircularProgressBar.setTextColor (Color.ParseColor ("#fff69212"));
			initCircularProgressBar.setTextTypeFaceBold ();
			initCircularProgressBar.setProgress (0);

			initCircularProgressBar.getCircularProgressBar().setCircleWidth(20);
			initCircularProgressBar.getCircularProgressBar().setMax(100);
			initCircularProgressBar.getCircularProgressBar ().setPrimaryColor (Color.ParseColor ("#fff69212"));// Color.Rgb(255,215,0));
			initCircularProgressBar.getCircularProgressBar().setBackgroundColor(Color.White);  
		}


	}
}


