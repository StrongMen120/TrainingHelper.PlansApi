namespace Training.API.Plans.Integrations.Configuration;
public class StorageConfiguration
{
    public string BucketName { get; set; }
    public string AccessKey { get; set; }
    public string AccessKeySecret { get; set; }
    public string S3LoginRoot { get; set; }
}
