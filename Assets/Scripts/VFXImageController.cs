using System;
using System.Net.Http;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class VFXImageController : MonoBehaviour
{
    public TMP_InputField urlInputField;        
    public VisualEffect visualEffect;       
    public string texturePropertyName = "Texture";

    private void Start()
    {
        urlInputField.onEndEdit.AddListener(async url => await OnUrlEntered(url));
    }

    private async Task OnUrlEntered(string url)
    {
        if (await IsImageUrlValidAsync(url))
        {
            Texture2D downloadedTexture = await DownloadImageAsync(url);
            if (downloadedTexture != null)
            {
                ApplyImageToVFXGraph(visualEffect, downloadedTexture);
            }
        }
        else
        {
            Debug.LogWarning("Invalid URL or unsupported image format.");
        }
    }

    private async Task<bool> IsImageUrlValidAsync(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));
                if (response.IsSuccessStatusCode)
                {
                    var contentType = response.Content.Headers.ContentType.MediaType;
                    return contentType == "image/jpeg" || contentType == "image/png";
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error checking URL: {ex.Message}");
            }
            return false;
        }
    }

    private async Task<Texture2D> DownloadImageAsync(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                byte[] imageData = await client.GetByteArrayAsync(url);
                Texture2D texture = new Texture2D(2, 2);
                if (texture.LoadImage(imageData))
                {
                    return texture;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error downloading image: {ex.Message}");
            }
            return null;
        }
    }

    private void ApplyImageToVFXGraph(VisualEffect vfx, Texture2D texture)
    {
        if (vfx.HasTexture(texturePropertyName))
        {
            vfx.SetTexture(texturePropertyName, texture);
        }
        else
        {
            Debug.LogWarning($"Property '{texturePropertyName}' not found in VFX Graph.");
        }
    }
}
