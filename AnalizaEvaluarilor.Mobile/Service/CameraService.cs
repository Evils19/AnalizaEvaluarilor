namespace AnalizaEvaluarilor.Mobile.Service;

public class CameraService
{

    public async Task<FileResult?> CapturePhotoAync()
    {

        if (MediaPicker.Default.IsCaptureSupported)
        {
            return await MediaPicker.Default.CapturePhotoAsync()!;
        }

        throw new NotSupportedException("Capture Photo is not supported on this device");
    }
    
    public async Task<FileResult?> PickPhotoAsync()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            return await MediaPicker.Default.PickPhotoAsync()!;
        }

        throw new NotSupportedException("Pick Photo is not supported on this device");
    }

}