using System.Net;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Training.API.Plans.Core.Abstraction.Services;
using Training.API.Plans.Core.Domain;
using Training.API.Plans.Integrations.Configuration;

namespace Training.API.Plans.Integrations.Services;

public class StorageService : IStorageService
{
    private readonly IAmazonS3 _awsS3Client;
    private readonly StorageConfiguration _options;
    public StorageService(StorageConfiguration options)
    {
        _options = options;
        _awsS3Client = new AmazonS3Client(new BasicAWSCredentials(_options.AccessKey, _options.AccessKeySecret), new AmazonS3Config
        {
            ServiceURL = _options.S3LoginRoot,
            ForcePathStyle = true,
        });
    }

    public async Task<ExercisesInfoDetailsModel> DownloadExerciseInfoDetailsAsync(ExercisesInfoModel Exercise)
    {
        var files = await Exercise.Files.ToAsyncEnumerable().SelectAwait( async obj => await this.DownloadFileAsync(obj.PhotoId, obj.Identifier)).ToListAsync();
        return new ExercisesInfoDetailsModel(Exercise.Identifier, Exercise.Name, Exercise.Description, Exercise.AuthorId, Exercise.BodyElements, files, Exercise.CreatedAt, Exercise.CreatedBy, Exercise.ModifiedAt, Exercise.ModifiedBy);
    }

    public async Task<FileDetailsModel> DownloadFileAsync(string Key, Guid ExerciseId)
    {
        MemoryStream? ms = null;
        long Size = 0;
        string ContentType = string.Empty;
        try
        {
            GetObjectRequest getObjectRequest = new()
            {
                BucketName = _options.BucketName,
                Key = Key
            };
            using (var response = await _awsS3Client.GetObjectAsync(getObjectRequest))
            {
                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    using (ms = new MemoryStream())
                    {
                        
                        await response.ResponseStream.CopyToAsync(ms);
                    }
                    ContentType = response.Headers.ContentType;
                    Size = response.Headers.ContentLength;
                }
            }
            if (ms is null || ms.ToArray().Length < 1)
                throw new FileNotFoundException(string.Format("The document '{0}' is not found", Key));
            
            return new FileDetailsModel(ExerciseId,Key.Split("-EXI")[0],Size,ContentType,ms.ToArray());
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public async Task RemoveFileAsync(string Key)
    { 
        try
        {
            DeleteObjectRequest request = new()
            {
                BucketName = _options.BucketName,
                Key = Key,
            };
            
            await _awsS3Client.DeleteObjectAsync(request);
        }
        catch (System.Exception e)
        {
            
            throw;
        }
    }

    public async Task UploadFileAsync(IFormFile File, string Key)
    {
        try
        {
            using var newMemoryStream = new MemoryStream();
            File.CopyTo(newMemoryStream);
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = newMemoryStream,
                Key = Key,
                BucketName = _options.BucketName,
                ContentType = File.ContentType,
                CannedACL = S3CannedACL.NoACL
            };
            var fileTransferUtility = new TransferUtility(_awsS3Client);
            await fileTransferUtility.UploadAsync(uploadRequest);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
}