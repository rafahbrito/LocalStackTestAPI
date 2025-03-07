using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;

namespace LocalStackTestAPI.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class S3BucketsController : ControllerBase
{
    private readonly IAmazonS3 _s3Client;

    public S3BucketsController(IAmazonS3 s3Client) => _s3Client = s3Client;

    [HttpPost]
    public async Task<IActionResult> CreateBucket(string bucketName)
    {
        var existingBuckets = await _s3Client.ListBucketsAsync();

        if(existingBuckets.Buckets.Exists(b => b.BucketName == bucketName))
            return BadRequest($"O bucket {bucketName} já existe.");

        var request = new PutBucketRequest { BucketName = bucketName };
        await _s3Client.PutBucketAsync(request);

        return Ok($"Bucket {bucketName} criado com sucesso!");
    }

    [HttpGet]
    public async Task<IActionResult> ListBuckets()
    {
        var response = await _s3Client.ListBucketsAsync();

        var buckets = new List<string>();

        foreach (var bucket in response.Buckets)
        {
            buckets.Add(bucket.BucketName);
        }

        return Ok(buckets);
    }


    [HttpDelete]
    public async Task<IActionResult> DeleteBucket(string bucketName)
    {
        var existingBuckets = await _s3Client.ListBucketsAsync();

        if (!existingBuckets.Buckets.Exists(b => b.BucketName == bucketName))
            return NotFound($"O bucket {bucketName} não foi encontrado.");

        await _s3Client.DeleteBucketAsync(bucketName);

        return NoContent();
    }
}

