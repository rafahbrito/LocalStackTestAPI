using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;

namespace LocalStackTestAPI.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class S3FilesController : ControllerBase
{
    private readonly IAmazonS3 _s3Client;

    public S3FilesController(IAmazonS3 s3Client) => _s3Client = s3Client;

    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file, string bucketName, string? prefix)
    {
        var existingBuckets = await _s3Client.ListBucketsAsync();
        if (!existingBuckets.Buckets.Exists(b => b.BucketName == bucketName))
            return BadRequest($"O bucket {bucketName} não existe.");

        var request = new PutObjectRequest
        {
            BucketName = bucketName,
            Key = string.IsNullOrWhiteSpace(prefix) ? file.FileName : $"{prefix.TrimEnd('/')}/{file.FileName}",
            InputStream = file.OpenReadStream(),
            ContentType = file.ContentType
        };
        await _s3Client.PutObjectAsync(request);
        return Ok($"Arquivo {file.FileName} enviado com sucesso!");
    }

    [HttpGet]
    public async Task<IActionResult> ListFiles(string bucketName, string? prefix)
    {
        var existingBuckets = await _s3Client.ListBucketsAsync();

        if (!existingBuckets.Buckets.Exists(b => b.BucketName == bucketName))
            return BadRequest($"O bucket {bucketName} não existe.");

        var request = new ListObjectsV2Request()
        {
            BucketName = bucketName,
            Prefix = prefix
        };

        var response = await _s3Client.ListObjectsV2Async(request);
        var files = response.S3Objects.Select(file =>
        {
            var urlRequest = new GetPreSignedUrlRequest()
            {
                BucketName = bucketName,
                Key = file.Key,
                Expires = DateTime.UtcNow.AddMinutes(1)
            };
            return new
            {
                Name = file.Key,
                PresignedUrl = _s3Client.GetPreSignedURL(urlRequest)
            };
        }).ToList();

        return Ok(files);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteFile(string bucketName, string key)
    {
        var existingBuckets = await _s3Client.ListBucketsAsync();

        if (!existingBuckets.Buckets.Exists(b => b.BucketName == bucketName))
            return NotFound($"O bucket {bucketName} não existe.");
        
        await _s3Client.DeleteObjectAsync(bucketName, key);

        return NoContent();
    }
}
