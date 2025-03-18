using BlazorQrCodeScanner;
using BlazorQrCodeScanner.MediaTrack;

namespace NeoBotAI.Components.Shared
{
    public partial class Scanner : IAsyncDisposable
    {
        private QrCodeScanner? qrScanner;
        private bool isScannerStarted;
        private string scannedImage;

        /// <summary>
        /// Initializes the component and requests camera permissions.
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            //Do native permission check on MAUI only
            if (await Permissions.CheckStatusAsync<Permissions.Camera>() != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.Camera>();
            }
            await base.OnInitializedAsync();
        }

        /// <summary>
        /// Called when the QR code scanner instance is created.
        /// </summary>
        private void OnScannerCreated()
        {
            qrScanner?.StartAsync(new MediaTrackConstraintSet
            {
                FacingMode = VideoFacingMode.Environment
            }, new QrCodeConfig
            {
                FormatsToSupport = new[] { BarcodeType.QR_CODE },
                QrBox = new QrBoxFunction("calculateBoundingBox"), // or pass QrBoxSize width and height or aspectratio as QrBoxNumber
                ExperimentalFeatures = new ExperimentalFeaturesConfig
                {
                    UseBarCodeDetectorIfSupported = true
                },
                DefaultZoomValueIfSupported = 2,
                Fps = 10
            });
        }

        /// <summary>
        /// Called when a QR code is successfully scanned.
        /// </summary>
        /// <param name="result">The result of the QR code scan.</param>
        private void OnQrCodeScanned(QrCodeScanResult result)
        {
            Console.WriteLine(result.DecodedText);
            scannedImage = result.ImageUrl;
            StateHasChanged();
        }

        /// <summary>
        /// Called when the scanner starts.
        /// </summary>
        private async void OnScanStarted()
        {
            await Task.Delay(500);
            isScannerStarted = true;
            StateHasChanged();
        }

        public async ValueTask DisposeAsync()
        {
            if(qrScanner is not null) 
                await qrScanner.DisposeAsync();
        }
    }
}