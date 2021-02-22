namespace MusicManager.Server.Core.Validators.FileTypeValidators.FileTypes
{
    public sealed class Wav : FileType
    {
        public Wav()
        {
            Name = "WAV";
            Description = "WAVE Audio File";
            AddExtensions("wav");
            AddSignatures(
                new byte[] { 0x52, 0x49, 0x46, 0x46 }
            );
        }
    }
}
